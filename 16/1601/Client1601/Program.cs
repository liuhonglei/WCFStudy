using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Text;

namespace Client1601
{
    class Program
    {
        static void Main(string[] args)
        {
            DeliveryRequirementsAttribute
        }
    }

    public  class MessageInspector {

        public int DropRate { get; private set; }
        public Random Randomizer { get; private set; }
        public MessageInspector(int dropRate) {
            this.DropRate = dropRate;
            Randomizer = new Random();
        }

        public virtual void ProcessMessage(ref Message message) {
            int randomNumber = this.Randomizer.Next(100);
            if (randomNumber <= this.DropRate)
                message = null;
        }
    }
}
