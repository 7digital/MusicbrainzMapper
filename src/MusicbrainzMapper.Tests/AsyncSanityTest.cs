using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MusicbrainzMapper.Tests
{
    public abstract class AsyncSanityTest
    {
        public class TaskTiming
        {
            public long CreationTime;
            public long CompletionTime;
        }

        [Test, Explicit]
        public async void TaskCreationIsQuickerThanExecution()
        {
            var taskTiming = await GetTaskTiming();

            Assert.That(taskTiming.CreationTime, Is.LessThan(taskTiming.CompletionTime));
        }

        public async Task<TaskTiming> GetTaskTiming()
        {
            var stopwatch = Stopwatch.StartNew();

            var timingTask = ToTest().ContinueWith(task => stopwatch.ElapsedTicks);

            var taskCreationTime = stopwatch.ElapsedTicks;

            var taskCreationAndCompletionTime = await timingTask;

            var taskCompletionTime = taskCreationAndCompletionTime - taskCreationTime;

            return new TaskTiming
                       {
                           CreationTime = taskCreationTime,
                           CompletionTime = taskCompletionTime
                       };
        }

        protected abstract Task ToTest();
    }
}