﻿using Microsoft.Extensions.Configuration;

namespace Acme.Web.Api.Config
{
    public class ApplicationConfigurationFactory : IApplicationConfigurationFactory
    {
        private string _readOnlyString;
        private string _readWriteConnectionString;

        public ApplicationConfigurationFactory()
        {
            _readWriteConnectionString = Startup.Configuration.GetConnectionString("ReadWriteConnectionString");
            _readOnlyString = Startup.Configuration.GetConnectionString("ReadOnlyString");
        }

        public ApplicationConfiguration Config => new ApplicationConfiguration
        {
            ReadOnlyString = _readOnlyString,
            ReadWriteConnectionString = _readWriteConnectionString
        };
    }
}