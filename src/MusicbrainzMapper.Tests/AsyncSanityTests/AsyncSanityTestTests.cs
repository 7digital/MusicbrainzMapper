using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MusicbrainzMapper.Tests.AsyncSanityTests
{
    [TestFixture]
    public class AsyncSanityTestTests
    {
        public class GoodAsync: AsyncSanityTest
        {
            protected override Task ToTest()
            {
                return Task.Factory.StartNew(() => Thread.Sleep(100));
            }
        }

        public class BadAsync: AsyncSanityTest
        {
            protected override Task ToTest()
            {
                var task = Task.Factory.StartNew(() => Thread.Sleep(100));
                task.Wait();
                return task;
            }
        }

        [Test]
        public async void CreationIsQuickerThanCompletionForGoodAsync()
        {
            var toTest = new GoodAsync();
            var timing = await toTest.GetTaskTiming();

            Assert.That(timing.CreationTime, Is.LessThan(timing.CompletionTime));
        }

        [Test]
        public async void CreationIsSlowerThanCompletionForBadAsync()
        {
            var toTest = new BadAsync();
            var timing = await toTest.GetTaskTiming();

            Assert.That(timing.CreationTime, Is.GreaterThan(timing.CompletionTime));
        }

    }
}