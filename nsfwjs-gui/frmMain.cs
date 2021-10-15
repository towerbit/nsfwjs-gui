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
using System.IO;
using Microsoft.VisualBasic;

namespace nsfwjs_gui
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.EnableNotifyMessage, true);
            
            gbxSettings.Enabled = true;
            btnStart.Enabled = true;
            btnAdd.Enabled = true;
            btnRemove.Enabled = true;
            btnStop.Enabled = false;

            cboHost.Text = Properties.Settings.Default.Host;
            txtOutputDirectory.Text = Properties.Settings.Default.OutputDirectory;
            nudSkipLimit.Value = Properties.Settings.Default.SkipLimit;
            nudProbability.Value = Properties.Settings.Default.MinProbability;

            initLvwSource();
            initCmsAdd();
            initHWDeviceType();
        }

        protected override CreateParams CreateParams
        { 
            get 
            {
                //return base.CreateParams; 
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        protected override void OnNotifyMessage(Message m)
        {
            if(m.Msg!=0x14)
                base.OnNotifyMessage(m);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            Properties.Settings.Default.Host = cboHost.Text;
            Properties.Settings.Default.OutputDirectory = txtOutputDirectory.Text;
            Properties.Settings.Default.SkipLimit= (int)nudSkipLimit.Value;
            Properties.Settings.Default.MinProbability= (int)nudProbability.Value;
            Properties.Settings.Default.Save();

            _cts?.Cancel();
        }
        private ContextMenuStrip _cmsSource;
        private void initLvwSource()
        {
            lvwSource.SmallImageList = new ImageList();
            lvwSource.SmallImageList.ImageSize = new Size(16, 16);
            lvwSource.SmallImageList.Images.Add("unknown", Properties.Resources.unknown);
            lvwSource.SmallImageList.Images.Add("good", Properties.Resources.good);
            lvwSource.SmallImageList.Images.Add("bad", Properties.Resources.bad);
            lvwSource.View = View.Details;
            lvwSource.MultiSelect = true;
            lvwSource.Columns.Add("Source");
            lvwSource.Columns[0].Width = lvwSource.Width;

            var mnuSource = new ToolStripMenuItem("Source");
            mnuSource.Click += (s, e) =>
            {
                if (lvwSource.SelectedItems.Count > 0 && !lvwSource.SelectedItems[0].Text.Contains("://"))
                {
                    //定位到源文件
                    explorerOpen(lvwSource.SelectedItems[0].Text, true);
                }
            };

            var mnuOutput = new ToolStripMenuItem("Output");
            mnuOutput.Click += (s, e) =>
            {
                if (lvwSource.SelectedItems.Count > 0)
                {
                    //定位到输出目录
                    var outputFolder = Path.Combine(txtOutputDirectory.Text,
                                                    validFilename(lvwSource.SelectedItems[0].Text));
                    explorerOpen(outputFolder);
                }
            };
            _cmsSource = new ContextMenuStrip();
            _cmsSource.Items.AddRange(new[] { mnuSource, mnuOutput });
        }

        private ContextMenuStrip _cmsAdd;
        private void initCmsAdd()
        {
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

            _cmsAdd = new ContextMenuStrip();
            _cmsAdd.Items.AddRange(new[] { mnuFile, mnuUrl });
        }

        private void initHWDeviceType()
        {
            var types= Properties.Settings.Default.HWDeviceType.Split(",");
            cboHWDeviceType.Items.Clear();
            cboHWDeviceType.Items.Add("NONE");
            cboHWDeviceType.Items.AddRange(types);
            cboHWDeviceType.SelectedIndex = 0;
        }

        private CancellationTokenSource _cts;

        private void btnStart_Click(object sender, EventArgs e)
        {
            lstLog.Items.Clear();
            foreach (ListViewItem item in lvwSource.Items)
                item.ForeColor = Color.Black;
               
            StringBuilder  argsb = new StringBuilder();
            if (cboHost.Text=="127.0.0.1" || cboHost.Text.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                argsb.Append($" --host=localhost");
            else
                argsb.Append($" --host={cboHost.Text}");

            if (nudSkipLimit.Value > 0)
                argsb.Append($" --skip={nudSkipLimit.Value} ");

            if (txtOutputDirectory.TextLength > 0 && Directory.Exists(txtOutputDirectory.Text))
                argsb.Append($" --output={txtOutputDirectory.Text}");

            argsb.Append($" --prob={nudProbability.Value / 100}");

            argsb.Append($" --gpu={cboHWDeviceType.SelectedIndex}");

            if (txtOutputDirectory.TextLength > 0 && !Directory.Exists(txtOutputDirectory.Text))
                try
                {
                    Directory.CreateDirectory(txtOutputDirectory.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

            btnStart.Enabled = false;
            gbxSettings.Enabled = false;
            btnAdd.Enabled = false;
            btnRemove.Enabled = false;

            btnStop.Enabled = true;

            _cts = new CancellationTokenSource();
            foreach (ListViewItem item in lvwSource.Items)
            {
                item.ForeColor = Color.Red;

                var p = new Process();
#if DEBUG
                p.StartInfo.FileName = @"../net5.0/nsfwjs-ffmpeg.exe";
                p.StartInfo.WorkingDirectory = @"../net5.0/";
#else
                p.StartInfo.FileName = "nsfwjs-ffmpeg.exe";
                p.StartInfo.WorkingDirectory = Application.StartupPath;
#endif
                p.StartInfo.Arguments = $"--src=\"{item.Text}\" {argsb.ToString()}";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();

                Task t = new(() =>
                {
                    while (!p.StandardOutput.EndOfStream && !_cts.IsCancellationRequested)
                    {
                        this.Invoke(new Action(() =>
                        {
                            var idx=lstLog.Items.Add(p.StandardOutput.ReadLine());
                            lstLog.TopIndex++; 
                        }));
                    }
                    p.Kill(); //user canceled
                    p.Dispose();
                    p = null;
                }, _cts.Token);
                t.Start();

                while (null!=p && !p.HasExited)
                { 
                    Application.DoEvents();
                    if (_cts.IsCancellationRequested)
                        break;
                }

                if (_cts.IsCancellationRequested)
                    break;

                var exitCode = null!=p? p.ExitCode:0;
                lstLog.Items.Add($"nsfwjs-ffmpeg exit with {exitCode}");
                if(exitCode >= 0)
                    item.ImageKey = exitCode > nudSkipLimit.Value ? "bad": "good";
                item.ForeColor = Color.Black;
            }

            btnStart.Enabled = true;
            gbxSettings.Enabled = true;
            btnAdd.Enabled = true;
            btnRemove.Enabled = true;

            btnStop.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var p = this.PointToScreen(btnAdd.Location);
            _cmsAdd.Show(new Point(p.X,
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
                if (Directory.Exists(txtOutputDirectory.Text))
                    fbd.SelectedPath = txtOutputDirectory.Text;
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtOutputDirectory.Text = fbd.SelectedPath;
                }
            }
        }

        private void lvwSource_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var currSelected = lvwSource.HitTest(e.Location);
                if (currSelected.Item != null)
                {
                    lvwSource.SelectedItems.Clear();
                    currSelected.Item.Selected = true;
                    //打开选中项对应的输出目录
                    var outputFolder = Path.Combine(txtOutputDirectory.Text, validFilename(currSelected.Item.Text));
                    Debug.Print($"CurrSelected outputFolder:{outputFolder}");
                    explorerOpen(outputFolder);
                }
            }
        }

        private static void explorerOpen(string fileOrDir, bool selected = false)
        {
            var p = new Process();
            p.StartInfo.FileName = "explorer";
            p.StartInfo.Arguments = $"{(selected?"/select,":"")}{fileOrDir}";
            p.Start();
        }

        private static string validFilename(string src)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                src = src.Replace(c, '_');
            //foreach (char c in Path.GetInvalidPathChars())
            //    src=src.Replace(c, '_');
            return src;
        }

        private void lvwSource_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var currSelected = lvwSource.HitTest(e.Location);
                if (currSelected.Item != null)
                {
                    lvwSource.SelectedItems.Clear();
                    currSelected.Item.Selected = true;
                    lvwSource.ContextMenuStrip = _cmsSource;
                }
                else
                    lvwSource.ContextMenuStrip = null; //仅选中项目才显示右键菜单
            }
        }
    }
}
