using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF学习
{
    class Program
    {
        static void Main(string[] args)
        {
            //SOA鼓励创建可组合的服务：把最小粒度功能的服务成为原子服务，多个原子服务通过合理的组合、编排构成一个新的聚合型的服务。
            //    比如，我们把通过一系列独立服务承载的活动，按照相应的规则进行编排，构成一个聚合型的工作流服务。
            //SOA鼓励服务的复用：服务组合促进了服务的复用，SOA强调创建与场景无关的服务，同一个服务在不同场景可以复用。
            //SOA强调松耦合：SOA通过契约实现客户端对服务的调用，通过契约进行交互，促进服务的自治，只要契约不变，服务本身可以自由变化


            //创建svc文件
            //svc文件包含一个%@ ServiceHost%指令，例如 <%@ ServiceHost Language="C#" Debug="true" Service="Wcf_2_1.Service1" CodeBehind="Service1.svc.cs" %>
            //创建web应用
            //寄宿在iis下的wcf服务实际上就是一个web应用，所以需要通过iis管理器为寄宿的服务创建一个应用。直接创建一个web应用，并将物理地址映射到
            //wcf项目,承载web应用的配置自然定义在web.config文件中。

        }
    }
}
