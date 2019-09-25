using Service.Interface2001;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service2001
{
    
    public class ResourceService : IResourceService
    {
        public string GetString(string key)
        {
           return Resources.ResourceManager.GetString(key);
        }
    }
}
