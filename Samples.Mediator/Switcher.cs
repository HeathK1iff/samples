namespace Samples.Mediator
{
    public class Switcher : ISwitchable, IObservable<bool>
    {
        private HashSet<IObserver<bool>> _observers = new();
        public void SetOff()
        {
            NotifyAll(false);
        }

        public void SetOn()
        {
            NotifyAll(true);
        }

        private void NotifyAll(bool state)
        {
            foreach (var observer in _observers) 
            {
                observer.OnNext(state);
            }
        }

        public IDisposable Subscribe(IObserver<bool> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }

            return new Unsubscriber(_observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private HashSet<IObserver<bool>> _observers;
            private IObserver<bool> _observer;

            public Unsubscriber(HashSet<IObserver<bool>> observers, IObserver<bool> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                _observers.Remove(_observer);
                _observer = null;
            }
        }

    }
}