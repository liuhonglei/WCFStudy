using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Service902
{
    class PercallInstanceContextProvider : IInstanceContextProvider
    {
        public InstanceContext GetExistingInstanceContext(Message message, IContextChannel channel)
        {
            return null;
        }

        public void InitializeInstanceContext(InstanceContext instanceContext, Message message, IContextChannel channel)
        {
            
        }

        public bool IsIdle(InstanceContext instanceContext)
        {
            return true;
        }

        public void NotifyIdle(InstanceContextIdleCallback callback, InstanceContext instanceContext)
        {
           
        }
    }
}
