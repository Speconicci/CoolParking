using System;
using System.IO;
using CoolParking.Interfaces;

namespace CoolParking.Services
{
    public class LogService : ILogService
    {
        private string logPath;
        public string LogPath
        {
            get => logPath;
            private set
            {
                if (value != null) logPath = value;
                else throw new ArgumentNullException();
            }
        }

        public LogService(string logPath)
        {
            this.LogPath = logPath;
        }

        public string Read()
        {
            string readedLogInfo = null;
            try
            {
                using (StreamReader streamReader = new StreamReader(logPath))
                {
                    if (streamReader == null) throw new InvalidOperationException();
                    readedLogInfo = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("File read error");
                Console.WriteLine(e.Message);
            }
            return readedLogInfo;
        }

        public void Write(string logInfo)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(logPath, true))
                {
                    streamWriter.WriteLine(logInfo);
                    streamWriter.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("File write error");
                Console.WriteLine(e.Message);
            }
        }
    }
}
