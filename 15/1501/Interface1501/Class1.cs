using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Interface1501
{
    [ServiceContract(Namespace = "http://www.lhl.com")]
    public interface ICalculator
    {
        [OperationContract]
        double Add(double a, double b);

    }

    public enum EventType
    {
        StartCall,
        EndCall,
        StartExecute,
        EndExecute,
        StartCallBack,
        EndCallBack,
        StartExecuteCallBack,
        EndExecuteCallBack,

    }

    public static class EventMonitor
    {
        public const string ClientIdHeaderNamespace = "http://www.lhl.com";
        public const string ClientIdHeaderLocalName  = "ClientId";
        public static EventHandler<MonitorEventArgs> MonitoringNotificationSended;

        public static void Send(EventType eventType)
        {
            if (null != MonitoringNotificationSended)
            {
                int clientId = OperationContext.Current.IncomingMessageHeaders.GetHeader<int>(ClientIdHeaderLocalName, ClientIdHeaderNamespace);
                MonitoringNotificationSended(null, new MonitorEventArgs(clientId, eventType, DateTime.Now));
            }
        }

        public static void Send(int clientId, EventType eventType)
        {
            MonitoringNotificationSended?.Invoke(null, new MonitorEventArgs(clientId, eventType, DateTime.Now));
        }
    }

    public class MonitorEventArgs : EventArgs
    { 
        public int clientId { get; private set; }
        public EventType eventType { get; private set; }
        public DateTime now { get; private set; }

        public MonitorEventArgs(int clientId, EventType eventType, DateTime now)
        {
            this.clientId = clientId;
            this.eventType = eventType;
            this.now = now;
        }
    }
}
