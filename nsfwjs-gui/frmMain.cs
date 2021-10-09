using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nsfwjs_gui
{
    public partial class frmMain : Form
    {
        private ContextMenuStrip _cms;
        public frmMain()
        {
            InitializeComponent();
            nudOutputLimit.Enabled = false;

            lvwSource.SmallImageList = new ImageList();
            lvwSource.SmallImageList.ImageSize = new Size(16, 16);
            lvwSource.SmallImageList.Images.Add("unknown", Properties.Resources.unknown);
            lvwSource.SmallImageList.Images.Add("good", Properties.Resources.good);
            lvwSource.SmallImageList.Images.Add("bad", Properties.Resources.bad);
            lvwSource.View = View.Details;
            lvwSource.MultiSelect = true;
            lvwSource.Columns.Add("Source");
            lvwSource.Columns[0].Width = lvwSource.Width;

            var mnuFile = new ToolStripMenuItem("File");
            mnuFile.Click += (s, e) =>
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.Multiselect = true;
                    ofd.Filter = "Video File(*.mp4;*.mkv;*.avi)|*.mp4;*.mkv;*.avi";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        foreach (string filename in ofd.FileNames)
                        {
                            var newItem = new ListViewItem(filename, "unknown");
                            lvwSource.Items.Add(newItem);
                        }
                        lvwSource.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    }
                }
            };
            var mnuUrl = new ToolStripMenuItem("Url");
            mnuUrl.Click += (s, e) =>
            {
                string url = Microsoft.VisualBasic.Interaction.InputBox("Url", "Input url", "");
                if (!string.IsNullOrEmpty(url))
                {
                    var newItem = new ListViewItem(url, "unknown");
                    lvwSource.Items.Add(newItem);
                    lvwSource.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            };

            _cms = new ContextMenuStrip();
            _cms.Items.AddRange(new[]{ mnuFile, mnuUrl});
        }

        private CancellationTokenSource _cts;

        private void btnStart_Click(object sender, EventArgs e)
        {
            string args = "";
            if (rbtLocal.Checked)
            {
                startLocalRestServer();
                args += $"--webapi={Properties.Settings.Default.LocalUrl}";
            }
            else
                args += $"--webapi={Properties.Settings.Default.RemoteUrl}";

            if (chkOutputLimit.Checked && nudOutputLimit.Value > 0)
                args += $"--limit={nudOutputLimit.Value} ";

            if (txtOutputDirectory.TextLength > 0 && System.IO.Directory.Exists(txtOutputDirectory.Text))
                args += $"--output={txtOutputDirectory.Text}";

            foreach (ListViewItem item in lvwSource.Items)
            {
                var p = new Process();
                p.StartInfo.FileName = "nsfwjs-ffmpeg";
                p.StartInfo.Arguments = $"--src={item.Text} {args}";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();

                Task t = new(() =>
                {
                    while (!p.StandardOutput.EndOfStream && !_cts.IsCancellationRequested)
                    {
                        Console.WriteLine(p.StandardOutput.ReadLine());
                    }
                }, _cts.Token);
                t.Start();
                p.WaitForExit();
                item.ImageKey = p.ExitCode > 0 ? "good" : "bad";
            }
        }

        private void startLocalRestServer()
        {
            Process p = null;

            //test if nsfwjs-rest already run 
            _cts = new CancellationTokenSource();
            var client = new RestSharp.RestClient();
            var test = new RestSharp.RestRequest(RestSharp.Method.GET);
            System.Net.HttpStatusCode statusCode = client.Execute(test).StatusCode;
            if (statusCode != System.Net.HttpStatusCode.NotFound)
            {
                lstLog.Items.Add("start nsfwjs rest server...");
                p = new Process();
                p.StartInfo.FileName = "node";
                p.StartInfo.Arguments = Properties.Settings.Default.LocalArguments;
                p.StartInfo.WorkingDirectory = Properties.Settings.Default.LocalWorkingDirectory;
                //p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.UseShellExecute = false;
                //p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();

                Task t = new(() =>
                {
                    while (!p.StandardOutput.EndOfStream && !_cts.IsCancellationRequested)
                        lstLog.Items.Add(p.StandardOutput.ReadLine());
                    p.Kill();
                }, _cts.Token);
                t.Start();

                //waiting for nsfwjs rest server
                test = new RestSharp.RestRequest(RestSharp.Method.GET);
                while (client.Execute(test).StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Waiting for nsfwjs server starting...");
                    Thread.Sleep(1000);
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var p = this.PointToScreen(btnAdd.Location);
            _cms.Show(new Point(p.X, 
                                p.Y + btnAdd.Height));
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvwSource.SelectedItems.Count > 0)
            {
                var removeList = new List<ListViewItem>();
                foreach (ListViewItem item in lvwSource.SelectedItems)
                    removeList.Add(item);

                foreach (ListViewItem item in removeList)
                    lvwSource.Items.Remove(item);
            }
        }

        private void btnOutputDirectory_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (System.IO.Directory.Exists(txtOutputDirectory.Text))
                    fbd.SelectedPath = txtOutputDirectory.Text;
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtOutputDirectory.Text = fbd.SelectedPath; 
                }    
            }
        }

        private void chkOutputLimit_CheckedChanged(object sender, EventArgs e)
        {
            nudOutputLimit.Enabled = chkOutputLimit.Checked; 
        }
    }
}
