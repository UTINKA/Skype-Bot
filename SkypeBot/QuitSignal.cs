using System.Threading;

namespace SkypeBot
{
    public class QuitSignal
    {
        readonly ManualResetEvent signal = new ManualResetEvent(false);

        public void Wait()
        {
            signal.WaitOne();
        }

        public void Quit()
        {
            signal.Set();
        }
    }
}
