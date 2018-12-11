using System;
using System.IO;

namespace UC.CSP.MeetingCenter.BL.Logging
{
    public class FileLogger
    {
        // TODO: Move to config
        private readonly string filePath = "log.txt";
        public static FileLogger Instance { get; } = new FileLogger();
        private FileLogger()
        {

        }
        
        public void Log(string message)
        {
            using (var sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine($"{DateTime.Now}: {message}");
            }
        }
    }
}