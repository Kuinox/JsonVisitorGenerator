namespace Kuinox.JsonVisitorGenerator
{
    public partial class SchemaVisitor
    {
        public enum ChildKind
        {
            Invalid,
            Root,
            Definition,
            Property,
            SubType,
            AdditionalProperty,
            Dependency
        }
    }
}
