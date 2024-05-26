using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TimeLog
{
    public class TimeLogger
    {
        private string rootdir;
        private string logname;
        private FileInfo fi;
        private StreamWriter sw;

        public TimeLogger(string rootdir, string logname)
        {
            this.rootdir = rootdir;
            this.logname = logname;
        }

        public List<string> LoadLog()
        {
            List<string> previousTasks = new List<string>();
            if (Directory.Exists(rootdir))
            {
                fi = new FileInfo(Path.Combine(rootdir, logname));
                string content = "";
                string content1 = "";
                int i = 1;

                if (fi.Exists)
                {
                    content = File.ReadAllText(fi.FullName);
                    content = content.Substring(content.LastIndexOf("***TimeLog"));
                    content = content.Substring(content.IndexOf("-->") + 3).Trim();

                    while (content.Contains("-->"))
                    {
                        content1 = content.Substring(0, content.IndexOf("==>")).Trim();
                        previousTasks.Add(content1);


                        content = content.Substring(content.IndexOf("-->") + 3).Trim();

                        if (!content.Contains("==>")) break;
                    }
                    if (content.Contains("==>"))
                    {
                        content1 = content.Substring(0, content.IndexOf("==>")).Trim();
                        previousTasks.Add(content1);
                    }
                }

            }
            return previousTasks;
        }

        public void CreateLog(string mes)
        {
            if (!Directory.Exists(rootdir))
            {
                Directory.CreateDirectory(rootdir);
            }
            fi = new FileInfo(Path.Combine(rootdir, logname));

            if (fi.Exists)
            {
                if (fi.Length < 1000000)
                {
                    try
                    {
                        sw = fi.AppendText();
                    }
                    catch
                    {
                        fi = new FileInfo(Path.Combine(rootdir, logname + "locked." + dtmask()));
                        sw = fi.CreateText();
                    }
                }
                else
                {
                    try
                    {
                        fi.CopyTo(Path.Combine(rootdir, logname + "." + dtmask()));
                        fi.Delete();
                        sw = fi.CreateText();
                    }
                    catch
                    {
                        fi = new FileInfo(Path.Combine(rootdir, logname + "locked1." + dtmask()));
                        sw = fi.CreateText();
                    }
                }
            }
            else
            {
                sw = fi.CreateText();
            }
            sw.WriteLine(dtmask() + " --> " + mes);
            CloseLog();
        }

        public void CloseLog()
        {
            sw.Flush();
            sw.Close();
        }

        protected string dtmask()
        {
            string dtmask = DateTime.Now.ToString("yyMMddHHmmss");
            return dtmask;
        }

        public void Log(string mes)
        {
            CreateLog(mes);
        }
    }
}
