using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Interface2001
{
    class CultureReceiver : ICallContextInitializer
    {
        public CultureMessageHeadInfo messageHeadInfo;

        public CultureReceiver(CultureMessageHeadInfo messageHeadInfo)
        {
            this.messageHeadInfo = messageHeadInfo;
        }

        public void AfterInvoke(object correlationState)
        {
            CultureInfo[] cultureInfos = correlationState as CultureInfo[];
            if (null != cultureInfos)
            {
                Thread.CurrentThread.CurrentCulture = cultureInfos[0];
                Thread.CurrentThread.CurrentUICulture = cultureInfos[1];
            }
        }

        public object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message)
        {
            CultureInfo[] originalCulture = new CultureInfo[] { CultureInfo.CurrentCulture, CultureInfo.CurrentUICulture };
            CultureInfo currentCulture = null;
            CultureInfo currentUICulture = null;
            if (message.Headers.FindHeader(messageHeadInfo.CurrentCultureName, messageHeadInfo.NameSpace) > -1)
            {
                currentCulture = new CultureInfo(message.Headers.GetHeader<string>(messageHeadInfo.CurrentCultureName, messageHeadInfo.NameSpace));
                Thread.CurrentThread.CurrentCulture = currentCulture;
            }
            if (message.Headers.FindHeader(messageHeadInfo.CurrentUICultureName, messageHeadInfo.NameSpace) > -1)
            {
                currentUICulture = new CultureInfo(message.Headers.GetHeader<string>(messageHeadInfo.CurrentUICultureName, messageHeadInfo.NameSpace));
                Thread.CurrentThread.CurrentUICulture = currentUICulture;
            }
            return originalCulture;

        }
    }

    class CultureInfoSender : IClientMessageInspector
    {
        private CultureMessageHeadInfo messageHeadInfo;

        public CultureInfoSender(CultureMessageHeadInfo messageHeadInfo)
        {
            this.messageHeadInfo = messageHeadInfo;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {

        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            request.Headers.Add(MessageHeader.CreateHeader(messageHeadInfo.CurrentCultureName, messageHeadInfo.NameSpace, CultureInfo.CurrentCulture.Name));
            request.Headers.Add(MessageHeader.CreateHeader(messageHeadInfo.CurrentUICultureName, messageHeadInfo.NameSpace, CultureInfo.CurrentUICulture.Name));
            return null;
        }
    }


    public  class CulturePropagationBehaviorAttribute : Attribute, IServiceBehavior, IEndpointBehavior, IContractBehavior
    {
        private CultureMessageHeadInfo messageHeadInfo;
        public const string DefaultNamespace = "http://www.lhl.com/CulturePropagation";
        public const string DefaultCurrentCultureName = "CurrentCultureName";
        public const string DefaultCurrentUICultureName = "CurrentUICultureName";

        public CulturePropagationBehaviorAttribute()
        {
            this.messageHeadInfo = new CultureMessageHeadInfo
            {
                NameSpace = DefaultNamespace,
                CurrentCultureName = DefaultCurrentCultureName,
                CurrentUICultureName = DefaultCurrentUICultureName,
            };

        }

        public string Namespace { get; set; }
        public string CurrentCultureName { get; set; }
        public string CurrentUICultureName { get; set; }
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {

        }
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new CultureInfoSender(messageHeadInfo));

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new CultureInfoSender(messageHeadInfo));
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher cdp in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher edp in cdp.Endpoints)
                {
                    foreach (DispatchOperation dop in edp.DispatchRuntime.Operations)
                    {
                        dop.CallContextInitializers.Add(new CultureReceiver(messageHeadInfo));

                    }
                }
            }
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            foreach (var dop in dispatchRuntime.Operations)
            {
                dop.CallContextInitializers.Add(new CultureReceiver(messageHeadInfo));
            }
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            foreach (var dop in endpointDispatcher.DispatchRuntime.Operations)
            {
                dop.CallContextInitializers.Add(new CultureReceiver(messageHeadInfo));

            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {

        }

        public void Validate(OperationDescription operationDescription)
        {

        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }


    }

    class CultureMessageHeadInfo
    {
        public string NameSpace { get; set; }
        public string CurrentCultureName { get; set; }
        public string CurrentUICultureName { get; set; }
    }

}
