﻿
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Quick.Net.Tasks
{
    public class Job2TimedService : IHostedService, IDisposable
    {
        private Timer _timer;

        // 这里可以注入
        public Job2TimedService()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Job 2 is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(60 * 60 * 2));//两个小时

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            //实现业务逻辑
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Job 2 is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
