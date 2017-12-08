using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Util;
using Android.Widget;
using Java.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BaseActivity : Activity
    {
        /// <summary>
        /// 获取版本名称
        /// </summary>
        /// <returns></returns>
        public String GetVersionName(string packName, PackageManager packageManager)
        {
            try
            {
                PackageInfo packageInfo = packageManager.GetPackageInfo(packName, 0);
                int versionCode = packageInfo.VersionCode;
                String versionName = packageInfo.VersionName;
                return versionName;
            }
            catch (PackageManager.NameNotFoundException e)
            {
                e.PrintStackTrace();
            }
            return "";
        }
        /// <summary>
        /// 进度条
        /// </summary>
        /// <param name="urlToDownload"></param>
        /// <param name="progessReporter"></param>
        /// <returns></returns>
        public static async Task<int> CreateDownloadTask(string urlToDownload, IProgress<DownloadBytesProgress> progessReporter)
        {
            int receivedBytes = 0;
            int totalBytes = 0;
            WebClient client = new WebClient();

            using (var stream = await client.OpenReadTaskAsync(urlToDownload))
            {
                byte[] buffer = new byte[4096];
                totalBytes = Int32.Parse(client.ResponseHeaders[HttpResponseHeader.ContentLength]);

                for (; ; )
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        await Task.Yield();
                        break;
                    }

                    receivedBytes += bytesRead;
                    if (progessReporter != null)
                    {
                        DownloadBytesProgress args = new DownloadBytesProgress(urlToDownload, receivedBytes, totalBytes);
                        progessReporter.Report(args);
                    }
                }
            }
            return receivedBytes;
        }
        /// <summary>
        /// 进度条
        /// </summary>
        public class DownloadBytesProgress
        {
            public DownloadBytesProgress(string fileName, int bytesReceived, long totalBytes)
            {
                Filename = fileName;
                BytesReceived = bytesReceived;
                TotalBytes = totalBytes;
            }

            public long TotalBytes { get; private set; }

            public int BytesReceived { get; private set; }

            public float PercentComplete { get { return (float)BytesReceived / TotalBytes; } }

            public string Filename { get; private set; }

            public bool IsFinished { get { return BytesReceived == TotalBytes; } }
        }

      
        public void run(string urlToDownload, IProgress<DownloadBytesProgress> progessReporter)
        {

            int receivedBytes = 0;
            int totalBytes = 0;

            string dirPath = Android.OS.Environment.ExternalStorageDirectory.ToString();
            var filePath = Path.Combine(dirPath, "hz_android.apk");
            URL url = new URL(urlToDownload);//urlToDownload 下载文件的url地址
            HttpURLConnection conn = (HttpURLConnection)url.OpenConnection();
            conn.Connect();
            //WebClient client = new WebClient();
            Stream Ins = conn.InputStream;
            //Stream Ins = client.OpenRead(urlToDownload);
            totalBytes = conn.ContentLength;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            else
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            try
            {
              
                using (FileStream fos = new FileStream(filePath, FileMode.Create))
                {
                    byte[] buf = new byte[2048];

                    do
                    {
                        int numread = Ins.Read(buf, 0, 2048);
                        receivedBytes += numread;
                        if (numread <= 0)
                        {
                            break;
                        }
                        fos.Write(buf, 0, numread);

                        //进度条代码
                        if (progessReporter != null)
                        {
                            DownloadBytesProgress args = new DownloadBytesProgress(urlToDownload, receivedBytes, totalBytes);
                            progessReporter.Report(args);
                        }
                    } while (true);
                }
            }
            catch (Exception ex)
            {

            }



        }
        /// <summary>
        /// 下载的文件进行安装
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="context"></param>
        public void installApk(string filePath, ContextWrapper context)
        {

            if (context == null)
                return;
            // 通过Intent安装APK文件
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(Android.Net.Uri.Parse("file://" + filePath), "application/vnd.android.package-archive");
            //Uri content_url = Uri.Parse(filePath);
            //intent.SetData(content_url);
            intent.SetFlags(ActivityFlags.NewTask);
            context.StartActivity(intent);
        }
    }
}
