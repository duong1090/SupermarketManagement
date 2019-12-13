using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupermarketManagement.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("CategoryName"),
                       Value = item.GetPropertyValue("CategoryID"),
                       Selected = item.GetPropertyValue("CategoryID").Equals(selectedValue.ToString())
                   };
        }
        public static IEnumerable<SelectListItem> ToSelectListItemStaff<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("RoleName"),
                       Value = item.GetPropertyValue("RoleID"),
                       Selected = item.GetPropertyValue("RoleID").Equals(selectedValue.ToString())
                   };
        }
        public static IEnumerable<SelectListItem> ToSelectListItemProduct<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("Name"),
                       Value = item.GetPropertyValue("ProductID"),
                       Selected = item.GetPropertyValue("ProductID").Equals(selectedValue.ToString())
                   };
        }
    }
}
