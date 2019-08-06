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

namespace Service1501
{
    public partial class Form1 : Form
    {
        private SynchronizationContext _syncContext;
        private ServiceHost _serviceHost;

        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string header = string.Format(" {0,-13} {1,-22} {2} ","Client", "Time","Event");
            listBox1.Items.Add(header);
            _syncContext = SynchronizationContext.Current;
            EventMonitor.MonitoringNotificationSended += ReceiveMonitoringNotification;
            this.Disposed += Form1_Disposed;
            _serviceHost = new ServiceHost(typeof(CalculatorService));
            _serviceHost.Open();
        }

        private void Form1_Disposed(object sender, EventArgs e)
        {
            EventMonitor.MonitoringNotificationSended -= ReceiveMonitoringNotification;
             _serviceHost?.Close();
        }

        private void ReceiveMonitoringNotification(object sender, MonitorEventArgs e)
        {
            string message = string.Format(" {0,-13} {1,-22} {2} ", e.clientId, e.now.ToLongTimeString(), e.eventType);

            _syncContext.Post((d) => listBox1.Items.Add(message), null);
        }
    }
}
