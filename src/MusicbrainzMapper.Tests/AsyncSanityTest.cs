using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    public abstract class AsyncSanityTest
    {
        [Test, Explicit]
        public async void TaskCreationIsQuickerThanExecution()
        {
            var stopwatch = Stopwatch.StartNew();
            var taskCompletionTimeTask = ToTest().ContinueWith(task => stopwatch.ElapsedTicks);

            var taskCreationTime = stopwatch.ElapsedTicks;

            stopwatch.Restart();
            var taskCompletionTime = await taskCompletionTimeTask;

            Assert.That(taskCreationTime, Is.LessThan(taskCompletionTime));
        }

        protected abstract Task ToTest();
    }
}