using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneDrive
{
    public partial class CreateSecurity : Form
    {
        public CreateSecurity()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ODSecurity.Create();
            string password1 = textBox1.Text;
            string password2 = textBox2.Text;
            if (password1 != password2) MessageBox.Show("Passwords do not match!");
            else ODSecurity.Save(password1);
            this.Dispose();
        }
        private void CreateSecurity_Load(object sender, EventArgs e)
        {

        }
    }
}
