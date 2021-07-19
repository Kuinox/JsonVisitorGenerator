namespace Schema
{
    public class Schema
    {
        /// <summary>
        /// In JSON, this property is named "id"
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// In JSON, this property is named "$schema"
        /// </summary>
        public string Schema0 { get; set; }
        /// <summary>
        /// In JSON, this property is named "title"
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// In JSON, this property is named "description"
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// In JSON, this property is named "default"
        /// </summary>
        public object Default { get; set; }
        /// <summary>
        /// In JSON, this property is named "multipleOf"
        /// </summary>
        public double MultipleOf { get; set; }
        /// <summary>
        /// In JSON, this property is named "maximum"
        /// </summary>
        public double Maximum { get; set; }
        /// <summary>
        /// In JSON, this property is named "exclusiveMaximum"
        /// </summary>
        public bool ExclusiveMaximum { get; set; }
        /// <summary>
        /// In JSON, this property is named "minimum"
        /// </summary>
        public double Minimum { get; set; }
        /// <summary>
        /// In JSON, this property is named "exclusiveMinimum"
        /// </summary>
        public bool ExclusiveMinimum { get; set; }
        /// <summary>
        /// In JSON, this property is named "maxLength"
        /// </summary>
        public Schema.PositiveInteger.PositiveInteger MaxLength { get; set; }
        /// <summary>
        /// In JSON, this property is named "minLength"
        /// </summary>
        public Schema.PositiveIntegerDefault0.PositiveIntegerDefault0 MinLength { get; set; }
        /// <summary>
        /// In JSON, this property is named "pattern"
        /// </summary>
        public string Pattern { get; set; }
        /// <summary>
        /// In JSON, this property is named "additionalItems"
        /// </summary>
        public TODO_NOTSIMPLE AdditionalItems { get; set; }
        /// <summary>
        /// In JSON, this property is named "items"
        /// </summary>
        public TODO_NOTSIMPLE Items { get; set; }
        /// <summary>
        /// In JSON, this property is named "maxItems"
        /// </summary>
        public Schema.PositiveInteger.PositiveInteger MaxItems { get; set; }
        /// <summary>
        /// In JSON, this property is named "minItems"
        /// </summary>
        public Schema.PositiveIntegerDefault0.PositiveIntegerDefault0 MinItems { get; set; }
        /// <summary>
        /// In JSON, this property is named "uniqueItems"
        /// </summary>
        public bool UniqueItems { get; set; }
        /// <summary>
        /// In JSON, this property is named "maxProperties"
        /// </summary>
        public Schema.PositiveInteger.PositiveInteger MaxProperties { get; set; }
        /// <summary>
        /// In JSON, this property is named "minProperties"
        /// </summary>
        public Schema.PositiveIntegerDefault0.PositiveIntegerDefault0 MinProperties { get; set; }
        /// <summary>
        /// In JSON, this property is named "required"
        /// </summary>
        public Schema.StringArray.StringArray Required { get; set; }
        /// <summary>
        /// In JSON, this property is named "additionalProperties"
        /// </summary>
        public TODO_NOTSIMPLE AdditionalProperties { get; set; }
        /// <summary>
        /// In JSON, this property is named "definitions"
        /// </summary>
        public TODO_OBJECT Definitions { get; set; }
        /// <summary>
        /// In JSON, this property is named "properties"
        /// </summary>
        public TODO_OBJECT Properties { get; set; }
        /// <summary>
        /// In JSON, this property is named "patternProperties"
        /// </summary>
        public TODO_OBJECT PatternProperties { get; set; }
        /// <summary>
        /// In JSON, this property is named "enum"
        /// </summary>
        public TODO_ARRAY Enum { get; set; }
        /// <summary>
        /// In JSON, this property is named "type"
        /// </summary>
        public TODO_NOTSIMPLE Type { get; set; }
        /// <summary>
        /// In JSON, this property is named "format"
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// In JSON, this property is named "allOf"
        /// </summary>
        public Schema.SchemaArray.SchemaArray AllOf { get; set; }
        /// <summary>
        /// In JSON, this property is named "anyOf"
        /// </summary>
        public Schema.SchemaArray.SchemaArray AnyOf { get; set; }
        /// <summary>
        /// In JSON, this property is named "oneOf"
        /// </summary>
        public Schema.SchemaArray.SchemaArray OneOf { get; set; }
        /// <summary>
        /// In JSON, this property is named "not"
        /// </summary>
        public Schema.Schema Not { get; set; }
    }
}
namespace Schema.SchemaArray { public class SchemaArray { } }
namespace Schema.PositiveInteger { public class PositiveInteger { } }
namespace Schema.PositiveIntegerDefault0 { public class PositiveIntegerDefault0 { } }
namespace Schema.SimpleTypes { public class SimpleTypes { } }
namespace Schema.StringArray { public class StringArray { } }
