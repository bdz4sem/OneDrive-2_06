using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace OneDrive
{
    public partial class OpenSecurity : Form
    {
        public OpenSecurity()
        {
            InitializeComponent();
        }
        private void OpenODSButton_Click(object sender, EventArgs e)
        {
            string password = passwordTextbox.Text;
            ODSecurity.Open(password);
            if (ODSecurity.guid != null)
            {
                this.Dispose();
            }
            else
            {
                textBox1.Text = "Wrong Password!";
            }
        }

        private void passwordTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenODSButton_Click(this, e);
            }
        }

    }
}
