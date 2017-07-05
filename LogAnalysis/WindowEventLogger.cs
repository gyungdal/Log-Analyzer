using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Diagnostics.Eventing.Reader;
namespace LogAnalysis
{
    class WindowEventLogger
    {
        private static WindowEventLogger instance;
        private List<EventRecord> Records;
        public static WindowEventLogger GetInstance(string xmlPath)
        {
            if (instance == null)
                instance = new WindowEventLogger(xmlPath);
            return instance;
        }

        public WindowEventLogger()
        {

        }

        public static WindowEventLogger NewInstance(string xmlPath)
        {
            instance = new WindowEventLogger(xmlPath);
            new Thread(new ThreadStart(() =>
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            })).Start();
            return instance;
        }

        public WindowEventLogger(string xmlPath)
        {
            Records = new List<EventRecord>();
            using (var reader = new EventLogReader(xmlPath, PathType.FilePath))
            {
                EventRecord record;
                while ((record = reader.ReadEvent()) != null)
                {
                    Records.Add(record);
                    using (record)
                    {
                        Console.WriteLine("{0} {1}: {2}", record.TimeCreated, record.LevelDisplayName, record.FormatDescription());
                    }
                }
            }
        }
    }
}
