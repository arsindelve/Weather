using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Weather.Common
{
    public static class ExecutionHelper
    {
        public static async Task RunTaskForTimespan(TimeSpan timespan, Action a)
        {
            TaskFactory tf = new TaskFactory();
            await tf.StartNew(() =>
            {
                Stopwatch s = new Stopwatch();
                s.Start();
                while (s.Elapsed < timespan)
                {
                    a();
                }

                s.Stop();
            });
        }
    }
}
