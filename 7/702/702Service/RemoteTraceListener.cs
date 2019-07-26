using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Web;
using _702Service.Interface;

namespace _702Service
{
    public class RemoteTraceListener : TraceListener
    {
        public ITrace Tracer { get; private set; }
        public RemoteTraceListener(string traceserviceEndpoint) 
        {
            ChannelFactory<ITrace> channelFactory = new ChannelFactory<ITrace>(traceserviceEndpoint);
            this.Tracer = channelFactory.CreateChannel();

        }

        public override void Write(string message)
        {
            Tracer.Write(message);
        }

        public override void WriteLine(string message)
        {
            Tracer.WriteLine(message);
        }
    }
}