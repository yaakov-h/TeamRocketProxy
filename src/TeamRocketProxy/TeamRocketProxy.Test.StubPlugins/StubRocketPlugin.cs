using System;
using TeamRocketProxy.Integration;

namespace TeamRocketProxy.Test.StubPlugins
{
    public sealed class StubRocketPlugin : IRocketPlugin
    {
        public PluginDescriptor GetDescriptor()
            => new PluginDescriptor("Stub", "A basic mockup used for testing.");

        public PluginCapabilities Capabilities => PluginCapabilities.SupportsLiveCapture;

        public IInterceptionContext GetInterceptionContext(IServiceProvider serviceProvider)
            => new StubInterceptionContext();

        public void Describe(IMessageExplorer explorer, IInterceptedMessage message)
        {
            explorer.AddKeyValuePair("success", typeof(bool), true);

            var child = explorer.AddChildObject("child");
            child.AddKeyValuePair("text", typeof(string), "this is some text");
            child.AddKeyValuePair("int", typeof(int), 42);

            var simpleList = explorer.AddChildObject("simple_list");
            simpleList.AddKeyValuePair(0, typeof(string), "aaa");
            simpleList.AddKeyValuePair(1, typeof(string), "bbb");
            simpleList.AddKeyValuePair(2, typeof(string), "ccc");
            simpleList.AddKeyValuePair(3, typeof(string), "ddd");

            var complexList = explorer.AddChildObject("complex_list");
            for (var i = 0; i < 5; i++)
            {
                complexList.AddChildObject(i, "ChildObject").AddKeyValuePair("key", typeof(string), "value");
            }
        }
    }
}
