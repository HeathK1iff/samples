
namespace Samples.Command
{
    public class LedOnCommand : ICommand
    {
        private ILamp _lamp;
        public LedOnCommand(ILamp lamp)
        {
            _lamp = lamp;
        }

        public void Execute()
        {
            _lamp.SetOn();
        }
    }

}