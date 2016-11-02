using System;
using System.Reflection;

namespace DynamicFoliage.OptionsSpace.OptionsFramework
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DynamicDropDownAttribute : AbstractOptionsAttribute
    {
        public DynamicDropDownAttribute(string description, string itemsContainerClass, string itemsProperty, string actionClass, string actionMethod, string group = null) : base(description, group)
        {
            ActionClass = actionClass;
            ActionMethod = actionMethod;
            ItemsContainerClass = itemsContainerClass;
            ItemsProperty = itemsProperty;
        }

        public DynamicDropDownAttribute(string description, string itemsContainerClass, string itemsProperty, string group = null) : base(description, group)
        {
            ActionClass = null;
            ActionMethod = null;
            ItemsContainerClass = itemsContainerClass;
            ItemsProperty = itemsProperty;
        }

        public string[] Items
        {
            get
            {
                var property = Util.FindType(ItemsContainerClass).GetProperty(ItemsProperty, typeof(string[]));
                string[] items = (string[]) property.GetValue(null, null);
                return items;
            }
        }


        public Action<string> Action
        {
            get
            {
                if (ActionClass == null || ActionMethod == null)
                {
                    return null;
                }
                var method = Util.FindType(ActionClass).GetMethod(ActionMethod, BindingFlags.Public | BindingFlags.Static);
                if (method == null)
                {
                    return null;
                }
                return i =>
                {
                    method.Invoke(null, new object[] { i });
                };
            }
        }

        private string ActionClass { get; }

        private string ActionMethod { get; }

        private string ItemsContainerClass { get; }

        private string ItemsProperty { get; }
    }
}