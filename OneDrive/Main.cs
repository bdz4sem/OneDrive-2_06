using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;    
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Live;
using System.Collections;
using System.Web.Script.Serialization;
using System.Security.Cryptography;


namespace OneDrive
{
    public partial class Main : Form, IRefreshTokenHandler
    {
        public Main()
        {
            if (isPassword())
            {
                InitializeComponent();
                makeButtonsInvisible();
            }
            else Environment.Exit(0);
        }
        public string uploadFileName;
        public AuthResult authResult;
        string ClientID = "000000004814AD4C";
        MyBrowser myBrowser;
        LiveAuthClient liveAuthClient;
        LiveConnectClient liveConnectClient;
        private Microsoft.Live.RefreshTokenInfo refreshTokenInfo;
        string[] Scopes = { "wl.signin", "wl.basic", "wl.photos", "wl.share", "wl.skydrive", "wl.skydrive_update", "wl.work_profile" };

        LiveAuthClient AuthClient
        {
            get
            {
                if (this.liveAuthClient == null)
                {
                    this.AuthClient = new LiveAuthClient(ClientID, this);
                }
                return this.liveAuthClient;
            }

            set
            {
                this.liveAuthClient = value;
                this.liveConnectClient = null;
            }
        }
        
        // Security
        public static bool isPassword()
        {
            if (ODSecurity.IsODSCreated())
            {
                OpenSecurity openSecurity = new OpenSecurity();
                openSecurity.ShowDialog();
            }
            else
            {
                CreateSecurity createSecurity = new CreateSecurity();
                createSecurity.ShowDialog();
            }
            if (ODSecurity.guid != null) return true;
            return false;
        } // Window for entering app password, returns true if correct
        private CryptoStream createCryptoStream(Stream originalStream, string mode)
        {
            Rijndael rijAlg = Rijndael.Create();
            rijAlg.Key = ODSecurity.Key;
            rijAlg.IV = ODSecurity.IV;
            rijAlg.Padding = PaddingMode.ISO10126;
            ICryptoTransform crypt = null;
            CryptoStream crStream = null;
            switch (mode)
            {
                case "Decrypt":
                    crypt = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                    crStream = new CryptoStream(originalStream, crypt, CryptoStreamMode.Write);
                    break;
                case "Encrypt":
                    crypt = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                    crStream = new CryptoStream(originalStream, crypt, CryptoStreamMode.Read);
                    break;
            }
            return crStream;
        }

        
        // GUI only
        private void cleanupBrowser()
        {
            if (this.myBrowser != null)
            {
                this.myBrowser.Dispose();
                this.myBrowser = null;
            }
        } 
        public void makeButtonsInvisible()
        {
            SignIn.Enabled = true;
            DownloadButton.Enabled = false;
            UploadButton.Enabled = false;
            Sync.Enabled = false;
            Delete.Enabled = false;
            DownloadMyFiles.Enabled = false;
            SignOut.Enabled = false;
        }
        public void makeButtonsVisible()
        {
            SignIn.Enabled = false;
            DownloadButton.Enabled = true;
            UploadButton.Enabled = true;
            Sync.Enabled = true;
            Delete.Enabled = true;
            DownloadMyFiles.Enabled = true;
            SignOut.Enabled = true;
        }

        
        // Interaction with api
        private async void updateTree(TreeNode curNode)
        {
            drive tempDrive = new drive();
            driveList tempList;
            try
            {
                if (curNode.Name.StartsWith("folder"))
                {
                    tempDrive.id = curNode.Name;
                    tempList = await GetFolderInfo(tempDrive);
                }
                else
                {
                    tempDrive.id = curNode.Parent.Name;
                    tempList = await GetFolderInfo(tempDrive);
                }
                List<string> listOfFilesInTree = new List<string>();
                if (curNode.Nodes != null)
                    foreach (TreeNode i in curNode.Nodes)
                    {
                        listOfFilesInTree.Add(i.Text);
                    }
                foreach (drive iterDrive in tempList.data)
                {
                    if (!listOfFilesInTree.Contains(iterDrive.name))
                    {
                        TreeNode node = curNode.Nodes.Add(iterDrive.id, iterDrive.name);
                        if (ODSecurity.guid != null)
                        {
                            node.ImageIndex = node.SelectedImageIndex = 2;//setting image of encrypted files
                        }
                        else
                        {
                            node.ImageIndex = node.SelectedImageIndex = 1;
                        }
                    }
                }
                drive driveForComparison = new drive();
               /* foreach (TreeNode i in curNode.Parent.Nodes)
                {
                    if (i != null)
                    {
                        driveForComparison.name = i.Text;
                        if (!tempList.data.Contains(driveForComparison) && (!i.Name.StartsWith("folder")))
                        {
                            i.Remove();
                        }
                    }
                }
                */
            }
            catch (Exception ex)
            {
                QueueBox.AppendText("Error (Update) : \n" + ex.Message + "\n");
            }

        }
        public async void initializeTree()
        {
            try
            {
                LiveOperationResult res = await this.liveConnectClient.GetAsync("me/skydrive/files");

                JavaScriptSerializer ser = new JavaScriptSerializer();
                driveList rootDriveList = ser.Deserialize<driveList>(res.RawResult);
                drive Rootdrive = new drive();
                Rootdrive.name = "root";
                Rootdrive.id = "me/skydrive";

                treeView1.TopNode = new TreeNode();

                recursiveExplorer(this.treeView1.TopNode, Rootdrive);
            }
            catch (Exception ex)
            {
                QueueBox.AppendText("Error (InitializeTree) : \n" + ex.Message + "\n");
            }

        }
        private async void metaUpload(TreeNode curNode, string name)
        {
            drive curDrive = new drive();
            curDrive.id = curNode.Name;
            curDrive.name = curNode.Text;
            driveList localList = new driveList();
            localList = await GetFolderInfo(curDrive);
            foreach (drive i in localList.data)
            {
                descriptionSer temp = new descriptionSer();
                temp.description = ODSecurity.guid;
                JavaScriptSerializer ser = new JavaScriptSerializer();
                string currentDescription = ser.Serialize(temp);
                if (name.EndsWith(i.name))
                {
                    await this.liveConnectClient.PutAsync(i.id, currentDescription);
                }
            }
        }
        public async void recursiveExplorer(TreeNode currentNode, drive currentDrive)
        {
            try
            {
                driveList localList;
                localList = await GetFolderInfo(currentDrive);
                if (localList.data.Count() > 0)
                {
                    foreach (drive i in localList.data)
                    {
                        if (i.id.StartsWith("folder"))
                        {
                            TreeNode node = currentNode.Nodes.Add(i.id, i.name);
                            this.recursiveExplorer(node, i);
                            node.ImageIndex = node.SelectedImageIndex = 0;
                        }
                        else if (i.id.StartsWith("file"))
                        {
                            TreeNode nodes = currentNode.Nodes.Add(i.id, i.name);

                            if (i.description == ODSecurity.guid)
                            {
                                nodes.ImageIndex = nodes.SelectedImageIndex = 2;

                            }
                            else
                            {
                                nodes.ImageIndex = nodes.SelectedImageIndex = 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueueBox.AppendText("Error (RecursiveExplorer) : \n" + ex.Message + "\n");
            }

        }    
        
        public async Task<driveList> GetFolderInfo(drive thisDrive)
        {
            driveList resultList = null;
            try
            {
                LiveOperationResult result = await this.liveConnectClient.GetAsync(string.Concat(thisDrive.id, "/files"));
                JavaScriptSerializer ser = new JavaScriptSerializer();
                resultList = ser.Deserialize<driveList>(result.RawResult);
                return resultList;
            }
            catch (Exception ex)
            {
                QueueBox.AppendText("Error (GetFolderInfo) : \n" + ex.Message + "\n"); return resultList;
            }
        }        
        public async Task FolderSync(DirectoryInfo dir,TreeNode curNode)
        {
            FileInfo _temp = null;
            try
            {
                drive currentDrive = new drive();
                currentDrive.id = curNode.Name;
                currentDrive.name = curNode.Text;
                driveList currentDriveFiles = await GetFolderInfo(currentDrive);
                FileInfo[] currentFolderFiles = dir.GetFiles();

                drive tempDrive = new drive();

                foreach (FileInfo i in currentFolderFiles)
                {
                    _temp = i;
                    tempDrive.name = i.Name;
                    if (currentDriveFiles.data.Contains(tempDrive))
                    {

                    }
                    else
                    {
                        QueueBox.AppendText("Uploading : " + i.Name + "\n");
                        await this.UploadFromSource(currentDrive.id, i);
                        this.uploadFileName = i.Name;
                        metaUpload(curNode, i.Name);
                        QueueBox.Text = QueueBox.Text.Replace("Uploading : " + i.Name + "\n", "");
                        CompletedBox.AppendText("Uploaded : " + i.Name + "\n");
                    }
                }
               
                List<string> listOfNamesInFolder = new List<string>();
                foreach (FileInfo i in currentFolderFiles)
                {
                    listOfNamesInFolder.Add(i.Name);
                }
                DirectoryInfo[] currentFolders = dir.GetDirectories();
                foreach (DirectoryInfo i in currentFolders)
                {
                    folderSet(i, currentDrive);                      
                }
                updateTree(curNode);
            }
            catch (Exception ex)
            {
                QueueBox.Text = QueueBox.Text.Replace("Uploading : " + _temp.Name + "\n", "");
                QueueBox.AppendText("Error (FolderSync) : \n" + ex.Message + "\n");
            }
        }
        public async void folderSet(DirectoryInfo inf, drive curFolderDrive)
        {
            driveList list = new driveList();
            drive tempDrive = new drive();
            tempDrive.name = inf.Name;
            if (list.data.Contains(tempDrive))
            {
                return;
            }
            else
            {
                folderName temp = new folderName();
                temp.name = tempDrive.name;
                JavaScriptSerializer ser = new JavaScriptSerializer();
                string request = ser.Serialize(temp);
                await this.liveConnectClient.PostAsync(curFolderDrive.id, request);
            }


        }
        private async Task<LiveOperationResult> UploadFromSource(string path, FileInfo file)
        {
            Stream stream = file.OpenRead();
            CryptoStream crStream = createCryptoStream(stream, "Encrypt");
            using (crStream)
            {
                return await this.liveConnectClient.UploadAsync(path, file.Name, crStream, OverwriteOption.DoNotOverwrite);
            }
        }
        private async Task DownloadFile(string path, string name)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            Stream stream = null;
            dialog.RestoreDirectory = true;
            dialog.FileName = name;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                throw new InvalidOperationException("No file is picked to upload.");
            }
            try
            {
                if ((stream = dialog.OpenFile()) == null)
                {
                    throw new Exception("Unable to open the file selected to upload.");
                }
                CryptoStream crStream = createCryptoStream(stream, "Decrypt");
                using (crStream)
                {
                    LiveDownloadOperationResult result = await this.liveConnectClient.DownloadAsync(string.Concat(path, "/content"));
                    if (result.Stream != null)
                    {
                        using (result.Stream)
                        {
                            await result.Stream.CopyToAsync(crStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task SilentDowloadFile(string path, string name)
        {
            FileStream stream = new FileStream(textBox2.Text + "/" + name, FileMode.Create);
            try
            {
                CryptoStream crStream = createCryptoStream(stream, "Decrypt");
                using (crStream)
                {
                    LiveDownloadOperationResult result = await this.liveConnectClient.DownloadAsync(string.Concat(path, "/content"));
                    if (result.Stream != null)
                    {
                        using (result.Stream)
                        {
                            await result.Stream.CopyToAsync(crStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<LiveOperationResult> UploadFile(string path)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            Stream stream = null;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                throw new InvalidOperationException("No file is picked to upload.");
            }
            try
            {
                if ((stream = dialog.OpenFile()) == null)
                {
                    throw new Exception("Unable to open the file selected to upload.");
                }
                CryptoStream crStream = createCryptoStream(stream, "Encrypt");
                this.uploadFileName = dialog.FileName;
                using (crStream)
                {
                    return await this.liveConnectClient.UploadAsync(path, dialog.SafeFileName, crStream, OverwriteOption.DoNotOverwrite);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        // Authorization
        Task IRefreshTokenHandler.SaveRefreshTokenAsync(RefreshTokenInfo tokenInfo)
        {
            // Note: 
            // 1) In order to receive refresh token, wl.offline_access scope is needed.
            // 2) Alternatively, we can persist the refresh token.
            return Task.Factory.StartNew(() =>
            {
                this.refreshTokenInfo = tokenInfo;
            });
        }
        Task<RefreshTokenInfo> IRefreshTokenHandler.RetrieveRefreshTokenAsync()
        {
            return Task.Factory.StartNew<RefreshTokenInfo>(() =>
            {
                return this.refreshTokenInfo;
            });
        }
        private async void onAuthCompleted(AuthResult authResult)
        {
            this.cleanupBrowser();
            if (authResult.AuthorizeCode != null)
            {
                try
                {
                    LiveConnectSession session = await this.AuthClient.ExchangeAuthCodeAsync(authResult.AuthorizeCode);
                    this.liveConnectClient = new LiveConnectClient(session);
                    LiveOperationResult meRes = await this.liveConnectClient.GetAsync("me");
                    dynamic meData = meRes.Result;
                    this.nameLabel.Text = meData.name;
                    LiveDownloadOperationResult meImgResult = await this.liveConnectClient.DownloadAsync("me/picture");
                    treeView1.Nodes[0].Name = "me/skydrive";

                    this.meImage.Image = Image.FromStream(meImgResult.Stream);
                    makeButtonsVisible();
                }
                catch (Exception ex)
                {
                    QueueBox.AppendText("Error (OnAuthCompleted) : \n" + ex.Message + "\n");
                }
                initializeTree();
            }
        }   // (Authorization code -> Token); Loads name, image, folders
        

        // Event handlers
        private async void DownloadButton_Click(object sender, System.EventArgs e)
        {
            TreeNode _node = null;
            string _temp = null;
            try
            {
                TreeNode curNode = treeView1.SelectedNode;
                _node = curNode;
                string address = curNode.Name;
                _temp = curNode.Text;
                string exec = "Downloading : " + curNode.Text + "\n";
                QueueBox.AppendText(exec);
                await this.DownloadFile(curNode.Name, curNode.Text);
                
                QueueBox.Text = QueueBox.Text.Replace(string.Concat(exec), "");
                CompletedBox.AppendText(exec);
                
            }
            catch (Exception ex)
            {
                QueueBox.Text = QueueBox.Text.Replace("Downloading : " + _node.Text + "\n", "");
                QueueBox.AppendText("Error (" + _temp+ ") : \n" + ex.Message + "\n");
            }
        }
        private async void UploadButton_Click(object sender, System.EventArgs e)
        {
            TreeNode curNode = treeView1.SelectedNode;
            string _temp = curNode.Text;
            try
            {
                if (curNode.Name.StartsWith("file"))
                    throw new Exception("incorrectNode");
                string exec = "Uploading in " + curNode.Text + "\n";
                QueueBox.AppendText(exec);
                await this.UploadFile(curNode.Name);

                exec = "Uploaded : " + uploadFileName.TrimStart('/') + "\n";
                QueueBox.Text = QueueBox.Text.Replace(string.Concat(exec), "");
                CompletedBox.AppendText(exec);
                updateTree(curNode);
                metaUpload(curNode, this.uploadFileName);
            }
            catch (Exception ex)
            {
                QueueBox.Text = QueueBox.Text.Replace("Uploading in " + curNode.Text + "\n", "");
                QueueBox.AppendText("Error (" + _temp + ") : \n" + ex.Message + "\n");
            }
        }
        private void SignInButton_Click(object sender, EventArgs e)
        {
            string startUri = this.AuthClient.GetLoginUrl(Scopes);
            string endUri = "https://login.live.com/oauth20_desktop.srf";
            this.myBrowser = new MyBrowser(startUri, endUri, this.onAuthCompleted);
            this.myBrowser.Show();
            this.myBrowser.MyBrowser_Load();
        }
        private void SignOutButton_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate(this.AuthClient.GetLogoutUrl());
            this.AuthClient = null;
            meImage.Image = Properties.Resources.noavatar;
            nameLabel.Text = "User";
            makeButtonsInvisible();
            treeView1.Nodes[0].Nodes.Clear();
        }
        private void Browse_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialog1.ShowDialog();
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
            catch (Exception ex)
            {
                QueueBox.AppendText("Error (Browse) : \n" + ex.Message + "\n");
            }
        }
        private async void Sync_Click(object sender, EventArgs e)
        {
            DownloadMyFiles.Enabled = false;
            try
            {
                drive rootDrive = new drive();
                TreeNode curNode = treeView1.SelectedNode;
                rootDrive.id = curNode.Name;
                DirectoryInfo dirInf = new DirectoryInfo(textBox2.Text);
                await FolderSync(dirInf, curNode);
                DownloadMyFiles.Enabled = true;
            }
            catch (Exception ex)
            {
                QueueBox.AppendText("Error (Sync) : \n" + ex.Message + "\n"); 
                DownloadMyFiles.Enabled = true;
            }
        }
        private async void Delete_Click(object sender, EventArgs e)
        {
            string _temp = null;
            try
            {
                TreeNode curNode = treeView1.SelectedNode;
                //if(curNode.Name.StartsWith("folder")) throw new Exception ("deletion folder");
                await this.liveConnectClient.DeleteAsync(curNode.Name);
                curNode.Remove();
                _temp = curNode.Text;
                CompletedBox.AppendText("Deleted : " + curNode.Text + "\n");
            }
            catch (Exception ex)
            {
                QueueBox.AppendText("Error (" + _temp + ") : \n" + ex.Message + "\n");
            }
        }
        private async void DownloadMyFiles_Click(object sender, EventArgs e)
        {
            Sync.Enabled = false;
            drive tempDrive = new drive();
            driveList tempList;
            drive _temp = null;
            tempDrive.id = treeView1.SelectedNode.Name;
            tempList = await GetFolderInfo(tempDrive);
            try
            {
                foreach (drive temp in tempList.data)
                {
                    _temp = temp;
                    if (temp.description == ODSecurity.guid)
                    {
                        QueueBox.AppendText("Downloading : " + temp.name + "\n");
                        await SilentDowloadFile(temp.id, temp.name);
                        QueueBox.Text = QueueBox.Text.Replace("Downloading : " + temp.name + "\n", "");
                        CompletedBox.AppendText("Downloaded : " + temp.name + "\n");
                    }
                }
                Sync.Enabled = true;
            }
            catch(Exception ex)
            {
                QueueBox.Text = QueueBox.Text.Replace("Downloading : " + _temp.name + "\n", "");
                Sync.Enabled = true;
                QueueBox.AppendText("Error (" + _temp.name+ ") : \n" + ex.Message + "\n");
            }
        }
        private void Main_Load(object sender, EventArgs e)
        {
            treeView1.Nodes[0].Text = "me/skydrive/";
            treeView1.Nodes[0].Name = "me/skydrive";
        }
        private async void CreateFolderButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != null)
                {
                    TreeNode curNode = treeView1.SelectedNode;
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    folderName temp = new folderName();
                    temp.name = textBox1.Text;
                    string res = ser.Serialize(temp);
                    // QueueBox.Text = res;
                    await this.liveConnectClient.PostAsync(curNode.Name, res);
                    curNode.Nodes.Clear();
                    drive tempDrive = new drive();
                    tempDrive.id = curNode.Name;
                    tempDrive.name = curNode.Text;
                    recursiveExplorer(curNode, tempDrive);
                }
            }
            catch (Exception ex)
            {
                QueueBox.AppendText("Error (CreateFolder) : \n" + ex.Message + "\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes[0].Nodes.Clear();
            initializeTree();
        }
   
    }
}
