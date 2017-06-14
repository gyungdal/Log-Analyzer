using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace LogAnalysis
{
    class WindowEventLogger
    {
        private static WindowEventLogger instance;

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
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\Rafael\Desktop\order.xml");

            foreach (XmlNode xn in doc.SelectNodes("/orders/order"))
            {

                string orderDate = xn["order_date"].InnerText;
                string customer = xn["customer_id"].InnerText;
                int customerID = Int32.Parse(customer);//convert to integer

                foreach (XmlNode item in doc.SelectNodes("/orders/order/product"))
                {
                    string productID = item["product_id"].InnerText;
                    string productQT = item["product_qt"].InnerText;
                    int product_qt = Int32.Parse(productQT);//convert to integer

                }
            }
        }
    }
}
