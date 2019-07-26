using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Receiver
{
    public static class Class1
    {
        public static void ListAllBindingElements(this Binding binding) {

            int index = 1;
            foreach (var element in binding.CreateBindingElements())
            {
                //BindingContext context = new BindingContext(binding,null);
                Console.WriteLine(  "{0}.{1}",index++,element.GetType().FullName);
            }
        }
    }
}
