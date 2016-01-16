using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;
//========================================================================
// Copyright(C): Lohas
//
// CLR Version : 4.0.30319.42000
// NameSpace : $rootnamespace$
// FileName : Class1
// Created by : Jerry (hongjian_kong@outlook.com) at 2016/1/15 18:50:50
// Function :Class1  
//================================================================
using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Lohas.Core.Ioc
{
    public static class ObjectFactory
    {
        private static readonly IUnityContainer _iUnityContainer;
        private static readonly int _initialized;
        static ObjectFactory()
        {
            if (Interlocked.Exchange(ref _initialized, -1) == 0)
            {
                _iUnityContainer = LoadUnityContainer();
            }

        }

        private static IUnityContainer LoadUnityContainer()
        {
            var baseDirectory = System.Web.HttpContext.Current == null ? AppDomain.CurrentDomain.BaseDirectory : AppDomain.CurrentDomain.RelativeSearchPath;
            var unityAttributeType = typeof (DependencyInjectionAttribute);
            var baseIndex = baseDirectory.Length;
            var dlls = Directory.GetFiles(baseDirectory, "*Lohas.*.dll", SearchOption.TopDirectoryOnly);
            IUnityContainer defaultUnityContainer = new UnityContainer();
            defaultUnityContainer.AddNewExtension<Interception>();

            foreach (var item in dlls)
            {
                var file = item.Substring(baseIndex);
                if (!file.EndsWith(".Business.Component.dll") && !file.EndsWith(".Business.Component.Contract"))
                {
                    continue;
                }

                var assembly = Assembly.LoadFrom(item);

                var types = assembly.GetTypes().Where(t => t.IsClass);

                foreach (var type in types)
                {
                    var attribute = Attribute.GetCustomAttributes(type).OfType<DependencyInjectionAttribute>().FirstOrDefault();

                    if (attribute == null)
                    {
                        continue;
                    }

                    PropertyInfo registeredNameProperty = unityAttributeType.GetProperty("RegisterdName", typeof(string));
                    PropertyInfo typeToSolveProperty = unityAttributeType.GetProperty("TypeToResolve", typeof(Type));

                    var registeredName = registeredNameProperty.GetValue(attribute, null) as string;
                    var typeToSolve = typeToSolveProperty.GetValue(attribute, null) as Type;

                    if (typeToSolve == null)
                    {
                        continue;
                    }
                    //todo

                    var interceptions = new InjectionMember[0];

                    var lifttimeManager = new ContainerControlledLifetimeManager();

                    if (string.IsNullOrEmpty(registeredName))
                    {
                        defaultUnityContainer.RegisterType(typeToSolve, type,lifttimeManager, interceptions);
                    }
                    else
                    {
                        defaultUnityContainer.RegisterType(typeToSolve, type, registeredName, lifttimeManager, interceptions);
                    }
                }
            }
             
            return defaultUnityContainer;
        }


        private static IUnityContainer GetContainer()
        {
            return _iUnityContainer;
        }


        #region Resolve

        public static T Resolve<T>()
        {
            var unityContainer = GetContainer();

            if (unityContainer == null)
            {
                return default(T);
            }
            return unityContainer.Resolve<T>();
        }

        public static IEnumerable<T> ResolveAll<T>()
        {
            var unityContainer = GetContainer();

            if (unityContainer == null)
            {
                return new List<T>();
            }
            return unityContainer.ResolveAll<T>();
        }

        public static T Resolve<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Resolve<T>();
            }

            var unityContainer = GetContainer();

            if (unityContainer == null)
            {
                return default(T);
            }
            return unityContainer.Resolve<T>(name);
        }

        public static T Resolve<T>(params ResolverOverride[] overrides)
        {
            var unityContainer = GetContainer();

            if (unityContainer == null)
            {
                return default(T);
            }
            return unityContainer.Resolve<T>(overrides);
        }


        #endregion
    }
}
