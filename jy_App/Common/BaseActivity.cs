using Android.App;
using Android.Content.PM;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class BaseActivity : Activity
    {
        /// <summary>
        /// 获取版本名称
        /// </summary>
        /// <returns></returns>
        public String GetVersionName(string packName)
        {
            try
            {
                PackageInfo packageInfo = PackageManager.GetPackageInfo(packName, 0);
                int versionCode = packageInfo.VersionCode;
                String versionName = packageInfo.VersionName;
                Toast.MakeText(this, "versionCode" + versionCode + "versionName" + versionName, ToastLength.Short).Show();
                return versionName;
            }
            catch (PackageManager.NameNotFoundException e)
            {
                e.PrintStackTrace();
            }
            return "";
        }
    }
}
