
namespace Samples.Command
{
    public class LedOffCommand : ICommand
    {
        private ILamp _lamp;
        public LedOffCommand(ILamp lamp)
        {
            _lamp = lamp;
        }

        public void Execute()
        {
            _lamp.SetOff();
        }
    }

}