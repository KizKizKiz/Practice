using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiTest.Extensions
{
    public static class TaskActionExtensions
    {
        public static async Task RunWithCancellation(this Action<CancellationToken> action, CancellationToken token, Action actionIfCancel)
        {
            await Task.Run(() => action(token), token);

            if (token.IsCancellationRequested)
                actionIfCancel?.Invoke();
        }
        public static async Task After(this IEnumerable<Task> tasks, Action afterWork)
        {
            if(tasks == null)
                throw new ArgumentNullException(nameof(tasks));
            if(afterWork==null)
                throw new ArgumentNullException(nameof(afterWork));

            await Task.WhenAll(tasks.ToList());

            afterWork.Invoke();
        }

        public static async Task RunWithSync(this IEnumerable<Action<Barrier>> actions, Barrier barrier)
        {
            if (actions == null)
                throw new ArgumentNullException(nameof(actions));
            if (barrier == null)
                throw new ArgumentNullException(nameof(barrier));

            await Task.WhenAll(actions.Select(action => Task.Run(() => action(barrier))).ToList());
        }
    }
}
