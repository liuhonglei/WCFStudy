using System;
using System.Collections.Generic;
using System.Linq;
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

            string acfrom = "A", acTo = "B";
            double amount = 10.00;
            Transaction orginalTransaction = Transaction.Current;
            CommittableTransaction transaction = new CommittableTransaction();
            try
            {
                Transaction.Current = transaction;
                Withdraw(acfrom, amount);
                Deposit(acTo, amount);
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally {
                Transaction.Current = orginalTransaction;
                transaction.Dispose();
            }

            Console.ReadKey();
        }

        static void Withdraw(string accountId, double amount) {

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("id", accountId);
            parameters.Add("amount", amount);
            InvokeInTransaction(() => { });

        }

        

        static void Deposit(string accountId, double amount)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("id", accountId);
            parameters.Add("amount", amount);
            InvokeInTransaction(() => { });
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
}
