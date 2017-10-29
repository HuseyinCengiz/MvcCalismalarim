using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattern.Extension
{
    public static class Extension
    {
        public static T Donusturucu<T>(this object kaynak)
        {
            T hedef = Activator.CreateInstance<T>();
            Type ktip = kaynak.GetType();
            PropertyInfo[] kaynakProps = ktip.GetProperties();
            PropertyInfo[] hedefProps = typeof(T).GetProperties();
            foreach (PropertyInfo prop in kaynakProps)
            {
                object value = prop.GetValue(kaynak);
                PropertyInfo pinfo = hedefProps.FirstOrDefault(x => x.Name == prop.Name);
                if(pinfo!=null)
                pinfo.SetValue(hedef, value);
            }
            return hedef;
        }
    }
}
