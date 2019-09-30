using Service.Interface2104;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service2104
{
    public class CalculatorService : ICalculator
    {
        public int Add(int x, int y)
        {
            return x + y ;
        }
    }

    public class DiscoveryAsyncResult : IAsyncResult
    {
        public DiscoveryAsyncResult(AsyncCallback callback, object asyncState)
        {
            AsyncState = asyncState;
            this.AsyncWaitHandle = new ManualResetEvent(true);
            this.CompletedSynchronously = this.IsCompleted = true;
            callback?.Invoke(this);
        }

        public DiscoveryAsyncResult(AsyncCallback callback, object asyncState,EndpointDiscoveryMetadata endpoint ) : this(callback, asyncState)
        {
            this.Endpoint = endpoint;
        }

        public bool IsCompleted { get; private set; }

        public WaitHandle AsyncWaitHandle { get; private set; }

        public object AsyncState { get; private set; }

        public bool CompletedSynchronously { get; private set; }

        public EndpointDiscoveryMetadata Endpoint { get; private set; }
    }
    
}
