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

            var t = from l in LogRecordCollection(@"E:\Application.evtx")
                    where l.TimeCreated > new DateTime(2011, 03, 01)
                    orderby l.RecordId descending
                    select l;
            foreach (var tt in t)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("ID : " + tt.Id ?? "");
                Console.WriteLine("Activity ID : " + tt.ActivityId ?? "");
                Console.WriteLine("Container Log : " + tt.ContainerLog ?? "");
                Console.WriteLine("Provider Name : " + tt.ProviderName ?? "");
                Console.WriteLine("Machine Name : " + tt.MachineName ?? "");
                Console.WriteLine("Log Name: " + tt.LogName);
                Console.WriteLine("==================================\n\n");
                //               Console.WriteLine("XML : " + tt.ToXml());
            }
            while (true) ;
        
        instance = new WindowEventLogger(xmlPath);
            new Thread(new ThreadStart(() =>
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            })).Start();
            return instance;
        }


            static IEnumerable<EventLogRecord> LogRecordCollection(string filename, string xpathquery = "*")
            {
                var eventLogQuery = new EventLogQuery(filename, PathType.FilePath, xpathquery);

                using (var eventLogReader = new EventLogReader(eventLogQuery))
                {
                    EventLogRecord eventLogRecord;

                    while ((eventLogRecord = (EventLogRecord)eventLogReader.ReadEvent()) != null)
                        yield return eventLogRecord;
                }
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
