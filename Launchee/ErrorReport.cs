using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Launchee
{
    public class ErrorReport
    {
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string AppVersion { get; set; }
        public string AppConfiguration { get; set; }
        public string OsVersion { get; set; }

        public string Source { get; set; }
        public bool Is64Bits { get; set; }

        public ErrorReport(Exception ex)
        {
            TimeStamp = DateTime.UtcNow;
            Message = ex.Message;
            StackTrace = ex.StackTrace;
            Source = ex.Source;
            AppVersion = Assembly.GetAssembly(typeof (ErrorReport)).GetName().Version.ToString();
            OsVersion = Environment.OSVersion.VersionString;
            Is64Bits = Environment.Is64BitOperatingSystem;

            try
            {
                if (File.Exists("jsontest.json"))
                {
                    AppConfiguration = File.ReadAllText("jsontest.json");
                }
            }
            catch (Exception)
            {
                // swallow   
            }
        }


        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
