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
    public delegate void AuthCompletedDelegate(AuthResult au);
    public partial class MyBrowser : Form
    {
        string startUrl;
        string endUrl;
        AuthResult authResult;
        private AuthCompletedDelegate authCompletedDelegate;
        
        public MyBrowser(string startUrl, string endUrl, AuthCompletedDelegate authCompletedDelegate)
        {
            this.startUrl = startUrl;
            this.endUrl = endUrl;
            InitializeComponent();
            this.authCompletedDelegate = authCompletedDelegate;
        }
        public void MyBrowser_Load()
        {
            webBrowser1.Navigate(this.startUrl);
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            toolStripTextBox1.Text = webBrowser1.Url.ToString();
            if (webBrowser1.Url.ToString().Contains(this.endUrl))
            {
                this.authCompletedDelegate(authResult = new AuthResult(webBrowser1.Url));
            }
        }
    }
}
