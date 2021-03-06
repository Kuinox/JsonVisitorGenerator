using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Kuinox.JsonVisitorGenerator
{
    partial class SourceGenerator
    {

        static bool IsBaseType( JProperty property ) => IsBaseType( property.Value as JObject );
        static bool IsBaseType( JObject property )
            => property["type"]?.ToString() is "boolean" or "string" or "uri" or "number";
        static bool IsArray( JObject property )
            => property["type"]?.ToString() is "array";
        static string GetBaseTypeReaderExpression( JObject property )
            => (property["type"]?.ToString()) switch
            {
                "boolean" => "reader.GetBoolean()",
                "string" => "reader.GetString()",
                "uri" => "new Uri(reader.GetString())",
                "number" => "reader.GetDouble()",
                _ => throw new InvalidOperationException( "Unknown base type or not a valid json type." )
            };

        static string CamelToPascal( string name )
         => string.IsNullOrWhiteSpace( name ) ?
           name
         : name.Substring( 0, 1 ).ToUpper() + name.Substring( 1 );
        static string PascalToCamel( string name )
           => string.IsNullOrWhiteSpace( name ) ?
             name
           : name.Substring( 0, 1 ).ToLower() + name.Substring( 1 );
        string GetMethodThatProcess( JObject typeDef, string methodName )
            => methodName + JSTypeToCS( typeDef, null ).ToString() + "(reader)";


        /// <param name="propertyName">When the type described is an object, there may be no given name of this object.
        /// The property name of the type definiton must be given, so it will be used to identify the object name.</param>
        string JSTypeToCS( JObject typeDefinition, string? propertyName )
        {
            string typeStr = (typeDefinition["type"] ?? typeDefinition["$ref"]).ToString();
            switch( typeStr )
            {
                case "uri":
                    return "Uri";
                case "boolean":
                    return "bool";
                case "string":
                    return "string";
                case "array":
                    return $"IEnumerable<{JSTypeToCS( typeDefinition["items"] as JObject, null )}>";
                case "object":
                    if( string.IsNullOrWhiteSpace( propertyName ) ) throw new InvalidOperationException();
                    return propertyName;
                default:
                    if( typeStr[0] == '#' )
                    {
                        return StringToCName( typeStr );
                    }
                    return "TODO_UNION_TYPE";
            };
        }

        string StringToCName( string str ) => str.Replace( "$", "_d_" ).Replace( "/", "_" ).Replace( "#", "_n_" );


    }
}
