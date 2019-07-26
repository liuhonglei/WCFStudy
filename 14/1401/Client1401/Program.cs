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
            InvokeInTransaction( () =>{  } );

        }

        

        static void Deposit(string accountId, double amount)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("id", accountId);
            parameters.Add("amount", amount);
            InvokeInTransaction(() => { });
        }

        private static void InvokeInTransaction(Func<object> p)
        {
            
        }
    }
}
