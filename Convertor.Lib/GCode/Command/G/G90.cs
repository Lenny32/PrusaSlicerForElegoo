namespace Convertor.Lib.GCode.Command.G
{
    public class G90 : GCommand
    {

        public override string GetClassName()
        {
            return "G90";
        }

        public override string ToString()
        {
            return "G90; Absolute Positioning";
        }
    }
}
