namespace Samples.Mediator
{
    public class AlertMediator
    {
        public AlertMediator(IObservable<bool> switchable, IAlert alertManager)
        {
            switchable.Subscribe(new SwithebaleObserver((state) =>
            {
                alertManager.DoAlert();
            }));
        }

        private class SwithebaleObserver : IObserver<bool>
        {
            private Action<bool> _action;

            public SwithebaleObserver(Action<bool> action)
            {
                _action = action;
            }

            public void OnCompleted()
            {
                throw new NotImplementedException();
            }

            public void OnError(Exception error)
            {
                throw new NotImplementedException();
            }

            public void OnNext(bool value)
            {
                _action?.Invoke(value);
            }
        }
    }
}