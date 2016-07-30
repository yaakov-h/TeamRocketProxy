﻿using System;
using System.Collections.Generic;

namespace TeamRocketProxy.Integration
{
    public interface IInterceptionContext : IDisposable
    {
        void Initialize();
        void SetFilterOptions(FilterOptions options);

        IEnumerable<IInterceptedMessage> GetMessages();
        event EventHandler<MessageInterceptionEventArgs> OnNewMessageIntercepted;

        void Save(string path);
        void Load(string path);
    }
}
