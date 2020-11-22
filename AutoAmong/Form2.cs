using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoAmong
{
    public partial class Form2 : Form
    {
        public static Form2 form;
        public static bool exitOnClose = false;
        private Bitmap[] cores = { AutoAmong.Resources._00, AutoAmong.Resources._01, AutoAmong.Resources._02,
                                   AutoAmong.Resources._03, AutoAmong.Resources._04, AutoAmong.Resources._05,
                                   AutoAmong.Resources._06, AutoAmong.Resources._07, AutoAmong.Resources._08,
                                   AutoAmong.Resources._09, AutoAmong.Resources._0a, AutoAmong.Resources._0b };
        private Connection conn;
        public Form2(string token) {
            form = this;
            InitializeComponent();
            this.CenterToScreen();
            exitOnClose = true;
            playersTable.Columns[0].Visible = false;
            conn = new Connection(token);
            conn.refreshDiscord();
            if (!this.IsDisposed) this.Show();
        }

        public void amongMemory() {
            Form2 form = this;
            MethodInvoker inv = delegate {
                AmongMemory.StartAnalyzing(form, conn);
            };
            try { this.Invoke(inv); } catch (Exception) { }
        }

        public void startAmong() {
            MethodInvoker inv = delegate {
                errorMsg.Text = "";
                playersTable.BackgroundColor = Color.White;
                playersTable.Enabled = true;
            };
            try { Invoke(inv); } catch (Exception) { }
        }

        public bool endAmong() {
            MethodInvoker inv = delegate {
                errorMsg.Text = "Crie uma sala no Among Us para começar";
                playersTable.BackgroundColor = Color.Gray;
                playersTable.Enabled = false;
            };
            try {
                bool returnValue = playersTable.Enabled;
                Invoke(inv);
                return returnValue;
            } catch (Exception) { }
            return false;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e) {
            try { AmongMemory.Stop(); } catch (Exception) { }
            try { if (amongThread!=null) amongThread.Interrupt(); } catch (Exception) { }
            if (exitOnClose)
                try { System.Environment.Exit(0); } catch (Win32Exception) { } catch (Exception) { }
            else
                Form1.form.Show();
        }

        public void dgAdd(string name, byte color, string discord) {
            MethodInvoker inv = delegate {
                if (playersTable.Rows
                       .Cast<DataGridViewRow>()
                       .Where(r => {
                           if (r.Cells[2].Value == null) return false;
                           else return r.Cells[2].Value.ToString().Equals(name);
                       })
                       .ToArray().Length > 0)
                    return;

                DataGridViewComboBoxCell CellSample = new DataGridViewComboBoxCell();
                CellSample.FlatStyle = FlatStyle.Flat;

                string[] discordUsers = { "Linking..." };
                CellSample.DataSource = discordUsers;
                CellSample.Value = discordUsers[0];

                int idx = playersTable.Rows.Add(AutoAmong.Resources.unmuted, cores[color], name, "Linking...", "Linking...");
                playersTable.Rows[idx].Cells[3] = CellSample;
                Update();
            };
            try { this.Invoke(inv); } catch (Exception) { }
        }

        public string dgDiscordIdByName(string name) {
            var where = playersTable.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => { try { return r.Cells[2].Value != null ? r.Cells[2].Value.ToString().Equals(name) : false; } catch (System.NullReferenceException) { return false; } });
            if (where.Count() > 0) {
                DataGridViewRow row = where.First();
                return row.Cells[4].Value.ToString();
            }
            else {
                return "<Unlinked>";
            }
        }

        public void dgSetColor(string name, byte color) {
            MethodInvoker inv = delegate {
                var where = playersTable.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => { try { return r.Cells[2].Value != null ? r.Cells[2].Value.ToString().Equals(name) : false; } catch (System.NullReferenceException) { return false; } });
                if (where.Count() > 0) {
                    DataGridViewRow row = where.First();
                    row.Cells[1].Value = cores[color];
                    Update();
                }
            };
            try { this.Invoke(inv); } catch (Exception) { }
        }

        public void dgRemove(string name) {
            MethodInvoker inv = delegate {
                try {
                    var where = playersTable.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => { try { return r.Cells[2].Value != null ? r.Cells[2].Value.ToString().Equals(name) : false; } catch (System.NullReferenceException) { return false; } });
                    if (where.Count() > 0) {
                        DataGridViewRow row = where.First();
                        playersTable.Rows.RemoveAt(row.Index);
                        Update();
                    }
                }
                catch (Exception) { }
            };
            try { this.Invoke(inv); } catch (Exception) { }
        }

        public void dgRemoveAll() {
            MethodInvoker inv = delegate {
                playersTable.Rows.Clear();
                Update();
            };
            try { this.Invoke(inv); } catch (Exception) { }
        }

        public static Thread amongThread;
        private void Form2_Load(object sender, EventArgs e) {
            playersTable.CellValueChanged += new DataGridViewCellEventHandler(DiscordUserComboBox_Change);
            playersTable.CurrentCellDirtyStateChanged += new EventHandler(DiscordUserComboBoxDirty_Change);
            playersTable.RowHeadersVisible = false;
            playersTable.BackgroundColor = Color.Gray;
            playersTable.Enabled = false;
            playersTable.MultiSelect = false;
            Update();
            amongThread = new Thread(new ThreadStart(amongMemory));
            amongThread.Start();

        }

        private void playersTable_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex == 0) {
                var row = playersTable.Rows[e.RowIndex];
                bool isMuted = ((Bitmap)row.Cells[0].Value).GetPixel(280, 280).GetHue() != 0;
                conn.forceMute(row.Cells[4].Value.ToString(), !isMuted);
                row.Cells[0].Value = isMuted ? AutoAmong.Resources.unmuted : AutoAmong.Resources.muted;
            } else if (e.ColumnIndex == 3) {
                var editingControl = playersTable.EditingControl as
                DataGridViewComboBoxEditingControl;
                if (editingControl != null)
                    editingControl.DroppedDown = true;
            }
        }

        private void playersTable_MouseDown(object sender, MouseEventArgs e) {
            if (playersTable.Rows.Count > 0) {
                playersTable.Rows[0].Cells[1].Selected = true;
                playersTable.Rows[0].Cells[1].Selected = false;
                playersTable.Invalidate();
            }
        }

        public void refreshDiscord() {
            conn.refreshDiscord();
            foreach (DataGridViewRow row in playersTable.Rows) {
                ((DataGridViewComboBoxCell)row.Cells[3]).Value = null;
                ((DataGridViewComboBoxCell)row.Cells[3]).Selected = false;
                playersTable.BeginEdit(false);

                DataGridViewComboBoxCell CellSample = new DataGridViewComboBoxCell();

                CellSample.Value = "<Unlinked>";
                CellSample.FlatStyle = FlatStyle.Flat;
                List<string> discordUsersList = new List<string>(){ "<Unlinked>" };
                row.Cells[4].Value = "<Unlinked>";
                foreach (var user in conn.discordUsers) {
                    discordUsersList.Add(user["tag"]);
                    if (row.Cells[2].Value.ToString().Trim() == user["nick"].Trim()) {
                        CellSample.Value = user["tag"];
                        row.Cells[4].Value = user["id"];
                    }
                }
                CellSample.DataSource = discordUsersList;
                row.Cells[3] = CellSample;
                Update();
            }
        }

        void DiscordUserComboBoxDirty_Change(object sender, EventArgs e) {
            if (playersTable.IsCurrentCellDirty) {
                playersTable.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DiscordUserComboBox_Change(object sender, DataGridViewCellEventArgs e) {
            var row = playersTable.Rows[e.RowIndex];
            DataGridViewComboBoxCell cb = (DataGridViewComboBoxCell) row.Cells[3];
            if (cb.Value != null && e.ColumnIndex == playersTable.Columns["Discord"].Index) {
                foreach (var user in conn.discordUsers) {
                    if (user["tag"] == cb.Value.ToString()) {
                        row.Cells[3].Value = user["tag"];
                        row.Cells[4].Value = user["id"];
                        conn.setUserIGN(user["id"], row.Cells[2].Value.ToString());

                        row.Cells[1].Selected = true;
                        row.Cells[1].Selected = false;
                        playersTable.Invalidate();
                    }
                }
            }
        }

        public string getEstado(bool client) {
            return client ? clientState.Text : matchState.Text;
        }

        public void setEstado(bool client, string state)
        {
            MethodInvoker inv = delegate {
                if (client)
                    clientState.Text = $"Estado: {state}";
                else
                    matchState.Text = $"Partida: {state}";
                this.Update();
            };

            try { this.Invoke(inv); } catch (Exception) { }
        }

        private void backBtn_Click(object sender, EventArgs e) {
            conn.endGame();
            Form1.form.Show();
            Form2.exitOnClose = false;
            Form2.form.Close();
        }

        private void voteBtn_Click(object sender, EventArgs e)
        {
            conn.inGame(false);
        }

        private void gameBtn_Click(object sender, EventArgs e)
        {
            conn.inGame(true);
        }
    }
}
