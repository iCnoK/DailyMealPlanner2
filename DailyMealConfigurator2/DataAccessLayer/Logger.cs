using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccessLayer
{
    public class Logger
    {
        public string LoggerFile { get; private set; }

        private StreamWriter StreamWriter { get; set; }

        public Logger(string file = null)
        {
            if (file == null)
            {
                LoggerFile = Environment.CurrentDirectory + "\\" + "log.txt";
            }
            if (!string.IsNullOrEmpty(file))
            {
                LoggerFile = file;
                if (!File.Exists(LoggerFile))
                {
                    StreamWriter = new StreamWriter(LoggerFile, true);
                    LogInformation($"DailyMealPlanner (c)\n" +
                        $"Log created at {DateTime.Now}");
                    return;
                }
                StreamWriter = new StreamWriter(LoggerFile, true);
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public void LogInformation(string text)
        {
            StreamWriter.WriteLine($"{DateTime.Now} : {text}");
        }

        public void Close()
        {
            StreamWriter.Close();
        }

        public static void LogInformation(string file, string text, bool dateTime = false)
        {
            if (!string.IsNullOrEmpty(file))
            {
                if (!File.Exists(file))
                {
                    File.Create(file);
                }
                StreamWriter writer = new StreamWriter(file, true);

                if (dateTime)
                {
                    writer.WriteLineAsync($"{DateTime.Now} : {text}");
                }
                else
                {
                    writer.WriteLineAsync(text);
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
