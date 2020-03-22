using Acme.ChangeHandlers;
using Acme.Data.Context;
using Acme.Data.DataModels;
using Acme.Data.DataModels.Contracts;
using Acme.Toolkit.Extensions;
using System;
using System.Linq;
using System.Security.Principal;

namespace Acme.Data.ChangeHandlers
{
    public class LogEntryChangeHandler : IChangeEventHandler
    {
        private readonly ICrudDataTools _dataTools;

        public LogEntryChangeHandler(ICrudDataTools dataTools)
        {
            _dataTools = dataTools;
        }

        public bool CanHandle(Type type)
        {
            return type.GetInterfaces().Any(x => x == typeof(ILoggable));
        }

        public void OnChange(object oldState, object newState, IIdentity identity)
        {
            identity.ThrowIfNull(nameof(identity));

            if (oldState != null && newState != null)
            {
                LogModify((ILoggable)newState, identity);
            }
            else if (oldState == null && newState != null)
            {
                LogAdd((ILoggable)newState, identity);
            }
            else if (oldState != null && newState == null)
            {
                LogRemove((ILoggable)newState, identity);
            }
        }

        private void Add(string logEntry)
        {
            _dataTools.AddModel(new LogEntryDataModel { LogText = logEntry, CreatedOn = DateTime.Now });
        }

        private void LogAdd(ILoggable newState, IIdentity identity)
        {
            var logEntry = $"{newState.LogDescriptorText} was added by {identity.LogDescriptorText()}";
            Add(logEntry);
        }

        private void LogModify(ILoggable newState, IIdentity identity)
        {
            var logEntry = $"{newState.LogDescriptorText} was modified by {identity.LogDescriptorText()}";
            Add(logEntry);
        }

        private void LogRemove(ILoggable newState, IIdentity identity)
        {
            var logEntry = $"{newState.LogDescriptorText} was removed by {identity.LogDescriptorText()}";
            Add(logEntry);
        }
    }
}