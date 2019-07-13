using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Extensions
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, long selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("Naziv"),
                       Value = item.GetPropertyValue("Sifra"),
                       Selected = item.GetPropertyValue("Sifra").Equals(selectedValue.ToString())
                   };
        }
    }
}
