using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        const int MOD_ALT = 0x0001;
        const int MOD_CONTROL = 0x0002;
        const int MOD_SHIFT = 0x0004;
        const int WM_HOTKEY = 0x0312;

        const int HOTKEY_ID = 0x0001;  // 0x0000～0xbfff 内の適当な値でよい
        const int HOTKEY2_ID = 0x0002;

        //ダイナミックリンクライブラリの登録
        [DllImport("user32.dll")]
        extern static int RegisterHotKey(IntPtr HWnd, int ID, int MOD_KEY, int KEY);
        //ダイナミックリンクライブラリの解除
        [DllImport("user32.dll")]
        extern static int UnregisterHotKey(IntPtr HWnd, int ID);

        public Form1()
        {
            InitializeComponent();
        }

        //ホットキーが入力されたときの処理
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY)
            {
                if ((int)m.WParam == HOTKEY_ID)
                {
                    Visible = !Visible;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Visible = false;
            //this.WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            RegisterHotKey(this.Handle, HOTKEY_ID, MOD_ALT, (int)Keys.A);
        }

        //taskbarにあるアイコンをクリックしたときの処理
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            //右クリックしたとき
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show();
            }
            //左クリックしたとき
            else if (e.Button == MouseButtons.Left)
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        //private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        //フォームのサイズが最小化されたとき、タスクを非表示にする
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
            }
        }

        //フォームが終了したときに、ホットキーを解除する
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(this.Handle, HOTKEY_ID);
        }

        //タスクから最小化を選んだとき、フォームを最小化する
        private void 最小化sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }

        //タスクから終了を選んだとき、フォームを終了する
        private void 終了ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
