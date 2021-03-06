﻿using Microsoft.Extensions.Configuration;

namespace Acme.Web.Api.Config
{
    public class ConfigurationFactory
    {
        private readonly string _readOnlyString;
        private readonly string _readWriteConnectionString;

        public ConfigurationFactory()
        {
            _readWriteConnectionString = Startup.Configuration.GetConnectionString("ReadWriteConnectionString");
            _readOnlyString = Startup.Configuration.GetConnectionString("ReadOnlyString");
        }

        public ApplicationConfiguration ApplicationConfig => new ApplicationConfiguration
        {
            ReadOnlyString = _readOnlyString,
            ReadWriteConnectionString = _readWriteConnectionString
        };
    }
}