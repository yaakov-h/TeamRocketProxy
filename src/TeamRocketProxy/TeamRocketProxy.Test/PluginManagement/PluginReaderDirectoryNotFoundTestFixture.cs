using System.IO;
using NUnit.Framework;
using TeamRocketProxy.PluginManagement;

namespace TeamRocketProxy.Test.PluginManagement
{
    class PluginReaderNoFilesTestFixture
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
            Directory.CreateDirectory(directory);

            reader = new PluginReader(directory);
        }

        [OneTimeSetUp]
        public void TearDownDirectory()
        {
            try
            {
                Directory.Delete(directory, recursive: true);
            }
            catch (DirectoryNotFoundException)
            {
            }
        }
    }
}
