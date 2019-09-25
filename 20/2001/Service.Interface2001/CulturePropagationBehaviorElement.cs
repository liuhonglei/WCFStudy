using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface2001
{
    public sealed class CulturePropagationBehaviorElement : BehaviorExtensionElement
    {
        [ConfigurationProperty("namespace", IsRequired = false,DefaultValue = CulturePropagationBehaviorAttribute.DefaultNamespace)]
        public string Namespace {
            get => (string)this["namespace"];
            set => this["namespace"] = value;
        }
        [ConfigurationProperty("currentCultureName", IsRequired = false, DefaultValue = CulturePropagationBehaviorAttribute.DefaultCurrentCultureName)]

        public string CurrentCultureName
        {
            get => (string)this["currentCultureName"];
            set => this["currentCultureName"] = value;
        }

        [ConfigurationProperty("currentUICultureName", IsRequired = false, DefaultValue = CulturePropagationBehaviorAttribute.DefaultCurrentUICultureName)]

        public string CurrentUICultureName
        {
            get =>(string) this["currentUICultureName"];
            set => this["currentUICultureName"] = value;
        }

        public override Type BehaviorType => typeof(CulturePropagationBehaviorAttribute);

        protected override object CreateBehavior()
        {
            return new CulturePropagationBehaviorAttribute
            {
                Namespace = Namespace,
                CurrentCultureName = CurrentCultureName,
                CurrentUICultureName = CurrentUICultureName
            };
        }
    }
}
