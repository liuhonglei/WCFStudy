using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace MessageDescriptionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ContractDescription c = ContractDescription.GetContract(typeof(ICalculator));
            ShowOperationMessage(c.Operations[0]);
            c = ContractDescription.GetContract(typeof(ICalculator1));
            ShowOperationMessage(c.Operations[0]);
            c = ContractDescription.GetContract(typeof(ICalculator2));
            ShowOperationMessage(c.Operations[0]);
            c = ContractDescription.GetContract(typeof(ICalculator3));
            ShowOperationMessage(c.Operations[0]);
            Console.Read();
        }

        static void ShowMessageBody(MessageDescription message) {

            Console.WriteLine( message.Direction == MessageDirection.Input ? "请求消息":"回复消息"  );

            MessageBodyDescription body = message.Body;
            Console.WriteLine(  " <tns : {0} xmlns :tns = \"{1}\"> ",body.WrapperName,body.WrapperNamespace);
            foreach (var part in body.Parts)
            {
                Console.WriteLine(" \t<tns : {0}> ..</tns = \"{0}\"> ",part.Name);
            }
            if (null != body.ReturnValue)
            {
                Console.WriteLine(" \t<tns : {0}> {1}</tns = \"{0}\"> ", body.ReturnValue.Name,body.ReturnValue.ToString());
            }
            Console.WriteLine("</tns = \"{0}\">",body.WrapperName);
        }

        static void ShowOperationMessage(OperationDescription operation)
        {
            MessageDescription inputMessage = operation.Messages[0];
            ShowMessageBody(inputMessage);
            if (operation.Messages.Count == 2)
            {
                MessageDescription outputMessage = operation.Messages[1];
                ShowMessageBody(outputMessage);
            }
            else
                Console.WriteLine("无回复消息！");
        }
    }
}
