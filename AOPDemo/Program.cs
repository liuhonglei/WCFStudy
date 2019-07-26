using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;

namespace Csharp实现AOP方法拦截
{
    #region AOP Helper

    public class MyApoAspect : IMessageSink
    {
        private IMessageSink _NextSink = null;
        private IMyAopMethodFilter _MethodFilter;

        internal MyApoAspect(IMessageSink msgSink, IMyAopMethodFilter methodFilter)
        {
            _NextSink = msgSink;
            _MethodFilter = methodFilter;
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            IMessage message = this.SyncProcessMessage(msg);
            replySink.SyncProcessMessage(message);
            return null;

        }

        public IMessageSink NextSink
        {
            get { return _NextSink; }
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            var cancel = false;
            _MethodFilter.PreAopMethodFilter(msg, ref cancel);
            if (cancel)
            {
                //此处不知如何返回
                return null;
            }
            IMessage returnMethod = _NextSink.SyncProcessMessage(msg);
            if (returnMethod is IMethodReturnMessage)
            {
                if (((IMethodReturnMessage)returnMethod).Exception != null)
                {
                    _MethodFilter.PostAopMethodFilter(msg);
                }

            }
            return returnMethod;
        }
    }

    public class MyAopProperty : IContextProperty, IContributeObjectSink
    {
        private IMyAopMethodFilter _MethodFilter;

        public MyAopProperty(IMyAopMethodFilter methodFilter)
        {
            _MethodFilter = methodFilter;
        }

        public void Freeze(Context newContext) { }

        public bool IsNewContextOK(Context newCtx)
        {
            return true;
        }

        public string Name
        {
            get
            {
                return "MyAopProperty";
            }
        }

        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            return new MyApoAspect(nextSink, _MethodFilter);
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class MyAopAttribute : ContextAttribute, IMyAopMethodFilter
    {
        private IMyAopMethodFilter _MethodFilter;

        public MyAopAttribute()
            : base("MyAopAttribute")
        {
            _MethodFilter = this;
        }

        public override void GetPropertiesForNewContext(IConstructionCallMessage ccm)
        {
            ccm.ContextProperties.Add(new MyAopProperty(_MethodFilter));
        }

        protected virtual void OnPreAopMethodFilter(IMessage msg, ref bool Cancel)
        {
            throw new NotImplementedException();
        }

        protected virtual void OnPostAopMethodFilter(IMessage msg)
        {
            throw new NotImplementedException();
        }

        public void PreAopMethodFilter(IMessage msg, ref bool Cancel)
        {
            this.OnPreAopMethodFilter(msg, ref Cancel);
        }

        public void PostAopMethodFilter(IMessage msg)
        {
            this.OnPostAopMethodFilter(msg);
        }
    }

    public interface IMyAopMethodFilter
    {
        void PreAopMethodFilter(IMessage msg, ref bool Cancel);
        void PostAopMethodFilter(IMessage msg);
    }

    #endregion

    #region AOP Demo

    public class Class1AopAttribute : MyAopAttribute
    {
        protected override void OnPreAopMethodFilter(IMessage msg, ref bool Cancel)
        {
            IMethodMessage call = msg as IMethodMessage;
            Type type = Type.GetType(call.TypeName);
            string callStr = type.Name + "." + call.MethodName;
            var args = call.Args;
            Console.WriteLine("在执行前拦截到" + callStr);
        }

        protected override void OnPostAopMethodFilter(IMessage msg)
        {
            IMethodMessage call = msg as IMethodMessage;
            Type type = Type.GetType(call.TypeName);
            string callStr = type.Name + "." + call.MethodName;
            var args = call.Args;
            Console.WriteLine("在exception异常后拦截到" + callStr);
        }
    }

    [Class1AopAttribute()]
    public class Class1 : ContextBoundObject
    {
        public int A { get; set; }

        public int B(ref int p)
        {
            throw new Exception();
            p++;
            return p + 1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Class1 c1 = new Class1();
            //c1.A = 2;

            int p = 0;
            c1.B(ref p);

            Context context = Context.DefaultContext;

            Console.ReadLine();
        }
    }

    #endregion
}
