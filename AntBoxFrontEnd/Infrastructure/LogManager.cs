using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AntBoxFrontEnd.Infrastructure
{
    public static class LogManager
    {
        public static readonly string Error = "Error";
        public static readonly string Login = "Login";

        public static void Write(string log, string type)
        {
            /*var pre = DateTime.Now.ToString("yyyyMM");
            //var filename = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + "log\\" + pre+"_log"+ type +".txt";
            var filename = "C:\\" + "log\\" + pre + "_log" + type + ".txt";

            FileInfo fileInfo = new FileInfo(filename);
            if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();

            


            if (!File.Exists(filename))
                File.Create(filename).Dispose();

            var sw = new System.IO.StreamWriter(filename, true);
            sw.WriteLine(DateTime.Now.ToString() + " "  + log);
            sw.Close();*/
        }




    }

    public static class LogType
    {
        public static readonly string Error = "Error";
        public static readonly string Login = "Login";
    }
}