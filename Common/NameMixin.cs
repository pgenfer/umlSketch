namespace Common
{
    public class NameMixin : INamed
    {
        public string Name { get; set; }
        public override string ToString() => Name;
    }
}
