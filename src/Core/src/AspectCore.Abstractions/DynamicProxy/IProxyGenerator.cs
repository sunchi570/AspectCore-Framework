﻿using System;

namespace AspectCore.DynamicProxy
{
    [NonAspect]
    public interface IProxyGenerator
    {
        object CreateInterfaceProxy(Type serviceType);

        object CreateInterfaceProxy(Type serviceType, object implementationInstance);

        object CreateClassProxy(Type serviceType, Type implementationType, object[] args);
    }
}