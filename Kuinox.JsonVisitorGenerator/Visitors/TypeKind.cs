using System;

namespace Kuinox.JsonVisitorGenerator
{
    public partial class SchemaVisitor
    {
        [Flags]
        public enum TypeKind
        {
            EmptyObject = 0,
            Simple = 1,
            Union = 1 << 1,
            Reference = 1 << 2,
            Constant = 1 << 3,
            Constraint = 1 << 4
        }
    }
}
