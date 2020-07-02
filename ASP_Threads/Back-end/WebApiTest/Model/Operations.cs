using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiTest.Extensions;

namespace WebApiTest.Model
{
    public static class Operations
    {
        private static async Task ExecSqlQueryAsync()
        {
            //here some sql query
            await Task.Delay(1000);
        }
        private static async Task DeserializeFromAsync()
        {
            //here some deserialization action
            await Task.Delay(1500);
        }
        private static void OperationCompleted()
        {
            //here some log action
            Thread.Sleep(1000);
        }
        public static async Task InitializeSystem()
        {
            //some initialization after going by url
            await new [] { ExecSqlQueryAsync(), DeserializeFromAsync() }.After(OperationCompleted);
        }

        public static async Task SyncOperations()
        {
            var actions = new List<Action<Barrier>> {Method1, Method2};
            await actions.RunWithSync(new Barrier(actions.Count));
        }
        private static void Method1(Barrier barrier)
        {
            for (int i = 0; i < ITERATIONS; i++)
            {
                Thread.Sleep(100);

                barrier.SignalAndWait();
            }
        }

        private static readonly int ITERATIONS = 10;
        private static void Method2(Barrier barrier)
        {
            for (int i = 0; i < ITERATIONS; i++)
            {
                Thread.Sleep(200);

                barrier.SignalAndWait();
            }
        }
        public static async Task RunWithTimeout(int timeout = 2000)
        {
            var tokenSource = new CancellationTokenSource();

            Action<CancellationToken> process = Process;
            var operation = process.RunWithCancellation(tokenSource.Token, () =>
            {
                Thread.Sleep(500);
            });
            var timeoutTask = Task.Run(() =>
            {
                Thread.Sleep(timeout);
                tokenSource.Cancel();
            });
            var completed = Task.WhenAny(operation, timeoutTask);
            await completed;
        }
        private static void Process(CancellationToken token)
        {
            var rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                if (token.IsCancellationRequested)
                    return;
                //some long action
                Thread.Sleep(rnd.Next(200,600));
            }
        }
    }
}
