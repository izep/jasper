﻿using System;
using System.Collections.Concurrent;
using Baseline;
using Jasper.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace Jasper.Settings
{
    public class SettingsCollection
    {
        private readonly ConcurrentCache<Type, object> _settings = new ConcurrentCache<Type, object>();

        public T Get<T>() where T : class, new()
        {
            var type = typeof(T);
            if (_settings.Has(type))
            {
                return _settings[type].As<T>();
            }

            return new T();
        }

        public void Alter<T>(Action<T> alteration) where T : class, new()
        {
            alteration(Get<T>());
        }

        public void Replace<T>(T settings) where T : class
        {
            _settings[typeof(T)] = settings;
        }

        public void Add(object instance)
        {
            _settings.Fill(instance.GetType(), instance);
        }

        public void Register(ServiceRegistry registry)
        {
            foreach (var setting in _settings)
            {
                registry.For(setting.GetType()).Use(setting).Singleton();
            }
        }
    }
}