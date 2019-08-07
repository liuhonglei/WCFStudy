using Interface1501;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client1501
{
    public partial class Form1 : Form
    {
        private SynchronizationContext _syncContext;
        private ChannelFactory<ICalculator> _channelFactory;
        private static int clientIndex = 0;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string header = string.Format(" {0,-13} {1,-22} {2} ", "Client", "Time", "Event");
            listBox1.Items.Add(header);
            _syncContext = SynchronizationContext.Current;
            _channelFactory = new ChannelFactory<ICalculator>("calculatorservice");

            EventMonitor.MonitoringNotificationSended += ReceiveMonitoringNotification;
            this.Disposed += Form1_Disposed;
            ICalculator proxy = _channelFactory.CreateChannel();
            (proxy as ICommunicationObject).Open(); //显式开启可以 服务调用可以及时处理
            for (int i = 0; i < 5; i++)
            {
                ThreadPool.QueueUserWorkItem((state) =>
                {
                    #region 不同的代理进行调用
                    //int clientId = Interlocked.Increment(ref clientIndex);
                    //ICalculator proxy = _channelFactory.CreateChannel();
                    //using (proxy as IDisposable)
                    //{
                    //    EventMonitor.Send(clientId, EventType.StartCall);
                    //    using (OperationContextScope contextScope = new OperationContextScope(proxy as IContextChannel))
                    //    {
                    //        MessageHeader<int> messageHeader = new MessageHeader<int>(clientId);
                    //        OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader.GetUntypedHeader(EventMonitor.ClientIdHeaderLocalName, EventMonitor.ClientIdHeaderNamespace));
                    //        proxy.Add(1, 2);

                    //    }
                    //    EventMonitor.Send(clientId, EventType.EndCall);
                    //}
                    #endregion
                    int clientId = Interlocked.Increment(ref clientIndex);
                    EventMonitor.Send(clientId, EventType.StartCall);
                    using (OperationContextScope contextScope = new OperationContextScope(proxy as IContextChannel))
                    {
                        MessageHeader<int> messageHeader = new MessageHeader<int>(clientId);
                        OperationContext.Current.OutgoingMessageHeaders.Add(messageHeader.GetUntypedHeader(EventMonitor.ClientIdHeaderLocalName, EventMonitor.ClientIdHeaderNamespace));
                        proxy.Add(1, 2);

                    }
                    EventMonitor.Send(clientId, EventType.EndCall);
                    #region

                    #endregion

                }, null);

            }
        }

        private void Form1_Disposed(object sender, EventArgs e)
        {
            EventMonitor.MonitoringNotificationSended -= ReceiveMonitoringNotification;
            _channelFactory?.Close();
        }

        private void ReceiveMonitoringNotification(object sender, MonitorEventArgs e)
        {
            string header = string.Format(" {0,-13} {1,-22} {2} ", e.clientId, e.now.ToLongTimeString(), e.eventType);
            _syncContext.Post(state => this.listBox1.Items.Add(header), null);

        }
    }
}
