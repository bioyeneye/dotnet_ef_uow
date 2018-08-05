using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Core.Lifetime;

namespace CoreLibrary.AutoFac
{
    public static class LibraryScopeResolver
    {
        public static IContainer Container;
        public static ILifetimeScope Scope { get; private set; }

        public static void EndLifetimeScope()
        {
            if (Scope != null)
            {
                Scope.Dispose();
                Scope = null;
            }
        }

        public static void SetLifetimeScope(this IContainer container)
        {

            if (Scope == null)
            {
                    Scope = container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
        }
    }
}
