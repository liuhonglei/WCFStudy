using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            //WCF分为服务模型层和信道层
            //服务模型层建立在信道层之上，提供一个统一的，可扩展的编程模型；信道层通过信道栈实现对消息的传输和处理
            //绑定具有重要地位：组成整个信道层的信道栈由绑定创建，绑定是连接两个层次的纽带。

            //信道和信道栈
            //信道层，若干信道首尾相连，组成一个管道，成为信道栈。
            //WCF信道有两种必需的信道，传输信道和消息编码信道。信道栈的最终任务总是实现对消息的网络传输，所以传输信道是必需的，
            //传输前需要对消息进行编码，而消息编码功能是通过消息编码信道实现的。除此之外，其他一些功能也需要通过在消息交换中
            //添加一些相应的消息处理操作来实现，典型功能包括：
            //事务流转：将从客户端开始的事务流转到服务器，从而将服务的执行纳入事务。
            //安全传输：保证数据包或消息的安全，避免被恶意篡改与窥视，同时解决客户端和服务身份认证问题。
            //可靠传输：在网络环境稳定的情况下保证数据包或消息的可靠、有序传输。
            //对WS-*协议的支持都是通过在信道栈中添加相应的信道实现的，所以我们把这样的信道称为协议信道。
            //传输信道+消息编码信道+协议信道 = 完整、功能强劲的信道栈。

            //信道分3类：
            //传输信道 ： 实现基于某种网络协议（HTTP HTTPS TCP MSMQ NamePipes）的消息传输
            //消息编码 ： 实现消息编码，常见编码方式Text/XML Binary  MTOM.
            //协议信道 ： WS-*协议的支持，比如 WS-Security WS-RM WS-AT
            //信道管理器 ： ChannelManager 是信道监听器(ChannelListener)和信道工厂(ChannelFactory)的统称，分别对应服务端和客户端
            //ChannelFactory

            //消息交换模式：定义了参与者进行消息交换的模板。定义了消息发送者与接受者相互进行消息传输的次序
            //典型消息交换模式包括数据报模式、请求-回复模式、双工模式。
            //数据报模式，最简单的消息交换模式，称为发送-遗忘或者单向模式。
            //请求-回复模式：消息发送给接收方后等待回复消息，一般采用同步的通信方式。也可异步通信。
            //双工模式：任何一方都可以向对方发送消息。双工操作使服务端回调客户端成为可能。
            //比较典型的双工通信模式是订阅-发布模式，订阅方向发布方发送对某一主题的订阅请求，发布方将收到消息后
            //将订阅方添加进列表，主体发布时，发布方对所有订阅方进行广播。
            //不同网络传输协议对双工通信具有不同支持方式，TCP本身是双工，能够提供原生支持。Http不支持双工通信，
            //基于WSDualHttpBinding双工通信实际上采用了2个关联的Http通道实现。

            //信道形状：WCF通过信道形状表示不同的消息交换模式对消息交换双方信道的不同要求，并定义了相应接口来规范
            //基于不同信道形状的信道应该具有的操作。
            //   IOutputChannel /IInputChannel 、  IRequestChannel /IReplyChannel 、  IDuplexChannel
            //MEP                     消息发送                    消息接受
            //数据报                  IOutputChannel              IInputChannel
            //请求回复                IRequestChannel             IReplyChannel
            //双工                    IDuplexChannel              IDuplexChannel
            //IOutputChannel  有类型为Url的Via属性 和类型为EndpointAddress的RemoteAddress属性，分别对应物理地址和逻辑地址。

            //IRequestChannel 


            //会话信道
            //状态保持的角度，分为数据报信道和会话信道，数据报信道不需要保持客户端状态，多个客户端可以使用相同的信道。
            //会话信道绑定一个客户端，与客户端对象具有相同的生命周期。
            //WCF通过消息关联的方式来实现会话。就是将发送自相同客户端的消息通过一个会话ID来关联。

           
            //     通过提供通信会话的 ID，定义在交换消息的各方之间建立共享上下文的接口。
            //public interface ISession
            //{
            //     摘要: 
            //         获取用于唯一标识会话的 ID。
                
            //     返回结果: 
            //         用于唯一标识会话的 ID。
            //    string Id { get; }
            //}

            //IInputSession  IOutputSession  IRequestSession IReplySession IDuplexSession

            //会话机制通过会话信道栈实现，所有会话信道直接或间接实现ISessionChannel<TSession>
            //基于信道形状的接口均具有对应的支持会话的版本，包括IOutputSessionChannel、IInputSessionChannel
            //IRequestSessionChannel  IReplySessionChannel   IDuplexSessionChannel。


            //信道监听器
            //

            //绑定元素
            //绑定元素分为传输绑定元素 编码绑定元素 和 协议绑定元素。绑定元素最根本作用是用于创建信道监听器和信道工厂
            //绑定
            //绑定是绑定元素的有序集合。信道栈由信道组成，信道顺序决定信道栈的能力。绑定元素可以创建信道监听器和信道工厂，
            //并创建信道，所以绑定本身的特性和能力由自身所包含的绑定元素及这些绑定元素之间的先后顺序决定。
            //要看绑定是否支持某种特性，只需查看是否具有与此特性有关的绑定元素即可。
            //比如要判断某种绑定是否支持某种类型的传输协议，只需看看构成该绑定的传输绑定元素就可以了。
            //WSHttpBinding的传输绑定元素是HttpTransportBindingElement，所以支持http或https的传输协议。
            //是否支持事务流转，查看绑定元素集合是否包含TransactionFlowBindingElement。

            //所有绑定都继承自Binding抽象类。
            //调用BuildChannelListener<TChannel>和BuildChannelFactory<TChannel>创建信道监听器
            //和信道工厂时需要涉及绑定上下文的BindingContext。
            //BindingContext中类型为BindingElementCollection 的RemainingBindingElements 属性包含了所有的绑定元素。
            //public BindingParameterCollection BindingParameters { get; }是以元素类型作为key的字典。
            //通过该属性绑定相应的参数到指定的上下文去控制自定义信道的处理行为。
            //作为实现WCF扩展常用的四大行为（服务行为、终结点行为、契约行为和操作行为）的AddBindingParameters方法传递
            //的参数最终都会保存在这里。
            //public IChannelFactory<TChannel> BuildInnerChannelFactory<TChannel>();
            //public IChannelListener<TChannel> BuildInnerChannelListener<TChannel>() where TChannel : class, IChannel;
            //通过调用这两个方法得到下一个信道监听器和信道工厂。


            //系统绑定
            //BasicHttpBinding WSDualHttpBinding WSHttpBinding WS2007HttpBinding NetTcpBinding NetNamedPipeBinding NetMsmqBinding
            //Net前缀的NetTcpBinding  NetNamedPipeBinding   NetMsmqBinding局限于.net平台使用，这些绑定类型用于
            //WCF客户端和WCF服务端直接通信，通信场景用于局域网内部，又称为局域网绑定。
            //NetNamedPipeBinding仅限于同台机器上的跨进程通信，虽然命名管道支持统机器通信。
            //NetMsmqBinding，称为队列服务。

            //WCF主要通过以WS为前缀的WSHttpBinding 、WS2007HttpBinding实现跨平台支持。提供了很多WS-*标准的支持。
            //称为互联网绑定
            //WSDualHttpBinding实现了基于Http的双向通信，使服务端回调客户端成为可能。
            //WSDualHttpBinding和NetTcpBinding均支持双向通信。WSDualHttpBinding基本上用于局域网环境。

            //BasicHttpBinding主要目的在于实现与之前的.asmx Web服务进行互操作。

            //自定义绑定
            //CustomBinding

            //绑定配置

        }
    }

 
    
}
