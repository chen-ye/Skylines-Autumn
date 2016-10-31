using System;

namespace DynamicFoliage.Redirection
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class RedirectMethodAttribute : Attribute
    {
        public RedirectMethodAttribute()
        {
            this.OnCreated = false;
        }

        public RedirectMethodAttribute(bool onCreated)
        {
            this.OnCreated = onCreated;
        }

        public bool OnCreated { get; }
    }
}
