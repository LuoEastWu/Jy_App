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
using Android.Content.PM;

namespace jy_App
{
    [Activity(Label = "LoginActivity", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        private TextView tv_version;
        private EditText edi_username;
        private EditText edi_password;
        private Button btn_login;
        private Button btn_setting;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Login);
            initView();

            
        }
        /// <summary>
        /// 初始化界面控件
        /// </summary>
        private void initView()
        {
            tv_version = (TextView)FindViewById(Resource.Id.tv_version);
            edi_username  = (EditText)FindViewById(Resource.Id.edi_username);
            edi_password = (EditText)FindViewById(Resource.Id.edi_password);
            btn_login = (Button)FindViewById(Resource.Id.btn_login);
            btn_setting = (Button)FindViewById(Resource.Id.btn_setting);
            tv_version.Text = "版本号：" + new Common.BaseActivity().GetVersionName(PackageName);
            btn_setting.Click += Btn_setting_Click;
        }

        private void Btn_setting_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this,typeof(SettingActivity));
            StartActivity(intent);
        }

        

    }
}