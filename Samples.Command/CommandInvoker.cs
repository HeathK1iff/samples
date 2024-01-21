
namespace Samples.Command
{
    public class CommandInvoker
    {
        private ICommand _command;

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public void InvokeAll()
        {
            _command.Execute();
        }
    }

}