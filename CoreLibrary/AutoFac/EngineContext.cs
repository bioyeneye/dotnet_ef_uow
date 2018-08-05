using System;
using Autofac;

namespace CoreLibrary.AutoFac
{
    public class EngineContext
    {
        public static ILifetimeScope Current => LibraryScopeResolver.Scope;

        public static T Resolve<T>(string key = "") where T : class
        {

            if (string.IsNullOrEmpty(key))
            {
                return Current.Resolve<T>();
            }
            return Current.ResolveKeyed<T>(key);
        }

        public static object Resolve(Type type)
        {
            return Current.Resolve(type);
        }
    }
}