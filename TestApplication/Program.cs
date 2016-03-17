﻿namespace FluentScheduler.Tests.TestApplication
{
    using LLibrary;
    using System;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            ListenForStart();
            ListenForEnd();
            ListenForException();
            Initialize();
            Sleep();
        }

        private static void ListenForStart()
        {
            L.Register("[job start]", "\"{0}\" has started.");
            JobManager.JobStart += (schedule, e) => L.Log("[job start]", schedule.Name);
        }

        private static void ListenForEnd()
        {
            L.Register("[job end]", "\"{0}\" has ended{1}.");

            JobManager.JobEnd += (schedule, e) =>
                L.Log("[job end]", schedule.Name,
                    schedule.Duration > TimeSpan.FromSeconds(1) ?
                    " with duration of " + schedule.Duration : string.Empty);
        }

        private static void ListenForException()
        {
            L.Register("[job exception]", "An error just happened:" + Environment.NewLine + "{0}");
            JobManager.JobException += (sender, e) => L.Log("[job exception]", e.ExceptionObject);
        }

        private static void Initialize()
        {
            JobManager.Initialize(new MyRegistry());
        }

        private static void Sleep()
        {
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
