namespace OneDrive
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node0");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.SignIn = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.meImage = new System.Windows.Forms.PictureBox();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.UploadButton = new System.Windows.Forms.Button();
            this.SignOut = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.QueueBox = new System.Windows.Forms.RichTextBox();
            this.Sync = new System.Windows.Forms.Button();
            this.Browse = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Delete = new System.Windows.Forms.Button();
            this.CompletedBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DownloadMyFiles = new System.Windows.Forms.Button();
            this.CreateFolderButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.meImage)).BeginInit();
            this.SuspendLayout();
            // 
            // SignIn
            // 
            this.SignIn.Location = new System.Drawing.Point(525, 120);
            this.SignIn.Name = "SignIn";
            this.SignIn.Size = new System.Drawing.Size(80, 23);
            this.SignIn.TabIndex = 0;
            this.SignIn.Text = "Sign In";
            this.SignIn.UseVisualStyleBackColor = true;
            this.SignIn.Click += new System.EventHandler(this.SignInButton_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nameLabel.Location = new System.Drawing.Point(525, 95);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(80, 22);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // treeView1
            // 
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            treeNode2.Name = "Drive";
            treeNode2.Text = "Node0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(367, 316);
            this.treeView1.TabIndex = 6;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "fold2.jpg");
            this.imageList1.Images.SetKeyName(1, "файл.png");
            this.imageList1.Images.SetKeyName(2, "080832-glossy-black-icon-business-lock6-sc48.png");
            // 
            // meImage
            // 
            this.meImage.Image = global::OneDrive.Properties.Resources.noavatar;
            this.meImage.InitialImage = ((System.Drawing.Image)(resources.GetObject("meImage.InitialImage")));
            this.meImage.Location = new System.Drawing.Point(525, 12);
            this.meImage.Name = "meImage";
            this.meImage.Size = new System.Drawing.Size(80, 80);
            this.meImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.meImage.TabIndex = 1;
            this.meImage.TabStop = false;
            this.meImage.WaitOnLoad = true;
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(385, 12);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(75, 23);
            this.DownloadButton.TabIndex = 8;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // UploadButton
            // 
            this.UploadButton.Location = new System.Drawing.Point(385, 41);
            this.UploadButton.Name = "UploadButton";
            this.UploadButton.Size = new System.Drawing.Size(75, 23);
            this.UploadButton.TabIndex = 9;
            this.UploadButton.Text = "Upload";
            this.UploadButton.UseVisualStyleBackColor = true;
            this.UploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // SignOut
            // 
            this.SignOut.Location = new System.Drawing.Point(525, 149);
            this.SignOut.Name = "SignOut";
            this.SignOut.Size = new System.Drawing.Size(80, 23);
            this.SignOut.TabIndex = 11;
            this.SignOut.Text = "Sign Out";
            this.SignOut.UseVisualStyleBackColor = true;
            this.SignOut.Click += new System.EventHandler(this.SignOutButton_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(525, 178);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(80, 23);
            this.webBrowser1.TabIndex = 12;
            this.webBrowser1.Visible = false;
            // 
            // QueueBox
            // 
            this.QueueBox.Location = new System.Drawing.Point(12, 357);
            this.QueueBox.Name = "QueueBox";
            this.QueueBox.Size = new System.Drawing.Size(367, 114);
            this.QueueBox.TabIndex = 16;
            this.QueueBox.Text = "";
            // 
            // Sync
            // 
            this.Sync.Location = new System.Drawing.Point(385, 137);
            this.Sync.Name = "Sync";
            this.Sync.Size = new System.Drawing.Size(107, 23);
            this.Sync.TabIndex = 17;
            this.Sync.Text = "Sync";
            this.Sync.UseVisualStyleBackColor = true;
            this.Sync.Click += new System.EventHandler(this.Sync_Click);
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(509, 251);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(72, 23);
            this.Browse.TabIndex = 18;
            this.Browse.Text = "Browse";
            this.Browse.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(385, 225);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(196, 20);
            this.textBox2.TabIndex = 19;
            this.textBox2.Text = "C:\\oneDrive";
            // 
            // Delete
            // 
            this.Delete.Location = new System.Drawing.Point(385, 70);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(75, 23);
            this.Delete.TabIndex = 20;
            this.Delete.Text = "Delete";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // CompletedBox
            // 
            this.CompletedBox.Location = new System.Drawing.Point(12, 497);
            this.CompletedBox.Name = "CompletedBox";
            this.CompletedBox.Size = new System.Drawing.Size(367, 114);
            this.CompletedBox.TabIndex = 21;
            this.CompletedBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Processing";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 481);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Completed";
            // 
            // DownloadMyFiles
            // 
            this.DownloadMyFiles.Location = new System.Drawing.Point(385, 166);
            this.DownloadMyFiles.Name = "DownloadMyFiles";
            this.DownloadMyFiles.Size = new System.Drawing.Size(107, 23);
            this.DownloadMyFiles.TabIndex = 24;
            this.DownloadMyFiles.Text = "Download My Files";
            this.DownloadMyFiles.UseVisualStyleBackColor = true;
            this.DownloadMyFiles.Click += new System.EventHandler(this.DownloadMyFiles_Click);
            // 
            // CreateFolderButton
            // 
            this.CreateFolderButton.Location = new System.Drawing.Point(509, 315);
            this.CreateFolderButton.Name = "CreateFolderButton";
            this.CreateFolderButton.Size = new System.Drawing.Size(75, 23);
            this.CreateFolderButton.TabIndex = 25;
            this.CreateFolderButton.Text = "CreateFolder";
            this.CreateFolderButton.UseVisualStyleBackColor = true;
            this.CreateFolderButton.Click += new System.EventHandler(this.CreateFolderButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(385, 289);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(196, 20);
            this.textBox1.TabIndex = 26;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(304, 328);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OneDrive.Properties.Resources.Background3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(622, 626);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.CreateFolderButton);
            this.Controls.Add(this.DownloadMyFiles);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CompletedBox);
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Browse);
            this.Controls.Add(this.Sync);
            this.Controls.Add(this.QueueBox);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.SignOut);
            this.Controls.Add(this.UploadButton);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.meImage);
            this.Controls.Add(this.SignIn);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "OneDrive Client";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.meImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SignIn;
        private System.Windows.Forms.PictureBox meImage;
        public System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Button UploadButton;
        public System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button SignOut;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.RichTextBox QueueBox;
        private System.Windows.Forms.Button Sync;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button Delete;
        private System.Windows.Forms.RichTextBox CompletedBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DownloadMyFiles;
        private System.Windows.Forms.Button CreateFolderButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}

