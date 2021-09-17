using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApp.Extensions
{
    public static class EnumSelecListExtension
    {
        public static IEnumerable<SelectListItem> ToSelectList<TEnum>(this TEnum enumObj)
                where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return from TEnum e in Enum.GetValues(typeof(TEnum))
                   select new SelectListItem
                   {
                       Text = EnumGetDescriptionExtension.GetDescription(e as Enum),
                       Value = Convert.ToInt32(e).ToString()
                       //Selected = Convert.ToInt32(e).Equals(0)
                   };
        }
    }
}
