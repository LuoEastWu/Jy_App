using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Java.Net;
using Common;
using System.Json;

namespace jy_App
{
    [Activity(Label = "SettingActivity")]
    public class SettingActivity : Activity
    {
        private Context mContext;
        private EditText ed_enter;
        private EditText ed_port;
        private Button save;
        private Button btn_wifi;
        private Button btn_getNewVersion;
        private Button btn_exit;
        private int name;
        private String url_down;
        private Button btn_server_select;
        private Button test;
        private ProgressDialog progessReporter;

      

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Setting);
            initView();
        }
        /// <summary>
        /// 初始化界面控件
        /// </summary>
        private void initView()
        {
            btn_wifi = (Button)FindViewById(Resource.Id.btn_wifi);
            btn_getNewVersion = (Button)FindViewById(Resource.Id.btn_getNewVersion);
            btn_exit = (Button)FindViewById(Resource.Id.btn_exit);
            btn_server_select = (Button)FindViewById(Resource.Id.btn_server_select);
            test = (Button)FindViewById(Resource.Id.test);
            btn_wifi.Click += new System.EventHandler(this.Button_Click);
            btn_getNewVersion.Click += new System.EventHandler(this.Button_Click);
            btn_exit.Click += new System.EventHandler(this.Button_Click);
            btn_server_select.Click += new System.EventHandler(this.Button_Click);
            test.Click += new System.EventHandler(this.Button_Click);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (sender == btn_wifi)
            {
                Intent intent = new Intent(Android.Provider.Settings.ActionWifiSettings);
                StartActivity(intent);
            }
            else if (sender == btn_getNewVersion)
            {
                GetRequestByHWR();
            }
            else if (sender == btn_exit)
            {

            }
            else if (sender == btn_server_select)
            {

            }
            else if (sender == test)
            {

            }
        }

        private async void GetRequestByHWR()
        {
            string url = "http://47.90.48.6:8888/pda/GetVosionNo";
            IDictionary<string, string> routeParames = new Dictionary<string, string>();
            var result = await HttpHelp.SendGetHttpRequestBaseOnHttpWebRequest(url, routeParames);

            var data = Common.DataWorking.JsonToObject<Models.Version>(HttpHelp.HttpGet(url, ""));
            if (data.EJEAndroidPDA.Count > 0)
            {

            }
            
        }


        public async void run(string urlToDownload)
        {
            //String target = Environment.getExternalStorageDirectory()
            //            + "/JYandroidpda.apk";
            int receivedBytes = 0;
            int totalBytes = 0;
            string dirPath = Android.OS.Environment.ExternalStorageDirectory + "";
            var filePath = Path.Combine(dirPath, "hz_android.apk");
            URL url = new URL(urlToDownload);//urlToDownload 下载文件的url地址
            HttpURLConnection conn = (HttpURLConnection)url.OpenConnection();
            conn.Connect();
            Stream Ins = conn.InputStream;
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
            using (FileStream fos = new FileStream(filePath, FileMode.Create))
            {
                byte[] buf = new byte[512];

                do
                {
                    int numread = Ins.Read(buf, 0, 512);
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

            //调用下载的文件进行安装
            installApk(filePath);
        }

        private void installApk(string filePath)
        {
           Htt
            if (context == null)
                return;
            // 通过Intent安装APK文件
            Intent intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(Uri.Parse("file://" + filePath), "application/vnd.android.package-archive");
            //Uri content_url = Uri.Parse(filePath);
            //intent.SetData(content_url);
            intent.SetFlags(ActivityFlags.NewTask);
            context.StartActivity(intent);
        }
    }

}