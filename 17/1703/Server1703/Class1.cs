using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Server1703
{
   static  class Class1
    {
        public static MsmqMessageProperty GetMsmqMessageProperty(this OperationContext context)
        {
            if (context.IncomingMessageProperties.ContainsKey(MsmqMessageProperty.Name))
                return context.IncomingMessageProperties[MsmqMessageProperty.Name] as MsmqMessageProperty;
            return null;
        }
    }
}
