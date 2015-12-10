/**
 * Copyright © 2015 Petro Bazeliuk 
 *
 * The contents of this file are subject to the terms of one of the following
 * open source licenses: Apache 2.0 or or EPL 1.0 (the "Licenses"). You can
 * select the license that you prefer but you may not use this file except in
 * compliance with one of these Licenses.
 * 
 * You can obtain a copy of the Apache 2.0 license at
 * http://www.opensource.org/licenses/apache-2.0
 * 
 * You can obtain a copy of the EPL 1.0 license at
 * http://www.opensource.org/licenses/eclipse-1.0
 * 
 * See the Licenses for the specific language governing permissions and
 * limitations under the Licenses.
 *
 */

using System;
using System.Security;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;

using V8Commit.Services.ConversionServices;
using V8Commit.Services.HashServices;

namespace V8Commit.ConsoleApp
{
    public static class UnityConfig
    {
        public static void Initialize()
        {
            IUnityContainer unityContainer = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));

            unityContainer.RegisterType<IConversionService<UInt64, DateTime>, UInt64ToDateTime>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IHashService, MD5HashService>(new ContainerControlledLifetimeManager());
        }
    }
}
