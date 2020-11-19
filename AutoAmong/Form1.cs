using System;
using System.Threading;
using System.Windows.Forms;

namespace AutoAmong
{
    public partial class Form1 : Form
    {
        public static Form1 form;
        public static Label error;
        public string startupHash;
        public Form1(string hash)
        {
            startupHash = hash;
            if (startupHash != "")
                Hide();
            form = this;
            InitializeComponent();
            CenterToScreen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (startupHash != "")
            {
                syncField.Text = startupHash;
                syncBtn_Click(null, null);
            }
        }

        private void syncBtn_Click(object sender, EventArgs e)
        {
            error = syncLabel;
            syncBtn.Enabled = false;
            syncField.Enabled = false;
            Thread verifyThread = new Thread(new ThreadStart(verifyToken));
            verifyThread.Start();
            Update();
        }

        private void syncField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                syncBtn_Click(null, null);
        }

        private void verifyToken()
        {
            MethodInvoker inv = delegate {
                string token = syncField.Text;
                syncLabel.Text = "Falha ao sincronizar.";
                if (Connection.ping(token)) {
                    Hide();
                    syncLabel.Visible = false;
                    Update();
                    Form2 amongForm = new Form2(token);
                } else {
                    syncLabel.Visible = true;
                    Update();
                    Show();
                }
                syncBtn.Enabled = true;
                syncField.Enabled = true;
                syncField.Focus();
            };
            Invoke(inv);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
