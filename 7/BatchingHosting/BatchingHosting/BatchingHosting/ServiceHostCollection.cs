using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Collections.ObjectModel;
using Artech.BatchingHosting.Configuration;
using System.ServiceModel.Description;

namespace Artech.BatchingHosting
{
public class ServiceHostCollection : Collection<ServiceHost>, IDisposable
{
    public ServiceHostCollection(params Type[] serviceTypes)
    {
        BatchingHostingSettings settings = BatchingHostingSettings.GetSection();
        foreach (ServiceTypeElement element in settings.ServiceTypes)
        {
            this.Add(element.ServiceType);
        }

        if (null != serviceTypes)
        { 
            Array.ForEach<Type>(serviceTypes, serviceType=> this.Add(new ServiceHost(serviceType)));
        }
    }
    public void Add(params Type[] serviceTypes)
    {
            //ServiceMetadataBehavior
        if (null != serviceTypes)
        {
            Array.ForEach<Type>(serviceTypes, serviceType => this.Add(new ServiceHost(serviceType)));
        }
    }
    public void Open()
    {
        foreach (ServiceHost host in this)
        {
            host.Open();
        }
    }
    public void Dispose()
    {
        foreach (IDisposable host in this)
        {
            host.Dispose();
        }
    }
}
}
