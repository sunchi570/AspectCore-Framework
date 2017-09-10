﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AspectCore.Configuration;
using AspectCore.DynamicProxy;
using Xunit;

namespace AspectCore.Tests.DynamicProxy
{
    public class InheritedTest : DynamicProxyTestBase
    {
        [Fact]
        public void Interface_Interceptor_Inherited()
        {
            var proxy = ProxyGenerator.CreateInterfaceProxy<INamedService, Service>();
            Assert.Equal("serviceproxy", proxy.GetName());
        }

        [Fact]
        public void Class_Interceptor_Inherited()
        {
            var proxy = ProxyGenerator.CreateClassProxy<Service>();
            Assert.Equal("serviceproxy", proxy.GetName());
        }

        public interface INamed
        {
            [NamedInterceptor(Inherited = true)]
            string GetName();
        }

        public interface INamedService:INamed
        {
        }

        public class Service : INamedService
        {
            public virtual string GetName()
            {
                return "service";
            }
        }

        public class NamedInterceptor : InterceptorAttribute
        {
            public async override Task Invoke(AspectContext context, AspectDelegate next)
            {
                await next(context);
                context.ReturnValue = context.ReturnValue + "proxy";
            }
        }
    }
}