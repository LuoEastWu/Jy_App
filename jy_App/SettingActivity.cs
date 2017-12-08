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

    }

}