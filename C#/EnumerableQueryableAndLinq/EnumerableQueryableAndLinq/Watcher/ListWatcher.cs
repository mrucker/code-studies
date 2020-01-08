using System.Collections.Generic;


namespace EnumerableQueryableAndLinq.Watcher
{
    class ListWatcher: IWatcher
    {
        private readonly List<string> _messages = new List<string>();
        public IEnumerable<string> Messages { get { return _messages; } }

        public void Notify(string message)
        {
            _messages.Add(message);
        }
    }
}
