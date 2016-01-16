//========================================================================
// Copyright(C): Lohas
//
// CLR Version : 4.0.30319.42000
// NameSpace : Lohas.Core.Ioc
// FileName : DependencyInjectionAttribute
// Created by : Jerry (hongjian_kong@outlook.com) at 2016/1/16 12:36:45
// Function :DependencyInjectionAttribute  
//================================================================
using System;

namespace Lohas.Core.Ioc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor, Inherited = false)]
    public sealed class DependencyInjectionAttribute : Attribute
    {

        public DependencyInjectionAttribute(Type typeToResolve, string name)
        {
            this.TypeToResolve = typeToResolve;
            this.RegisterdName = name;
        }

        public string RegisterdName { get; set; }

        public Type TypeToResolve { get; private set; }

        public DependencyInjectionAttribute(Type typeToResolve): this(typeToResolve, null)
        {

        }


    }
}
