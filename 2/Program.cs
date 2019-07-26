using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
            //URI 统一资源标识
            //唯一地标识一个网络资源的同时也表示资源所处的位置及访问的方式。URI具有如下的结构：
            //传输协议：//主机名：端口号/资源路径
            //典型传输协议：
            //Http Https
            //Net.Tcp  net.tcp://localhost:808/service
            //Net.Pipe  net.pipe://localhost/service
            //Net Msmq net.msmq://lhl.com/service
            
            //EndPointAddress
            //public class ServiceEndpoint
            //{
            //    public EndpointAddress Address { get; set; }
            //    public Binding Binding { get; set; }
            //    public ContractDescription Contract { get; set; }
            //}
            
            //public class EndpointAddress
            //{
            //    public Uri Uri { get; }
            //    public AddressHeaderCollection Headers { get; }
            //    public EndpointIdentity Identity { get; }
            //}
            //EndPointAddress的属性Uri既作为服务的为标识，也作为服务的地址。这个地址可是服务的物理地址，也可以是逻辑地址。
            //Headers是一个AddressHeader的集合，存放一些寻址信息。
            //Identity属性标识服务的身份，被客户端用于认证服务。
            
            ////ServiceHostBase
            //public abstract class ServiceHostBase { 
            //    public virtual void AddServiceEndpoint(ServiceEndpoint endpoint);
            //    public ServiceEndpoint AddServiceEndpoint(string implementedContract, Binding binding, string address);
            //    public ServiceEndpoint AddServiceEndpoint(string implementedContract, Binding binding, Uri address);
            //    public ServiceEndpoint AddServiceEndpoint(string implementedContract, Binding binding, string address, Uri listenUri);
            //    public ServiceEndpoint AddServiceEndpoint(string implementedContract, Binding binding, Uri address, Uri listenUri);
            //}
            //listenUri表示服务的监听地址

            //基地址和相对地址
            //除了以绝对路径的方式指定某个服务的终结点外，还可以通过基地址+相对地址的方式对其进行设置。
            
            //ServiceHost
            //public ServiceHost(object singletonInstance, params Uri[] baseAddresses);
            //public ServiceHost(Type serviceType, params Uri[] baseAddresses);
            //在指定了基地址后，具体添加终结点的时候，只需要指定基于基地址的相对地址即可。
            Uri[] baseAddr =new Uri[2];
            baseAddr[0] = new Uri("http://127.0.0.1/myservices");
            baseAddr[1] = new Uri("net.tcp://127.0.0.1/myservices");
            using(ServiceHost serviceHost = new ServiceHost(typeof(CalculatorService),baseAddr)){
                serviceHost.AddServiceEndpoint(typeof(ICalculator), new BasicHttpBinding(), "calculatorservice");
                serviceHost.AddServiceEndpoint(typeof(ICalculator), new NetTcpBinding(), "calculatorservice");
                serviceHost.Open();
            }

            //得到两个地址，分别为  http://127.0.0.1/myservices/calculatorservice
            //net.tcp://127.0.0.1/myservices/calculatorservice
            //一个传输协议只能指定一个基地址，指定过个会报错。
            //基地址和相对地址可以通过配置的方式进行设置，

            //地址跨终结点共享
            //一个服务实现两个不同的契约接口，在为这两个契约发布地址时，这两个具有相同地址的终结点实际上共享同一个绑定对象。
            using (ServiceHost serivceHost = new ServiceHost(typeof(InstrumentationService)))
            {

                WSHttpBinding binding = new WSHttpBinding();
                serivceHost.AddServiceEndpoint(typeof(IEventLogWriter), binding, "http://localhost:3721/instrumentationService");
                serivceHost.AddServiceEndpoint(typeof(IPerformanceCounterWriter), binding, "http://localhost:3721/instrumentationService");

                serivceHost.Open();
            }
            //客户端终结点地址
            //ClientBase<TChannel>
            //public abstract class ClientBase<TChannel> {
            //    protected ClientBase();
            //    protected ClientBase(InstanceContext callbackInstance);
            //    protected ClientBase(ServiceEndpoint endpoint);
            //    protected ClientBase(string endpointConfigurationName);
            //    protected ClientBase(Binding binding, EndpointAddress remoteAddress);
            //    protected ClientBase(string endpointConfigurationName, EndpointAddress remoteAddress);
            //    protected TChannel Channel { get; }
            //    public ChannelFactory<TChannel> ChannelFactory { get; }
            //    public void Close();
            //    public void Dispose();
            //}
            //定义在CalculatorClient中的Add是通过从基类继承的Channel属性的同名方法实现的。
            //该属性是通过属性 ChannelFactory 返回的ChannelFactory<TChannel>对象创建的。

            //地址报头  AddressHeaderCollection
            //WCF建立在消息交换机制上，支持多种消息。消息格式可以使XML，也可是非XML，比如JSON
            //使用最多的XML消息是SOAP，一个完整的SOAP由消息主体和一组消息报头组成。主体一般是对业务数据的封装，
            //而报头用于保存一些控制信息。
            //对客户端来说，终结点地址上的AddressHeader最终会被添加到请求消息的报头集合中。
            //对服务端来说，会提取出相应的报头信息和本地终结点的地址报头进行比较以选择出于请求匹配的终结点。

            //创建AddressHeader
            //public abstract class AddressHeader{
            
            //    public static AddressHeader CreateAddressHeader(object value);
            //    public static AddressHeader CreateAddressHeader(object value, System.Runtime.Serialization.XmlObjectSerializer serializer);
            //    public static AddressHeader CreateAddressHeader(string name, string ns, object value);
            //    public static AddressHeader CreateAddressHeader(string name, string ns, object value, System.Runtime.Serialization.XmlObjectSerializer serializer);
            //}
            //AddressHeader将指定的对象序列化后存放在地址报头中。默认序列化器为DataContractSerializer。
            //AddressHeader最终需要转换为SOAP消息的报头，而SOAP报头具有自己的名称和命名空间。
            //消息报头通过MessageHeader表示。
            //通过AddHeadersTo（Message message）将AddressHeaderCollection添加到一个代表消息的Message对象的报头。

            //终结点地址报头指定
            //using (ServiceHost serivceHost = new ServiceHost(typeof(InstrumentationService)))
            //{
            //    Uri uri = new Uri("http://localhost:3721/instrumentationService");
            //    AddressHeader header = AddressHeader.CreateAddressHeader("Licensed User", "LHL", "UserType");
            //    EndpointAddress address = new EndpointAddress(uri, header);
            //    Binding binding = new WSHttpBinding();
            //    ContractDescription contract = ContractDescription.GetContract(typeof(IEventLogWriter));
            //    ServiceEndpoint endpoint = new ServiceEndpoint(contract, binding, address);
            //    serivceHost.AddServiceEndpoint(endpoint);
            //    serivceHost.Open();
            //}
            //通过配置方式指定报头
          //  <system.serviceModel>
          //  <services>
          //    <service name="Service.InstrumentationService" >
          //      <endpoint address="http://localhost:3721/instrumentationService" binding="wsHttpBinding" contract="Service.IEventLogWriter">
          //        <headers>
            //          <UserType xmlns="lhl" > Licensed User </UserType> //AddressHeader序列化后的内容
          //        </headers>
          //      </endpoint>
          //      <endpoint address="http://localhost:3721/instrumentationService" binding="wsHttpBinding" contract="Service.IPerformanceCounterWriter"></endpoint>
          //    </service>
          //  </services>
          //</system.serviceModel>

            // 1.SOAP具有一个<TO>报头表示调用服务的地址，被选择的终结点地址必须具有相匹配的Uri。
            // 2.如果终结点地址具有相应的地址报头，则要求请求消息也具有相应的报头。
            // 两者同时满足，终结点才会被选择用于处理消息。
            // 直接手工方式在Message上添加相应报头，OperationContext，具有IncomingMessageHeaders和OutgoingMessageHeaders
            // 分别表示入栈消息和出栈消息，对应客户端来说，对应回复消息和请求消息，对于服务端正好相反。

            //AddressFilterMode  Prefix Exact Any  精确匹配  基于前缀  任意匹配

            //端口共享

            //逻辑地址和物理地址
            //EndpointAddress对象的Uri属性仅仅代表服务的逻辑地址，而物理地址对于服务端来说是监听地址，
            //对于客户端来说是消息真正发送的目标地址。默认是统一的，在网络环境限制时，逻辑地址和物理地址实现分离。

            //物理地址接受消息 逻辑地址发送消息 ？
            //客户端监听端口8888，终结点端口9999，那么客户端会将消息发往8888端口，而请求的消息报头<To/>地址中端口为9999
            //逻辑地址和物理地址的分离反映在客户端就意味着请求消息的<To>报头的地址（逻辑地址）和消息真正发送的地址（物理地址）可以不一致。
            //而对于服务端来说，意味着服务的监听地址（物理地址）和接受到的消息的<To>报头的地址（逻辑地址）可以不一样。
            //对于服务端来说，物理地址是真正用来请求监听的地址

            //监听地址和监听模式

            //信道分发器 ChannelDispacher  信道监听器 ChannelListener 终结点分发器 EndpointDispacher

            //消息筛选器 MessageFilter
            //ActionMessageFilter 
            //EndpointAddressMessageFilter
            //XPathMessageFilter
            //PrefixEndpointAddressMessageFilter
            //MatchAllMessageFilter
            //MatchNoneMessageFilter
            //EndpointDispatcher
            //ServiceHost生成信道分发器，每个信道分发器有自己的信道监听器，
            //每个信道分发器会生成并拥有自己的终结点分发器。
            //当信道监听器监听到有请求进来，会根据消息自身携带的信息与之相匹配的终结点分发器。
            //根据消息进行终结点分发器的选择称为消息筛选。
        }
        
    }
    
}
