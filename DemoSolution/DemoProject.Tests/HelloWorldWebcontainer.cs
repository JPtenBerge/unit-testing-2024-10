using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Tests
{
    [TestClass]
    public class HelloWorldWebcontainer
    {
        static IContainer _container = new ContainerBuilder()
            // Set the image for the container to "testcontainers/helloworld:1.1.0".
            .WithImage("testcontainers/helloworld:1.1.0")
            // Bind port 8080 of the container to a random port on the host.
            .WithPortBinding(8080, true)
            // Wait until the HTTP endpoint of the container is available.
            .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(8080)))
            // Build the container configuration.
            .Build();

        [ClassInitialize]
        public static async Task InitAll(TestContext ctx)
        {
            await _container.StartAsync();
        }

        [TestMethod]
        public async Task MyFirstTestcontainer()
        {
            var httpClient = new HttpClient();
            var requestUri =
                new UriBuilder(
                    Uri.UriSchemeHttp,
                    _container.Hostname,
                    _container.GetMappedPublicPort(8080)
                ).Uri;
            var html = await httpClient.GetStringAsync(requestUri);
            Assert.AreEqual("hoi", html);
        }
    }
}
