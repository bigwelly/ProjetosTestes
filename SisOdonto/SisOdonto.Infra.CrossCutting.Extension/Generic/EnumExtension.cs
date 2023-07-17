using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SisOdonto.Infra.CrossCutting.Extension.Generic
{
    public static class EnumExtension
    {
        public static object GetDefaultValue(this System.Enum value)
        {
            DefaultValueAttribute[] enumValues = (DefaultValueAttribute[])
                value
                .GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DefaultValueAttribute), false);

            return (enumValues.Length > 0) ? enumValues[0].Value : null;
        }

        public static System.Enum GetEnumByDefaultValue<T>(string value) where T : System.Enum
        {
            var enumValues = typeof(T).GetEnumValues();

            foreach (var item in enumValues)
            {
                var enumValue = (DefaultValueAttribute[])item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DefaultValueAttribute), false);

                if (enumValue.Any() && enumValue.Where(o => o.Value.ToString() == value).FirstOrDefault() != null)
                {
                    return (T)item;
                }
            }

            return null;
        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            string description = null;

            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (descriptionAttributes.Length > 0)
                        {
                            // we're only getting the first description we find others will be ignored
                            description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                        }

                        break;
                    }
                }
            }

            return description;
        }

        /// <summary>
		/// Recupera o Valor do enum, realizando cast
		/// </summary>
		/// <typeparam name="T">tipo do objeto(int,char)</typeparam>
		/// <param name="enumValue"></param>
		/// <returns>retorna um valor do tipo T</returns>
		public static T GetValue<T>(this Enum enumValue)
        {
            var obj = Convert.ChangeType(enumValue, typeof(T));

            return (T)obj;
        }
    }
}
