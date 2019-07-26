using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

namespace _502
{
    class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order
            {
                Customer = "李四",
                Date = DateTime.Now.ToShortDateString(),
                ID = Guid.NewGuid().ToString(),
                Name = "Test",
                ShipAddress = "科技路"
            };
            //Message m = Message.CreateMessage( MessageVersion.Default,"test");
            //m.WriteMessage();
           var header =   MessageHeader.CreateHeader("1", "2", "3");

            //Serialize<IOrder>(order,"order.xml",false);
            Serialize<IOrder>(order, "order.xml", typeof(Order));
            Console.Read();
        }

        static void Serialize<T>(T instance, string fileName, bool preserveReference)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T), null, int.MaxValue, false, preserveReference, null);
            using (XmlWriter writer = new XmlTextWriter(fileName, Encoding.UTF8))
            {
                serializer.WriteObject(writer, instance);
            }
        }
        static void Serialize<T>(T instance, string fileName, params Type[] knowntypes)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T), knowntypes, int.MaxValue, false, false, null);
            using (XmlWriter writer = new XmlTextWriter(fileName, Encoding.UTF8))
            {
                serializer.WriteObject(writer, instance);
            }
        }

    }

    public interface IOrder {

         string ID { get; set; }
         string Date { get; set; }
         string Customer { get; set; }
         string ShipAddress { get; set; }

    }
    [DataContract]
    public abstract class OrderBase : IOrder
    {
        [DataMember]
        public  string ID { get; set; }
        [DataMember]
        public  string Date { get; set; }
        [DataMember]
        public  string Customer { get; set; }
        [DataMember]
        public  string ShipAddress { get; set; }
    }

    [DataContract]
    public class Order : OrderBase
    {
        [DataMember]
        public string Name { get; set; }
       

    }
}
