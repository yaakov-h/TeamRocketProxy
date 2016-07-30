using System.IO;
using NUnit.Framework;
using TeamRocketProxy.PluginManagement;

namespace TeamRocketProxy.Test.PluginManagement
{
    class PluginReaderDirectoryNotFoundTestFixture
    {
        [Test]
        public void ReturnsNonNullValue()
        {
            Assert.That(reader.LoadPlugins(), Is.Not.Null);
        }

        [Test]
        public void HasNoPlugins()
        {
            Assert.That(reader.LoadPlugins(), Is.Empty);
        }

        PluginReader reader;
        string directory;

        [OneTimeSetUp]
        public void SetUpDirectory()
        {
            directory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            reader = new PluginReader(directory);
        }
    }
}
