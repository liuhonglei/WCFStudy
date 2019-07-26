using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace Service.Interface1001
{
    public class DisplayMessageFormatterAttribute : Attribute, IOperationBehavior
    {
        private object GetField(object target,string fieldName) {
            Type type = target.GetType();
            FieldInfo field = type.GetField(fieldName,BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return field.GetValue(target);

        }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
            
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
            Console.WriteLine("{0}", clientOperation.Name);
            IClientMessageFormatter formatter = clientOperation.Formatter;
            Console.WriteLine($"\t{formatter.GetType().Name}");
            if (formatter.GetType().Name != "CompositeClientFormatter")
                return;
            Object innerFormatter = this.GetField(formatter, "request");
            Console.WriteLine($"\t\t{innerFormatter.GetType().Name}");

            if (innerFormatter.GetType().Name == "UriTemplateClientFormatter")
            {
                innerFormatter = this.GetField(innerFormatter,"inner");
                Console.WriteLine($"\t\t\t{innerFormatter.GetType().Name}");
                return;
            }
            if (innerFormatter.GetType().Name == "ContentTypeSettingClientMessageFormatter")
            {
                innerFormatter = this.GetField(innerFormatter, "innerFormatter");
                Console.WriteLine($"\t\t\t{innerFormatter.GetType().Name}");
                if (innerFormatter.GetType().Name == "UriTemplateClientFormatter")
                {
                    innerFormatter = this.GetField(innerFormatter, "inner");
                    Console.WriteLine($"\t\t\t\t{innerFormatter.GetType().Name}");
                }
            }

        }


        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            
        }

        public void Validate(OperationDescription operationDescription)
        {
            
        }
    }
}
