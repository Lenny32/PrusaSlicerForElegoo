namespace Convertor.Lib.GCode.Command.G
{
    public abstract class GCommand : ICommand
    {
        public abstract string GetClassName();
        //public override string ToString()
        //{
        //    return $"{GetClassName()} ";
        //}
    }
}
