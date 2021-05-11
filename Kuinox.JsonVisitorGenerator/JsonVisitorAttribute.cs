using System;
using System.Collections.Generic;
using System.Text;

namespace Kuinox.JsonVisitorGenerator
{
    [AttributeUsage( AttributeTargets.Class )]
    public sealed class JsonVisitorAttribute : Attribute
    {
        public JsonVisitorAttribute( string jsonVisitorPath )
            => JsonVisitorPath = jsonVisitorPath;
        public string JsonVisitorPath { get; }
    }
}
