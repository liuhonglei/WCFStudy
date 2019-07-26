using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Client1302
{
    public static class Utility
    {
        public static T CreateInstance<T>(string typeQname,Type[] parameterTypes,object[] parameters) where T : class{

            Type type = Type.GetType(typeQname);

            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
            ConstructorInfo constructorInfo = type.GetConstructor(bindingFlags, Type.DefaultBinder, parameterTypes, null);
            return Activator.CreateInstance(type, bindingFlags, Type.DefaultBinder, parameters, CultureInfo.InvariantCulture) as T;
        }

        public static void SetPropertyValue(object target,string propertyName,object propertyValue) {

            BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance;
            PropertyInfo propertyInfo = target.GetType().GetProperty(propertyName, bindingFlags);
            propertyInfo.SetValue(target,propertyValue,null);
        }

    }
}
