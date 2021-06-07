using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System;
/// <summary>Tool specification schema file by NUKE<br></summary>
public class JsonVisitor
{/// <summary></summary>
    public JsonVisitor Visit( Utf8JsonReader reader )
    {
        reader.Read(); // Open Object.
        reader.Read(); // first property.
        while( reader.TokenType != JsonTokenType.EndObject )
        {
            string property = reader.GetString();
            switch( property )
            {
                //TODO.
                default:
                    throw new InvalidDataException( "Unknown property" );
            }
        }
        throw new NotImplementedException();
    }
    /// <summary></summary>
    public JsonVisitor Read( Utf8JsonReader reader )
    {
        reader.Read(); // Open Object.
        reader.Read(); // first property.
        while( reader.TokenType != JsonTokenType.EndObject )
        {
            string property = reader.GetString();
            switch( property )
            {
                //TODO.
                default:
                    throw new InvalidDataException( "Unknown property" );
            }
        }
        throw new NotImplementedException();
    }
    /// <summary></summary>
    protected string Visit$schema( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary></summary>
    protected string Read$schema( Utf8JsonReader reader )
    {
        return reader.GetString();
        reader.Read();
    }
    /// <summary>Contains all references on which this definition is based on. Allows checking for updates.</summary>
    protected IEnumerable<string> VisitReferences( Utf8JsonReader reader )
    {
        reader.Read();
        reader.Read();
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            reader.Read();
        }
        return array0;
    }
    /// <summary>Contains all references on which this definition is based on. Allows checking for updates.</summary>
    protected IEnumerable<string> ReadReferences( Utf8JsonReader reader )
    {
        List<string> array0 = new List<string>();
        reader.Read();
        reader.Read();
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            array0.Add( reader.GetString() );
            reader.Read();
        }
        return array0;
    }
    /// <summary></summary>
    protected IEnumerable<string> VisitImports( Utf8JsonReader reader )
    {
        reader.Read();
        reader.Read();
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            reader.Read();
        }
        return array0;
    }
    /// <summary></summary>
    protected IEnumerable<string> ReadImports( Utf8JsonReader reader )
    {
        List<string> array0 = new List<string>();
        reader.Read();
        reader.Read();
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            array0.Add( reader.GetString() );
            reader.Read();
        }
        return array0;
    }
    /// <summary>Name of the Tool.</summary>
    protected string VisitName( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>Name of the Tool.</summary>
    protected string ReadName( Utf8JsonReader reader )
    {
        return reader.GetString();
        reader.Read();
    }
    /// <summary>Url to the official website.</summary>
    protected string VisitOfficialUrl( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>Url to the official website.</summary>
    protected string ReadOfficialUrl( Utf8JsonReader reader )
    {
        return reader.GetString();
        reader.Read();
    }
    /// <summary>Obsolete message. Tool is marked as obsolete when specified.</summary>
    protected string VisitDeprecationMessage( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>Obsolete message. Tool is marked as obsolete when specified.</summary>
    protected string ReadDeprecationMessage( Utf8JsonReader reader )
    {
        return reader.GetString();
        reader.Read();
    }
    /// <summary>Help or introduction text to for the tool. Supports 'a-href', 'c', 'em', 'b', 'ul', 'li' and 'para' tags for better formatting.</summary>
    protected string VisitHelp( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>Help or introduction text to for the tool. Supports 'a-href', 'c', 'em', 'b', 'ul', 'li' and 'para' tags for better formatting.</summary>
    protected string ReadHelp( Utf8JsonReader reader )
    {
        return reader.GetString();
        reader.Read();
    }
    /// <summary>ID for the NuGet package.</summary>
    protected string VisitPackageId( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>ID for the NuGet package.</summary>
    protected string ReadPackageId( Utf8JsonReader reader )
    {
        return reader.GetString();
        reader.Read();
    }
    /// <summary>Exact name of the main executable found in the './tools' folder. Case-sensitive.</summary>
    protected string VisitPackageExecutable( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>Exact name of the main executable found in the './tools' folder. Case-sensitive.</summary>
    protected string ReadPackageExecutable( Utf8JsonReader reader )
    {
        return reader.GetString();
        reader.Read();
    }
    /// <summary>Exact name of the executable that can be found via 'where' or 'which'.</summary>
    protected string VisitPathExecutable( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>Exact name of the executable that can be found via 'where' or 'which'.</summary>
    protected string ReadPathExecutable( Utf8JsonReader reader )
    {
        return reader.GetString();
        reader.Read();
    }
    /// <summary>Defines that locating the executable is implemented customly.</summary>
    protected bool VisitCustomExecutable( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>Defines that locating the executable is implemented customly.</summary>
    protected bool ReadCustomExecutable( Utf8JsonReader reader )
    {
        return reader.GetBoolean();
        reader.Read();
    }
    /// <summary>Enables custom logger.</summary>
    protected bool VisitCustomLogger( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>Enables custom logger.</summary>
    protected bool ReadCustomLogger( Utf8JsonReader reader )
    {
        return reader.GetBoolean();
        reader.Read();
    }
    /// <summary>Help or introduction text to for the tool. Can contain HTML tags for better formatting.</summary>
    protected IEnumerable<JsonVisitor.Task> VisitTasks( Utf8JsonReader reader )
    {
        reader.Read();
        reader.Read();
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            JsonVisitor.Task.Visit( reader );
        }
        return array0;
    }
    /// <summary>Help or introduction text to for the tool. Can contain HTML tags for better formatting.</summary>
    protected IEnumerable<JsonVisitor.Task> ReadTasks( Utf8JsonReader reader )
    {
        List<JsonVisitor.Task> array0 = new List<JsonVisitor.Task>();
        reader.Read();
        reader.Read();
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            array0.Add( JsonVisitor.Task.Read( reader ) );
        }
        return array0;
    }
    /// <summary>Common properties for all tasks.</summary>
    protected IEnumerable<JsonVisitor.Property> VisitCommonTaskProperties( Utf8JsonReader reader )
    {
        reader.Read();
        reader.Read();
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            JsonVisitor.Property.Visit( reader );
        }
        return array0;
    }
    /// <summary>Common properties for all tasks.</summary>
    protected IEnumerable<JsonVisitor.Property> ReadCommonTaskProperties( Utf8JsonReader reader )
    {
        List<JsonVisitor.Property> array0 = new List<JsonVisitor.Property>();
        reader.Read();
        reader.Read();
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            array0.Add( JsonVisitor.Property.Read( reader ) );
        }
        return array0;
    }
    /// <summary>Named common property sets which can be used by tasks.</summary>
    protected CommonTaskPropertySets VisitCommonTaskPropertySets( Utf8JsonReader reader )
    {
        reader.Read(); // Open Object.
        reader.Read(); // first property.
        while( reader.TokenType != JsonTokenType.EndObject )
        {
            string property = reader.GetString();
            switch( property )
            {
                //TODO.
                default:
                    throw new InvalidDataException( "Unknown property" );
            }
        }
        throw new NotImplementedException();
    }
    /// <summary>Named common property sets which can be used by tasks.</summary>
    protected CommonTaskPropertySets ReadCommonTaskPropertySets( Utf8JsonReader reader )
    {
        reader.Read(); // Open Object.
        reader.Read(); // first property.
        while( reader.TokenType != JsonTokenType.EndObject )
        {
            string property = reader.GetString();
            switch( property )
            {
                //TODO.
                default:
                    throw new InvalidDataException( "Unknown property" );
            }
        }
        throw new NotImplementedException();
    }
    /// <summary>Common used data classes.</summary>
    protected IEnumerable<JsonVisitor.DataClass> VisitDataClasses( Utf8JsonReader reader )
    {
        reader.Read();
        reader.Read();
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            JsonVisitor.DataClass.Visit( reader );
        }
        return array0;
    }
    /// <summary>Common used data classes.</summary>
    protected IEnumerable<JsonVisitor.DataClass> ReadDataClasses( Utf8JsonReader reader )
    {
        List<JsonVisitor.DataClass> array0 = new List<JsonVisitor.DataClass>();
        reader.Read();
        reader.Read();
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            array0.Add( JsonVisitor.DataClass.Read( reader ) );
        }
        return array0;
    }
    /// <summary>Used enumerations.</summary>
    protected IEnumerable<JsonVisitor.Enumeration> VisitEnumerations( Utf8JsonReader reader )
    {
        reader.Read();
        reader.Read();
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            JsonVisitor.Enumeration.Visit( reader );
        }
        return array0;
    }
    /// <summary>Used enumerations.</summary>
    protected IEnumerable<JsonVisitor.Enumeration> ReadEnumerations( Utf8JsonReader reader )
    {
        List<JsonVisitor.Enumeration> array0 = new List<JsonVisitor.Enumeration>();
        reader.Read();
        reader.Read();
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            array0.Add( JsonVisitor.Enumeration.Read( reader ) );
        }
        return array0;
    }
    /// <summary>Can be used to store additional information about the tool.</summary>
    protected _metadata Visit_metadata( Utf8JsonReader reader )
    {
        reader.Read(); // Open Object.
        reader.Read(); // first property.
        while( reader.TokenType != JsonTokenType.EndObject )
        {
            string property = reader.GetString();
            switch( property )
            {
                //TODO.
                default:
                    throw new InvalidDataException( "Unknown property" );
            }
        }
        throw new NotImplementedException();
    }
    /// <summary>Can be used to store additional information about the tool.</summary>
    protected _metadata Read_metadata( Utf8JsonReader reader )
    {
        reader.Read(); // Open Object.
        reader.Read(); // first property.
        while( reader.TokenType != JsonTokenType.EndObject )
        {
            string property = reader.GetString();
            switch( property )
            {
                //TODO.
                default:
                    throw new InvalidDataException( "Unknown property" );
            }
        }
        throw new NotImplementedException();
    }
    /// <summary><br></summary>
    public class DataClass
    {/// <summary></summary>
        public DataClass Visit( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary></summary>
        public DataClass Read( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary>Name of the data class.</summary>
        protected string VisitName( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Name of the data class.</summary>
        protected string ReadName( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>The base class to inherit from.</summary>
        protected string VisitBaseClass( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>The base class to inherit from.</summary>
        protected string ReadBaseClass( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Enables generation of extension methods for modification.</summary>
        protected bool VisitExtensionMethods( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Enables generation of extension methods for modification.</summary>
        protected bool ReadExtensionMethods( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Omits generation of the data class.</summary>
        protected bool VisitOmitDataClass( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Omits generation of the data class.</summary>
        protected bool ReadOmitDataClass( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Properties of the data class.</summary>
        protected IEnumerable<JsonVisitor.Property> VisitProperties( Utf8JsonReader reader )
        {
            reader.Read();
            reader.Read();
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                JsonVisitor.Property.Visit( reader );
            }
            return array0;
        }
        /// <summary>Properties of the data class.</summary>
        protected IEnumerable<JsonVisitor.Property> ReadProperties( Utf8JsonReader reader )
        {
            List<JsonVisitor.Property> array0 = new List<JsonVisitor.Property>();
            reader.Read();
            reader.Read();
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                array0.Add( JsonVisitor.Property.Read( reader ) );
            }
            return array0;
        }
        /// <summary>Obsolete message. DataClass is marked as obsolete when specified.</summary>
        protected string VisitDeprecationMessage( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Obsolete message. DataClass is marked as obsolete when specified.</summary>
        protected string ReadDeprecationMessage( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
    }    /// <summary><br></summary>
    public class Enumeration
    {/// <summary></summary>
        public Enumeration Visit( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary></summary>
        public Enumeration Read( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary>Name of the enumeration.</summary>
        protected string VisitName( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Name of the enumeration.</summary>
        protected string ReadName( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>The enumeration values.</summary>
        protected IEnumerable<string> VisitValues( Utf8JsonReader reader )
        {
            reader.Read();
            reader.Read();
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                reader.Read();
            }
            return array0;
        }
        /// <summary>The enumeration values.</summary>
        protected IEnumerable<string> ReadValues( Utf8JsonReader reader )
        {
            List<string> array0 = new List<string>();
            reader.Read();
            reader.Read();
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                array0.Add( reader.GetString() );
                reader.Read();
            }
            return array0;
        }
        /// <summary>Obsolete message. Enumeration is marked as obsolete when specified.</summary>
        protected string VisitDeprecationMessage( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Obsolete message. Enumeration is marked as obsolete when specified.</summary>
        protected string ReadDeprecationMessage( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
    }    /// <summary><br></summary>
    public class Property
    {/// <summary></summary>
        public Property Visit( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary></summary>
        public Property Read( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary>Name of the property.</summary>
        protected string VisitName( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Name of the property.</summary>
        protected string ReadName( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Type of the property. I.e., bool, int, string, List<int>, Dictionary<string, object>, Lookup<string, int.></summary>
        protected string VisitType( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Type of the property. I.e., bool, int, string, List<int>, Dictionary<string, object>, Lookup<string, int.></summary>
        protected string ReadType( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Name used when serializing to JSON.</summary>
        protected string VisitJson( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Name used when serializing to JSON.</summary>
        protected string ReadJson( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Argument formatting for the property. '{value}' is replaced by the value of the property.</summary>
        protected string VisitFormat( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Argument formatting for the property. '{value}' is replaced by the value of the property.</summary>
        protected string ReadFormat( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Item formatting for dictionaries. '{key}' and '{value}' are replaced accordingly.</summary>
        protected string VisitItemFormat( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Item formatting for dictionaries. '{key}' and '{value}' are replaced accordingly.</summary>
        protected string ReadItemFormat( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Omits argument parsing.</summary>
        protected bool VisitNoArgument( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Omits argument parsing.</summary>
        protected bool ReadNoArgument( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Custom implementation of the property.</summary>
        protected bool VisitCustomImpl( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Custom implementation of the property.</summary>
        protected bool ReadCustomImpl( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Separator used for items of collection types.</summary>
        protected string VisitSeparator( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Separator used for items of collection types.</summary>
        protected string ReadSeparator( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Character that must be double-quoted.</summary>
        protected string VisitDisallowedCharacter( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Character that must be double-quoted.</summary>
        protected string ReadDisallowedCharacter( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Specifies if list items should be double quoted.</summary>
        protected bool VisitQuoteMultiple( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Specifies if list items should be double quoted.</summary>
        protected bool ReadQuoteMultiple( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Default value that will be used if no value is given.</summary>
        protected string VisitDefault( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Default value that will be used if no value is given.</summary>
        protected string ReadDefault( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Specifies that the value is secret and should be hidden in output.</summary>
        protected bool VisitSecret( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Specifies that the value is secret and should be hidden in output.</summary>
        protected bool ReadSecret( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Custom implementation of value presentation.</summary>
        protected bool VisitCustomValue( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Custom implementation of value presentation.</summary>
        protected bool ReadCustomValue( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Specifies that an overload for the property should be created.</summary>
        protected bool VisitCreateOverload( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Specifies that an overload for the property should be created.</summary>
        protected bool ReadCreateOverload( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Help text for the property.</summary>
        protected string VisitHelp( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Help text for the property.</summary>
        protected string ReadHelp( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Dictionary delegates for named properties.</summary>
        protected IEnumerable<JsonVisitor.Property> VisitDelegates( Utf8JsonReader reader )
        {
            reader.Read();
            reader.Read();
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                JsonVisitor.Property.Visit( reader );
            }
            return array0;
        }
        /// <summary>Dictionary delegates for named properties.</summary>
        protected IEnumerable<JsonVisitor.Property> ReadDelegates( Utf8JsonReader reader )
        {
            List<JsonVisitor.Property> array0 = new List<JsonVisitor.Property>();
            reader.Read();
            reader.Read();
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                array0.Add( JsonVisitor.Property.Read( reader ) );
            }
            return array0;
        }
        /// <summary>Obsolete message. Property is marked as obsolete when specified.</summary>
        protected string VisitDeprecationMessage( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Obsolete message. Property is marked as obsolete when specified.</summary>
        protected string ReadDeprecationMessage( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary></summary>
        protected bool VisitOnlyDelegates( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary></summary>
        protected bool ReadOnlyDelegates( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary></summary>
        protected bool VisitIsTailArgument( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary></summary>
        protected bool ReadIsTailArgument( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
    }    /// <summary><br>The settings of the task.</summary>
    public class SettingsClass
    {/// <summary>The settings of the task.</summary>
        public SettingsClass Visit( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary>The settings of the task.</summary>
        public SettingsClass Read( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary>Name of the data class.</summary>
        protected TODO_UNION_TYPE VisitName( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary>Name of the data class.</summary>
        protected TODO_UNION_TYPE ReadName( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary>The base class to inherit from.</summary>
        protected string VisitBaseClass( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>The base class to inherit from.</summary>
        protected string ReadBaseClass( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Enables generation of extension methods for modification.</summary>
        protected bool VisitExtensionMethods( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Enables generation of extension methods for modification.</summary>
        protected bool ReadExtensionMethods( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Omits generation of the data class.</summary>
        protected bool VisitOmitDataClass( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Omits generation of the data class.</summary>
        protected bool ReadOmitDataClass( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Properties of the data class.</summary>
        protected IEnumerable<JsonVisitor.Property> VisitProperties( Utf8JsonReader reader )
        {
            reader.Read();
            reader.Read();
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                JsonVisitor.Property.Visit( reader );
            }
            return array0;
        }
        /// <summary>Properties of the data class.</summary>
        protected IEnumerable<JsonVisitor.Property> ReadProperties( Utf8JsonReader reader )
        {
            List<JsonVisitor.Property> array0 = new List<JsonVisitor.Property>();
            reader.Read();
            reader.Read();
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                array0.Add( JsonVisitor.Property.Read( reader ) );
            }
            return array0;
        }
        /// <summary>Obsolete message. DataClass is marked as obsolete when specified.</summary>
        protected string VisitDeprecationMessage( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Obsolete message. DataClass is marked as obsolete when specified.</summary>
        protected string ReadDeprecationMessage( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
    }    /// <summary><br></summary>
    public class Task
    {/// <summary></summary>
        public Task Visit( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary></summary>
        public Task Read( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary>Help or introduction text to for the tool. Supports 'a-href', 'c', 'em', 'b', 'ul', 'li' and 'para' tags for better formatting.</summary>
        protected string VisitHelp( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Help or introduction text to for the tool. Supports 'a-href', 'c', 'em', 'b', 'ul', 'li' and 'para' tags for better formatting.</summary>
        protected string ReadHelp( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Postfix for the task alias.</summary>
        protected string VisitPostfix( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Postfix for the task alias.</summary>
        protected string ReadPostfix( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Return type of the task.</summary>
        protected string VisitReturnType( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Return type of the task.</summary>
        protected string ReadReturnType( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Skips appending of common task properties.</summary>
        protected bool VisitOmitCommonProperties( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Skips appending of common task properties.</summary>
        protected bool ReadOmitCommonProperties( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Appends the properties of the named property sets.</summary>
        protected IEnumerable<string> VisitCommonPropertySets( Utf8JsonReader reader )
        {
            reader.Read();
            reader.Read();
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                reader.Read();
            }
            return array0;
        }
        /// <summary>Appends the properties of the named property sets.</summary>
        protected IEnumerable<string> ReadCommonPropertySets( Utf8JsonReader reader )
        {
            List<string> array0 = new List<string>();
            reader.Read();
            reader.Read();
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                array0.Add( reader.GetString() );
                reader.Read();
            }
            return array0;
        }
        /// <summary>Generates a pre-process hook</summary>
        protected bool VisitPreProcess( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Generates a pre-process hook</summary>
        protected bool ReadPreProcess( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Generates a post-process hook</summary>
        protected bool VisitPostProcess( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Generates a post-process hook</summary>
        protected bool ReadPostProcess( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Enables log level parsing</summary>
        protected bool VisitLogLevelParsing( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Enables log level parsing</summary>
        protected bool ReadLogLevelParsing( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Custom start implementation.</summary>
        protected bool VisitCustomStart( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Custom start implementation.</summary>
        protected bool ReadCustomStart( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Custom process assertion implementation.</summary>
        protected bool VisitCustomAssertion( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Custom process assertion implementation.</summary>
        protected bool ReadCustomAssertion( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
            reader.Read();
        }
        /// <summary>Argument that will always be printed independently of any set property.</summary>
        protected string VisitDefiniteArgument( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Argument that will always be printed independently of any set property.</summary>
        protected string ReadDefiniteArgument( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary>Url of the task. If not specified, the tool url will be used.</summary>
        protected string VisitOfficialUrl( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Url of the task. If not specified, the tool url will be used.</summary>
        protected string ReadOfficialUrl( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
        /// <summary></summary>
        protected JsonVisitor.SettingsClass VisitSettingsClass( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary></summary>
        protected JsonVisitor.SettingsClass ReadSettingsClass( Utf8JsonReader reader )
        {
            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndObject )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
            throw new NotImplementedException();
        }
        /// <summary>Obsolete message. Task is marked as obsolete when specified.</summary>
        protected string VisitDeprecationMessage( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>Obsolete message. Task is marked as obsolete when specified.</summary>
        protected string ReadDeprecationMessage( Utf8JsonReader reader )
        {
            return reader.GetString();
            reader.Read();
        }
    }
}
