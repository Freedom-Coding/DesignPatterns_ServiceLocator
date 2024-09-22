using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocatorPattern
{
    public class ServiceInstaller : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> servicesToInstall = new List<MonoBehaviour>();
        [SerializeField] private bool InstallFromList;
        [SerializeField] private SoundService2D soundService;

        private void Awake()
        {
            ServiceLocator serviceLocator = ServiceLocator.Instance;

            IDebuggerService debuggerService = new DevelopmentDebugger();
            serviceLocator.Register(debuggerService);

            if (InstallFromList)
            {
                foreach (MonoBehaviour service in servicesToInstall)
                {
                    Type serviceType = service.GetType();
                    Type serviceInterface = serviceType.GetInterfaces()[0];

                    if (serviceInterface != null)
                    {
                        serviceLocator.Register(serviceInterface, service);
                    }
                    else
                    {
                        serviceLocator.Register(serviceType, service);
                    }
                }
            }
            else
            {
                serviceLocator.Register<ISoundService>(soundService);
            }
        }
    }
}