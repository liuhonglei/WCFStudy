using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace BindingsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public abstract class SimpleChannelBase : ChannelBase
    {
        protected void Print(string methodName) {
            Console.WriteLine($"{this.GetType()}.{methodName}");
        }

        protected override void OnClosing()
        {
            base.OnClosing();
        }

        protected override void OnFaulted()
        {
            base.OnFaulted();
        }

        protected override void OnOpened()
        {
            base.OnOpened();
        }

        protected override void OnOpening()
        {
            base.OnOpening();
        }

        protected override void OnAbort()
        {
            throw new NotImplementedException();
        }

        protected override void OnClose(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        protected override void OnEndClose(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        protected override void OnOpen(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public override T GetProperty<T>()
        {
            return base.GetProperty<T>();
        }

        protected override void OnClosed()
        {
            base.OnClosed();
        }

        public ChannelBase InnerChannel { get; private set; } 
        protected SimpleChannelBase(ChannelManagerBase channelManager,ChannelBase innerChannel) : base(channelManager)
        {
            InnerChannel = innerChannel;
        }
    }
}
