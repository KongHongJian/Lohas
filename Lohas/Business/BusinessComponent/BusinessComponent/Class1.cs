//========================================================================
// Copyright(C): Lohas
//
// CLR Version : 4.0.30319.42000
// NameSpace : $rootnamespace$
// FileName : Class1
// Created by : Jerry (hongjian_kong@outlook.com) at 2016/1/15 19:02:11
// Function :Class1  
//================================================================

using Lohas.Business.Component.Contract;
using Lohas.Core.Ioc;

namespace Lohas.Business.Component
{
    [DependencyInjection(typeof(IClass1))]
    public class Class1:IClass1
    {
        public string SayHello()
        {
            return "this is test";
        }
    }
}
