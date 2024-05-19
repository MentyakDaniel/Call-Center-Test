using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Call_Center_Test
{
    internal static class Extensions
    {
        public static T GetEnumValueFromDescription<T>(string description)
        {
            MemberInfo[] fis = typeof(T).GetFields();

            foreach (var fi in fis)
            {
                DescriptionAttribute[] attributes = 
                    (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0 && attributes[0].Description == description)
                    return (T)Enum.Parse(typeof(T), fi.Name);
            }

            throw new Exception("Not found");
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo? fieldInfo = value
                .GetType()
                .GetField(value.ToString());

            if(fieldInfo is not null)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo
                    .GetCustomAttributes(typeof(DescriptionAttribute), false);

                return attributes.Length > 0 
                    ? attributes[0].Description 
                    : value.ToString();
            }

            return value.ToString();
        }
    }
}
