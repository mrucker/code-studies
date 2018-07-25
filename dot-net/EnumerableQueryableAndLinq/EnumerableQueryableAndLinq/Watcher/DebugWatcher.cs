using System.Diagnostics;

namespace EnumerableQueryableAndLinq.Watcher
{
    class DebugWatcher:IWatcher
    {
        public void Notify(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
