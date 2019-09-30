namespace Convertor.Lib.GCode.Command.G
{
    public class G21 : GCommand
    {

        public override string GetClassName()
        {
            return "G21; set units to millimeters";
        }
    }
}
