namespace Convertor.Lib.GCode.Command.G
{
    public class G4 : GCommand
    {
        public int Value { get; set; }
        public override string GetClassName()
        {
            return this.GetType().Name;
        }
        public override string ToString()
        {
            return $"G4 P{Value}; wait {Value}ms";
        }
    }
}
