using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoAmongUs
{
    public class Memory
    {
        private static Process proc;
        private static List<Module> modules;

        private static void LoadModules()
        {
            if (proc == null) return;

            modules = new List<Module>();
            IntPtr[] buffer = new IntPtr[1024];
            uint cb = (uint)(IntPtr.Size * buffer.Length);
            if (WinAPI.EnumProcessModulesEx(proc.Handle, buffer, cb, out uint totalModules, 3u))
            {
                uint moduleSize = totalModules / (uint)IntPtr.Size;
                StringBuilder stringBuilder = new StringBuilder(260);
                for (uint count = 0; count < moduleSize; count++)
                {
                    stringBuilder.Clear();
                    if (WinAPI.GetModuleFileNameEx(proc.Handle, buffer[count], stringBuilder, (uint)stringBuilder.Capacity) == 0u)
                        break;
                    string fileName = stringBuilder.ToString();
                    stringBuilder.Clear();
                    if (WinAPI.GetModuleBaseName(proc.Handle, buffer[count], stringBuilder, (uint)stringBuilder.Capacity) == 0u)
                        break;
                    string moduleName = stringBuilder.ToString();
                    ModuleInfo moduleInfo = default;
                    if (!WinAPI.GetModuleInformation(proc.Handle, buffer[count], out moduleInfo, (uint)Marshal.SizeOf(moduleInfo)))
                        break;
                    modules.Add(new Module
                    {
                        FileName = fileName,
                        BaseAddress = moduleInfo.BaseAddress,
                        EntryPointAddress = moduleInfo.EntryPoint,
                        MemorySize = moduleInfo.ModuleSize,
                        Name = moduleName
                    });
                }
            }
        }

        public static Module GetModuleByName(string moduleName)
        {
            if (proc == null) return null;
            proc.Refresh();
            if (modules == null) LoadModules();

            foreach (Module module in modules)
            {
                if (module.Name.Equals(moduleName, StringComparison.OrdinalIgnoreCase))
                {
                    return module;
                }
            }

            LoadModules();
            return null;
        }

        public static IntPtr GetRealAddress(IntPtr baseAddress, int[] offsets)
        {
            if (proc == null) return IntPtr.Zero;

            IntPtr realAddress = ReadAddress(baseAddress);

            if (offsets == null) return realAddress;

            foreach (int offset in offsets) {
                realAddress = ReadAddress(realAddress + offset);
            }

            return realAddress;
        }

        public static int ReadInt(IntPtr address, int defaultReturn)
        {
            if (proc == null) return 0;

            byte[] buffer = new byte[4];

            try { 
                WinAPI.ReadProcessMemory(proc.Handle, address, buffer, buffer.Length, out int bytesRead);
                
                if (bytesRead == 0) return defaultReturn;

                return BitConverter.ToInt32(buffer, 0);

            } catch (Exception) { 
                return 0; 
            }
        }

        public static string ReadString(IntPtr address)
        {
            if (proc == null) return null;

            int stringLength = ReadInt(address + 0x8, 0);

            byte[] buffer = ReadBytes(address + 0xC, stringLength << 1);

            return System.Text.Encoding.Unicode.GetString(buffer);
        }

        public static byte[] ReadBytes(IntPtr address, int size)
        {
            if (proc == null) return null;

            byte[] buffer = new byte[size];

            WinAPI.ReadProcessMemory(proc.Handle, address, buffer, size, out int bytesRead);

            return buffer;
        }

        public static bool WriteInt(IntPtr address, int value)
        {

            byte[] buffer = BitConverter.GetBytes(value);

            int bytesWritten = Write(address, buffer);

            return bytesWritten > 0;
        }

        public static bool WriteFloat(IntPtr address, float value)
        {
            byte[] buffer = BitConverter.GetBytes(value);

            int bytesWritten = Write(address, buffer);

            return bytesWritten > 0;
        }

        public static bool WriteString(IntPtr address, string value)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(value);

            int bytesWritten = Write(address, buffer);

            return bytesWritten > 0;
        }

        public static int Write(IntPtr address, byte[] buffer)
        {
            WinAPI.WriteProcessMemory(proc.Handle, address, buffer, buffer.Length, out int bytesWritten);

            return bytesWritten;
        }

        public static T Read<T>(byte[] bytes)
        {
            if (proc == null) return default(T);

            GCHandle gcHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var data = (T)Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof(T));
            gcHandle.Free();
            return data;
        }

        public static IntPtr ReadAddress(IntPtr address)
        {
            if (proc == null) return IntPtr.Zero;

            byte[] buffer = new byte[4];

            try { WinAPI.ReadProcessMemory(proc.Handle, address, buffer, buffer.Length, out int bytesRead); }
            catch (Exception) { return IntPtr.Zero; }

            return (IntPtr)BitConverter.ToUInt32(buffer, 0);
        }

        public static bool OpenProcess(string processName) {
            proc = Process.GetProcessesByName(processName).FirstOrDefault();

            return proc != null;
        }

        public class Module
        {
            public IntPtr BaseAddress { get; set; }
            public IntPtr EntryPointAddress { get; set; }
            public string FileName { get; set; }
            public uint MemorySize { get; set; }
            public string Name { get; set; }
            public FileVersionInfo FileVersionInfo { get { return FileVersionInfo.GetVersionInfo(FileName); } }
            public override string ToString()
            {
                return Name ?? base.ToString();
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ModuleInfo
        {
            public IntPtr BaseAddress;
            public uint ModuleSize;
            public IntPtr EntryPoint;
        }

        private static class WinAPI
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool IsWow64Process(IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] out bool wow64Process);
            [DllImport("psapi.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool EnumProcessModulesEx(IntPtr hProcess, [Out] IntPtr[] lphModule, uint cb, out uint lpcbNeeded, uint dwFilterFlag);
            [DllImport("psapi.dll", SetLastError = true)]
            public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, uint nSize);
            [DllImport("psapi.dll")]
            public static extern uint GetModuleBaseName(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, uint nSize);
            [DllImport("psapi.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetModuleInformation(IntPtr hProcess, IntPtr hModule, out ModuleInfo lpmodinfo, uint cb);
        }
    }
}
