using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocatorPattern
{
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("Service Locator", typeof(ServiceLocator)).GetComponent<ServiceLocator>();
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }

        private Dictionary<Type, object> services = new Dictionary<Type, object>();

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Register<T>(T serviceInstance)
        {
            if (!services.ContainsKey(typeof(T)))
            {
                services.Add(typeof(T), serviceInstance);
            }
            else
            {
                services[typeof(T)] = serviceInstance;
            }
        }

        public void Register(Type type, object serviceInstance)
        {
            if (!services.ContainsKey(type))
            {
                services.Add(type, serviceInstance);
            }
            else
            {
                services[type] = serviceInstance;
            }
        }

        public T GetService<T>()
        {
            if (services.TryGetValue(typeof(T), out object serviceObject))
            {
                return (T)serviceObject;
            }
            else
            {
                Debug.LogWarning($"Did you forget to register the service of type {typeof(T)} ?");
                return default;
            }
        }

        public bool TryGetService<T>(out T service)
        {
            if (services.TryGetValue(typeof(T), out object serviceObject))
            {
                service = (T)serviceObject;
                return true;
            }
            else
            {
                service = default;
                Debug.LogWarning($"Did you forget to register the service of type {typeof(T)} ?");
                return false;
            }
        }
    }
}