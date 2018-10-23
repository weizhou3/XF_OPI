using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace XF_OPI
{
    public partial class FTPForm : Form
    {
        public FTPForm(string FileName, bool Upload)
        {
            InitializeComponent();
            FtpS.FileName = FileName;
            FtpS.Upload = Upload;
        }

        public string result { get; set; }
        OmronFINsClass Omron = new OmronFINsClass();
        OmronFINsClass.PLCcommand PLCcmd = new OmronFINsClass.PLCcommand();
        DataIO DIO = new DataIO();
        DataIO.FtpSetting FtpS = new DataIO.FtpSetting();
       
        private void ExportMUBA_Load(object sender, EventArgs e)
        {
            if (!bgwExport.IsBusy)
            {
                bgwExport.RunWorkerAsync();
            }
        }
        
        private void bgwExport_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //1. get FTP settings
                string[] FtpInfo;
                FtpInfo = DIO.ReadFromFile("FtpInfo.txt");
                FtpS.Localdest = @"D:\XFFTPfile\Download\"+@""+FtpS.FileName;
                FtpS.Server = FtpInfo[0];
                FtpS.UserName = FtpInfo[1];
                FtpS.Password = FtpInfo[2];


                if (FtpS.Upload)
                {
                    //2. send file to destination
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", FtpS.Server, FtpS.FileName)));
                    request.Method = WebRequestMethods.Ftp.UploadFile;//upload method
                    request.Credentials = new NetworkCredential(FtpS.UserName, FtpS.Password);
                    Stream FtpStream = request.GetRequestStream();
                    FileStream fs = File.OpenRead(FtpS.FileName);

                    byte[] buffer = new byte[1024];
                    double total = (double)fs.Length;
                    int ByteRead = 0;
                    double read = 0;
                    do
                    {
                        if (!bgwExport.CancellationPending)
                        {
                            ByteRead = fs.Read(buffer, 0, 1024);
                            FtpStream.Write(buffer, 0, ByteRead);
                            read += (double)ByteRead;
                            double percentage = read / total * 100;
                            bgwExport.ReportProgress((int)percentage);
                        }else if (bgwExport.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                    } while (ByteRead != 0);
                    fs.Close();
                    FtpStream.Close();
                }
                else
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", FtpS.Server, FtpS.FileName)));
                    request.Method = WebRequestMethods.Ftp.DownloadFile;//download method
                    request.Credentials = new NetworkCredential(FtpS.UserName, FtpS.Password);

                    FtpWebRequest request1 = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", FtpS.Server, FtpS.FileName)));
                    request1.Method = WebRequestMethods.Ftp.GetFileSize;//GetFileSize method
                    request1.Credentials = new NetworkCredential(FtpS.UserName, FtpS.Password);
                    FtpWebResponse response1 = (FtpWebResponse)request1.GetResponse();
                    double total = response1.ContentLength;
                    response1.Close();

                    FtpWebRequest request2 = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", FtpS.Server, FtpS.FileName)));
                    request2.Method = WebRequestMethods.Ftp.GetDateTimestamp;//GetDateTimestamp method
                    request2.Credentials = new NetworkCredential(FtpS.UserName, FtpS.Password);
                    FtpWebResponse response2 = (FtpWebResponse)request2.GetResponse();
                    DateTime modify = response2.LastModified;
                    response2.Close();

                    Stream FtpStream = request.GetResponse().GetResponseStream();
                    FileStream fs = new FileStream(FtpS.Localdest,FileMode.Create);

                    byte[] buffer = new byte[1024];
                    //double total = (double)fs.Length;
                    int ByteRead = 0;
                    double read = 0;
                    do
                    {
                        if (!bgwExport.CancellationPending)
                        {
                            ByteRead = FtpStream.Read(buffer, 0, 1024);
                            fs.Write(buffer, 0, ByteRead);
                            read += (double)ByteRead;
                            double percentage = read / total * 100;
                            Thread.Sleep(1000);
                            bgwExport.ReportProgress((int)percentage);
                        }
                        else if (bgwExport.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                    } while (ByteRead != 0);
                    fs.Close();
                    FtpStream.Close();
                }
            }
            catch (Exception)
            {
                bgwExport.CancelAsync();
            }
        }

        private void bgwExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblUpload.Text = $"Processing {e.ProgressPercentage} %";
            progress.Value = e.ProgressPercentage;
            progress.Update();
        }

        private void bgwExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Cancelled)
                {
                    MessageBox.Show("Process has been cancelled.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if (!e.Cancelled)
                {
                    MessageBox.Show("Process has been completed.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }

            }
            else
            {
                MessageBox.Show(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }
    }
}
