using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Extensions
{
    public static class Extensions
    {
        public static IDictionary<string, string> EnumToDictionary<TEnum>()
                where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            IDictionary<string, string> enumDictionary = new Dictionary<string, string>();
            foreach(TEnum e in Enum.GetValues(typeof(TEnum)))
            {
                enumDictionary.Add(Convert.ToInt32(e).ToString(), GetEnumDescription(e as Enum));
            }
            return enumDictionary;
        }

        public static string GetEnumDescription(this Enum TEnum)
        {
            Type genericEnumType = TEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(TEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return TEnum.ToString();
        }

        //public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, long selectedValue)
        //{
        //    return from item in items
        //           select new SelectListItem
        //           {
        //               Text = item.GetPropertyValue("Naziv"),
        //               Value = item.GetPropertyValue("Sifra"),
        //               Selected = item.GetPropertyValue("Sifra").Equals(selectedValue.ToString())
        //           };
        //}

        public static string GetPropertyValue<T>(this T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, null).ToString();
        }
    }
}
