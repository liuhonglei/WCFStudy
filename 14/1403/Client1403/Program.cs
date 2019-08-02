using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Transactions;
using System.Xml;

namespace Client1403
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                WriteTransaction(TransactionProtocol.OleTransactions, Transaction.Current, "oletx.xml");

                WriteTransaction(TransactionProtocol.WSAtomicTransactionOctober2004, Transaction.Current, "wsat10.xml");
                WriteTransaction(TransactionProtocol.WSAtomicTransaction11, Transaction.Current, "wsat11.xml");
            }

        }

        private static void WriteTransaction(TransactionProtocol transactionProtocol, Transaction transaction, string fileName)
        {
            string action = string.Format("http://www.lhl.com/transactionformat/{0}", transactionProtocol.GetType().Name);
            Message message = Message.CreateMessage( MessageVersion.Default,action,Transaction.Current);
            TransactionFormatter formatter = new TransactionFormatter(transactionProtocol);
            formatter.WriteTransaction(Transaction.Current,message);
            using  ( XmlWriter writer = new XmlTextWriter(fileName,Encoding.UTF8) ) {
                message.WriteMessage(writer);
            }

        }
    }

    static class ReflectUtil
    {
        public static object CreateInstance(string typeAssemblyQName,params object[] parameters) {
            Type typeofInstance = Type.GetType(typeAssemblyQName);
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return Activator.CreateInstance(typeofInstance, bindingFlags,null,parameters,null );
        }

        public static object Invoke(string methodName,object targetInstance, params object[] parameters) {

            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return targetInstance.GetType().GetMethod(methodName, bindingFlags).Invoke(targetInstance, parameters);
        }
    }

    public class TransactionFormatter {

        const string OleTxFormatterType = "System.ServiceModel.Transactions.OleTxTransactionFormatter, System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
        const string Wsat10FormatterType = "System.ServiceModel.Transactions.WsatTransactionFormatter10, System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
        const string Wsat11FormatterType = "System.ServiceModel.Transactions.WsatTransactionFormatter11, System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";

        public object InternalFormatter { get; private set; }

        public TransactionFormatter(TransactionProtocol transactionProtocol) {
            if (transactionProtocol == TransactionProtocol.OleTransactions)
                this.InternalFormatter = ReflectUtil.CreateInstance(OleTxFormatterType);
            else if (transactionProtocol == TransactionProtocol.WSAtomicTransactionOctober2004)
                this.InternalFormatter = ReflectUtil.CreateInstance(Wsat10FormatterType);
            else
                this.InternalFormatter = ReflectUtil.CreateInstance(Wsat11FormatterType);
        }

        public Transaction ReadTransaction(Message message ) {
            object transactionInfo = ReflectUtil.Invoke("ReadTransaction", this.InternalFormatter, message);
            return ReflectUtil.Invoke("UnmarshalTransaction", transactionInfo) as Transaction;
        }

        public void WriteTransaction(Transaction transaction, Message message)
        {
             ReflectUtil.Invoke("WriteTransaction", this.InternalFormatter, transaction, message);
        }
    }
}
