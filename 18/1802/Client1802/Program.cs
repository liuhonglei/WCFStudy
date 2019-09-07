using Interface1802;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Client1802
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("calculatorservice"))
            {
                channelFactory.Credentials.UserName.UserName = "Foo";
                channelFactory.Credentials.UserName.Password = "Password";
                ICalculator calculator = channelFactory.CreateChannel();
                calculator.Add(1, 2);
                calculator.Add(1, 2);
                calculator.Add(1, 2);
                ServiceAuthorizationManager

            }

        }
    }
}
