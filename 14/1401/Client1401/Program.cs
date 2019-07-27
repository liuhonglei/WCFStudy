using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Transactions;

namespace Client1401
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new CommittableTransaction().TransactionInformation.LocalIdentifier);
            Console.WriteLine(new CommittableTransaction().TransactionInformation.LocalIdentifier);
            Console.WriteLine(new CommittableTransaction().TransactionInformation.LocalIdentifier);
            Console.WriteLine(new CommittableTransaction().TransactionInformation.DistributedIdentifier);

            //string acfrom = "A", acTo = "B";
            //double amount = 10.00;
            //Transaction orginalTransaction = Transaction.Current;
            //CommittableTransaction transaction = new CommittableTransaction();
            //try
            //{
            //    Transaction.Current = transaction;
            //    Withdraw(acfrom, amount);
            //    Deposit(acTo, amount);
            //    transaction.Commit();
            //}
            //catch (Exception)
            //{
            //    transaction.Rollback();
            //    throw;
            //}
            //finally {
            //    Transaction.Current = orginalTransaction;
            //    transaction.Dispose();
            //}

            PrintTransactionFlowSupport(new BasicHttpBinding());
            PrintTransactionFlowSupport(new WSHttpBinding());

            Console.ReadKey();
        }


        static void PrintTransactionFlowSupport(Binding binding) {
            var element = binding.CreateBindingElements().Find<TransactionFlowBindingElement>();
            Console.WriteLine($"{binding.GetType().Name }  { (element ==null ? "No" :"Yes" )  }");
        }



        static void Transfer(string accountFrom,string accountTo, double amount) {
            Transaction originalTransaction = Transaction.Current;
            CommittableTransaction transaction = new CommittableTransaction();
            try
            {
                Transaction.Current = transaction;
                Withdraw(accountFrom, amount);
                Deposit(accountTo, amount);


            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"错误{ ex.Message}");
                throw;
            }
            finally {
                Transaction.Current = originalTransaction;
                transaction.Dispose();
            }

        }

        static void Withdraw(string accountId, double amount) {

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("id", accountId);
            parameters.Add("amount", amount);
            InvokeInTransaction(() => { DbAccessUtil.ExecuteNonQuery("P_WITHDRAW", parameters); });

        }

        

        static void Deposit(string accountId, double amount)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("id", accountId);
            parameters.Add("amount", amount);
            InvokeInTransaction(() => { DbAccessUtil.ExecuteNonQuery("P_WITHDRAW", parameters); });
        }

        private static void InvokeInTransaction(Action action)
        {
            Transaction originalTransaction = Transaction.Current;
            CommittableTransaction transaction = null;
            DependentTransaction dependentTransaction = null;
            if (null == Transaction.Current)
            {
                transaction = new CommittableTransaction();
                Transaction.Current = transaction;
            }
            else {
                dependentTransaction = Transaction.Current.DependentClone(DependentCloneOption.BlockCommitUntilComplete);
                Transaction.Current = dependentTransaction;
            }
            try
            {
                action();
                if (null != transaction)
                    transaction.Commit();
                if (null != dependentTransaction)
                    dependentTransaction.Complete();
            }
            catch (Exception ex)
            {
                Transaction.Current.Rollback(ex);
                throw;
            }
            finally {
                Transaction transaction2 = Transaction.Current;
                Transaction.Current = originalTransaction;
                transaction2.Dispose();
            }
        }
    }

    internal class DbAccessUtil
    {
        internal static void ExecuteNonQuery(string v, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
