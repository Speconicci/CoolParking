using System;
using System.IO;
using CoolParking.Interfaces;

namespace CoolParking.Services
{
    public class LogService : ILogService
    {
        private string logPath;
        public string LogPath => logPath;

        public LogService(string logPath)
        {
            this.logPath = logPath;
        }

        public string Read()
        {
            string readedLogInfo = null;
            using (StreamReader streamReader = new StreamReader(logPath))
            {
                if (streamReader == null) throw new InvalidOperationException();
                readedLogInfo = streamReader.ReadToEnd();
                streamReader.Close();
            }
            return readedLogInfo;
        }

        public void Write(string logInfo)
        {
            using StreamWriter streamWriter = new StreamWriter(logPath, true);
            streamWriter.WriteLine(logInfo);
            streamWriter.Close();
        }
    }
}
