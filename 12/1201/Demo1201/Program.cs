using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;

namespace Demo1201
{
    class Program
    {
        static void Main(string[] args)
        {

            FaultCode code = FaultCode.CreateReceiverFaultCode( 
                new FaultCode("CalculationErro","http://www.lhl.com") );

            IList<FaultReasonText> reasonTexts = new List<FaultReasonText>();
            reasonTexts.Add(new FaultReasonText("parameter is invalid","en-US"));
            reasonTexts.Add(new FaultReasonText("输入参数不合法", "zh-CN"));

            FaultReason reason = new FaultReason(reasonTexts);

            CalculationError detail = new CalculationError("Divide","被除数为0");

            MessageFault messageFault = MessageFault.CreateFault(code,reason,detail,new DataContractSerializer(typeof(CalculationError)),
                                        "http://www.lhl.com/calculatorservice", "http://www.lhl.com/calculationcenter") ;
            var file1 = "faultsoap1.xml";
            var file2 = "faultsoap2.xml";
            WriteFault(messageFault, file1,EnvelopeVersion.Soap11);
            WriteFault(messageFault, file2, EnvelopeVersion.Soap12);

            Message message = Message.CreateMessage(MessageVersion.Soap11WSAddressing10, messageFault, "http://www.lhl.com/calculatorfault");
            Message message1 = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, messageFault, "http://www.lhl.com/calculatorfault");
            using (XmlWriter writer = new XmlTextWriter("11-10.xml", Encoding.UTF8))

            using (XmlWriter writer1 = new XmlTextWriter("12-10.xml", Encoding.UTF8))
            {
                message.WriteMessage(writer);
                message1.WriteMessage(writer1);
            }

            FaultException<CalculationError> faultException = FaultException.CreateFault(messageFault,typeof(CalculationError)) as FaultException<CalculationError>;
            Console.WriteLine($"{ faultException.Code } { faultException.Code.SubCode.Namespace } { faultException.Detail.CalType } { faultException.Detail.Message }");

            //FaultException<ExceptionDetail>

            Console.Read();
        }

        private static void WriteFault(MessageFault messageFault, string file1, EnvelopeVersion soap11)
        {
            using (FileStream stream = new FileStream(file1, FileMode.Create, FileAccess.Write))
            {
                using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8))
                {
                    messageFault.WriteTo(writer,soap11);
                    //Process.Start(file1);
                }
            }
        }
    }

    [DataContract]
     class CalculationError
    {
        public CalculationError(string calType,string message)
        {
            CalType = calType;
            Message = message;
        }
        [DataMember]
        public string CalType { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}
