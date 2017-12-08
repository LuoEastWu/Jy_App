using System;
using System.Collections.Generic;
using System.Text;

namespace Sqlite
{
    public class Config
    {
        public static string ConnectionString = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ormdemo.db");
    }
}
