using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;

namespace Test_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //var t = from l in LogRecordCollection("e:\\evt.evtx")
            //        where l.ProviderName.StartsWith("SQL") && l.TimeCreated > new DateTime(2011, 03, 01)
            //        select l;
            var t = from l in LogRecordCollection(@"E:\Security.evtx")
                    where l.TimeCreated > new DateTime(2011, 03, 01)
                    orderby l.RecordId descending
                    select l ;
            foreach(var tt in t)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("ID : " + tt.Id);
                Console.WriteLine("Activity ID : " + tt.ActivityId);
                Console.WriteLine("Container Log : " + tt.ContainerLog);
                Console.WriteLine("Provider Name : " + tt.ProviderName);
                Console.WriteLine("Task Display Name : " + tt.TaskDisplayName);
                Console.WriteLine("Machine Name : " + tt.MachineName);
                Console.WriteLine("Log Name: " + tt.LogName);
                Console.WriteLine("==================================\n\n");
 //               Console.WriteLine("XML : " + tt.ToXml());
            }
            while (true) ;
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
    }
}
