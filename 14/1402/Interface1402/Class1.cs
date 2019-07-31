using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Interface1402
{
    [ServiceContract(Namespace = "http://www.lhl.com/bankingservice")]
    public interface  IBankingService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void Transfer(string accountFrom, string accountTo, double amount);
    }
    [ServiceContract(Namespace = "http://www.lhl.com/bankingservice")]
    public interface IWithDrawService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void WithDraw(string accountId, double amount);

    }

    [ServiceContract(Namespace = "http://www.lhl.com/bankingservice")]
    public interface IDepositService
    {
        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void Deposit(string accountId, double amount);

    }
}
