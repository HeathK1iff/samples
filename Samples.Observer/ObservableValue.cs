namespace Samples.Observer
{
    public class ObservableValue : IObservable<ObservableValue>
    {
        private int _value;
        private HashSet<IObserver<ObservableValue>> _observers = new();

        public int CurrentValue 
        {
            get => _value;
            set
            {
                bool isChanged = _value != value;
                _value = value;
                
                if (isChanged)
                {
                    NotifyAll();
                }
            }
        }

        private void NotifyAll()
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(this);
            }
        }


        public IDisposable Subscribe(IObserver<ObservableValue> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(_observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            HashSet<IObserver<ObservableValue>> _observers;
            IObserver<ObservableValue> _observer;

            public Unsubscriber(HashSet<IObserver<ObservableValue>> observers, IObserver<ObservableValue> observer) 
            { 
                _observer = observer;
                _observers = observers;
            }

            public void Dispose()
            {
                _observers.Remove(_observer);
                _observer = null;
            }
        }
    }
}