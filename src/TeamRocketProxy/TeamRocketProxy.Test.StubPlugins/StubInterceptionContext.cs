using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using TeamRocketProxy.Integration;
using Timer = System.Timers.Timer;

namespace TeamRocketProxy.Test.StubPlugins
{
    sealed class StubInterceptionContext : IInterceptionContext
    {
        public StubInterceptionContext()
        {
            messages = new List<IInterceptedMessage>();

            timer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
            timer.Elapsed += OnTimerElapsed;
        }

        readonly object messagesLock = new object();
        readonly List<IInterceptedMessage> messages;
        readonly Timer timer;
        FilterOptions currentFilterOptions;
        long messageCounter;

        public event EventHandler<MessageInterceptionEventArgs> OnNewMessageIntercepted;

        public void StartCapture()
        {
            timer.Start();
        }

        public void StopCapture()
        {
            timer.Stop();
        }

        public void SetFilterOptions(FilterOptions options)
        {
            currentFilterOptions = options;
        }

        public IEnumerable<IInterceptedMessage> GetMessages()
        {
            lock (messagesLock)
            {
                IEnumerable<IInterceptedMessage> filteredMessages = this.messages;
                if (currentFilterOptions != null)
                {
                    filteredMessages = filteredMessages.Where(m => (m.Direction & currentFilterOptions.Direction) != 0);

                    if (!string.IsNullOrEmpty(currentFilterOptions.UserFilterText))
                    {
                        filteredMessages = filteredMessages.Where(m => m.MessageType.IndexOf(currentFilterOptions.UserFilterText, StringComparison.OrdinalIgnoreCase) >= 0);
                    }
                }

                return filteredMessages.ToList();
            }
        }

        void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var messageID = Interlocked.Increment(ref messageCounter);
            var direction = (messageID % 2 == 0) ? MessageDirection.Outbound : MessageDirection.Inbound;

            var message = new StubInterceptedMessage(messageID, direction, $"StubMessage{direction:G}");
            lock (messagesLock)
            {
                messages.Add(message);
            }
            OnNewMessageIntercepted?.Invoke(this, new MessageInterceptionEventArgs(message));
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public void Save(string path)
        {
            throw new NotImplementedException();
        }

        public void Load(string path)
        {
            throw new NotImplementedException();
        }
    }
}
