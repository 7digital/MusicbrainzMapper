﻿using System.Diagnostics;
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
            var taskEndTimeTask = ToTest().ContinueWith(task => stopwatch.ElapsedTicks);
            var taskStartTime = stopwatch.ElapsedTicks;

            var taskEndTime = await taskEndTimeTask;
            stopwatch.Stop();

            var taskRunTime = taskEndTime - taskStartTime;

            Assert.That(taskStartTime, Is.LessThan(taskRunTime));
        }

        protected abstract Task ToTest();
    }
}