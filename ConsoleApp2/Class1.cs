using System.Text.Json;
using System.Collections.Generic;
using System.IO;
/// <summary>
/// Tool specification schema file by NUKE
/// 
/// </summary>
public class JsonVisitor
{

    /// <summary>
    /// 
    /// </summary>
    public virtual void Visit( Utf8JsonReader reader )
    {
        VisitJsonVisitor( reader );
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>The parsed value.</returns>
    public JsonVisitor Read( Utf8JsonReader reader )
    {

        reader.Read(); // Open Object.
        reader.Read(); // first property.
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            string property = reader.GetString();
            switch( property )
            {
                //TODO.
                default:
                    throw new InvalidDataException( "Unknown property" );
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void Visit$schema( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected string Read$schema( Utf8JsonReader reader )
    {
        return reader.GetString();
    }

    /// <summary>
    /// Contains all references on which this definition is based on. Allows checking for updates.
    /// </summary>
    protected virtual void VisitReferences( Utf8JsonReader reader )
    {
        reader.Read(); //OpenArray
        reader.Read(); //First element

        while( reader.TokenType != JsonTokenType.EndArray )
        {
            reader.Read();
        }
    }
    /// <summary>
    /// Contains all references on which this definition is based on. Allows checking for updates.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected IEnumerable<string> ReadReferences( Utf8JsonReader reader )
    {
        List<string> array0 = new List<string>();
        reader.Read(); //OpenArray
        reader.Read(); //First element

        while( reader.TokenType != JsonTokenType.EndArray )
        {
            array0.Add( reader.GetString() );
            reader.Read();
        }
        return array0;
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void VisitImports( Utf8JsonReader reader )
    {
        reader.Read(); //OpenArray
        reader.Read(); //First element

        while( reader.TokenType != JsonTokenType.EndArray )
        {
            reader.Read();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected IEnumerable<string> ReadImports( Utf8JsonReader reader )
    {
        List<string> array0 = new List<string>();
        reader.Read(); //OpenArray
        reader.Read(); //First element

        while( reader.TokenType != JsonTokenType.EndArray )
        {
            array0.Add( reader.GetString() );
            reader.Read();
        }
        return array0;
    }

    /// <summary>
    /// Name of the Tool.
    /// </summary>
    protected virtual void VisitName( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>
    /// Name of the Tool.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected string ReadName( Utf8JsonReader reader )
    {
        return reader.GetString();
    }

    /// <summary>
    /// Url to the official website.
    /// </summary>
    protected virtual void VisitOfficialUrl( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>
    /// Url to the official website.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected string ReadOfficialUrl( Utf8JsonReader reader )
    {
        return reader.GetString();
    }

    /// <summary>
    /// Obsolete message. Tool is marked as obsolete when specified.
    /// </summary>
    protected virtual void VisitDeprecationMessage( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>
    /// Obsolete message. Tool is marked as obsolete when specified.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected string ReadDeprecationMessage( Utf8JsonReader reader )
    {
        return reader.GetString();
    }

    /// <summary>
    /// Help or introduction text to for the tool. Supports 'a-href', 'c', 'em', 'b', 'ul', 'li' and 'para' tags for better formatting.
    /// </summary>
    protected virtual void VisitHelp( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>
    /// Help or introduction text to for the tool. Supports 'a-href', 'c', 'em', 'b', 'ul', 'li' and 'para' tags for better formatting.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected string ReadHelp( Utf8JsonReader reader )
    {
        return reader.GetString();
    }

    /// <summary>
    /// ID for the NuGet package.
    /// </summary>
    protected virtual void VisitPackageId( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>
    /// ID for the NuGet package.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected string ReadPackageId( Utf8JsonReader reader )
    {
        return reader.GetString();
    }

    /// <summary>
    /// Exact name of the main executable found in the './tools' folder. Case-sensitive.
    /// </summary>
    protected virtual void VisitPackageExecutable( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>
    /// Exact name of the main executable found in the './tools' folder. Case-sensitive.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected string ReadPackageExecutable( Utf8JsonReader reader )
    {
        return reader.GetString();
    }

    /// <summary>
    /// Exact name of the executable that can be found via 'where' or 'which'.
    /// </summary>
    protected virtual void VisitPathExecutable( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>
    /// Exact name of the executable that can be found via 'where' or 'which'.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected string ReadPathExecutable( Utf8JsonReader reader )
    {
        return reader.GetString();
    }

    /// <summary>
    /// Defines that locating the executable is implemented customly.
    /// </summary>
    protected virtual void VisitCustomExecutable( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>
    /// Defines that locating the executable is implemented customly.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected bool ReadCustomExecutable( Utf8JsonReader reader )
    {
        return reader.GetBoolean();
    }

    /// <summary>
    /// Enables custom logger.
    /// </summary>
    protected virtual void VisitCustomLogger( Utf8JsonReader reader )
    {
        reader.Read();
    }
    /// <summary>
    /// Enables custom logger.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected bool ReadCustomLogger( Utf8JsonReader reader )
    {
        return reader.GetBoolean();
    }

    /// <summary>
    /// Help or introduction text to for the tool. Can contain HTML tags for better formatting.
    /// </summary>
    protected virtual void VisitTasks( Utf8JsonReader reader )
    {
        reader.Read(); //OpenArray
        reader.Read(); //First element

        while( reader.TokenType != JsonTokenType.EndArray )
        {
            JsonVisitor.Task.Visit( reader );
            reader.Read();
        }
    }
    /// <summary>
    /// Help or introduction text to for the tool. Can contain HTML tags for better formatting.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected IEnumerable<JsonVisitor.Task> ReadTasks( Utf8JsonReader reader )
    {
        List<JsonVisitor.Task> array0 = new List<JsonVisitor.Task>();
        reader.Read(); //OpenArray
        reader.Read(); //First element

        while( reader.TokenType != JsonTokenType.EndArray )
        {
            array0.Add( JsonVisitor.Task.Read( reader ) );
            reader.Read();
        }
        return array0;
    }

    /// <summary>
    /// Common properties for all tasks.
    /// </summary>
    protected virtual void VisitCommonTaskProperties( Utf8JsonReader reader )
    {
        reader.Read(); //OpenArray
        reader.Read(); //First element

        while( reader.TokenType != JsonTokenType.EndArray )
        {
            JsonVisitor.Property.Visit( reader );
            reader.Read();
        }
    }
    /// <summary>
    /// Common properties for all tasks.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected IEnumerable<JsonVisitor.Property> ReadCommonTaskProperties( Utf8JsonReader reader )
    {
        List<JsonVisitor.Property> array0 = new List<JsonVisitor.Property>();
        reader.Read(); //OpenArray
        reader.Read(); //First element

        while( reader.TokenType != JsonTokenType.EndArray )
        {
            array0.Add( JsonVisitor.Property.Read( reader ) );
            reader.Read();
        }
        return array0;
    }

    /// <summary>
    /// Named common property sets which can be used by tasks.
    /// </summary>
    protected virtual void VisitCommonTaskPropertySets( Utf8JsonReader reader )
    {
        VisitCommonTaskPropertySets( reader );
    }
    /// <summary>
    /// Named common property sets which can be used by tasks.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected CommonTaskPropertySets ReadCommonTaskPropertySets( Utf8JsonReader reader )
    {

        reader.Read(); // Open Object.
        reader.Read(); // first property.
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            string property = reader.GetString();
            switch( property )
            {
                //TODO.
                default:
                    throw new InvalidDataException( "Unknown property" );
            }
        }
    }

    /// <summary>
    /// Common used data classes.
    /// </summary>
    protected virtual void VisitDataClasses( Utf8JsonReader reader )
    {
        reader.Read(); //OpenArray
        reader.Read(); //First element

        while( reader.TokenType != JsonTokenType.EndArray )
        {
            JsonVisitor.DataClass.Visit( reader );
            reader.Read();
        }
    }
    /// <summary>
    /// Common used data classes.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected IEnumerable<JsonVisitor.DataClass> ReadDataClasses( Utf8JsonReader reader )
    {
        List<JsonVisitor.DataClass> array0 = new List<JsonVisitor.DataClass>();
        reader.Read(); //OpenArray
        reader.Read(); //First element

        while( reader.TokenType != JsonTokenType.EndArray )
        {
            array0.Add( JsonVisitor.DataClass.Read( reader ) );
            reader.Read();
        }
        return array0;
    }

    /// <summary>
    /// Used enumerations.
    /// </summary>
    protected virtual void VisitEnumerations( Utf8JsonReader reader )
    {
        reader.Read(); //OpenArray
        reader.Read(); //First element

        while( reader.TokenType != JsonTokenType.EndArray )
        {
            JsonVisitor.Enumeration.Visit( reader );
            reader.Read();
        }
    }
    /// <summary>
    /// Used enumerations.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected IEnumerable<JsonVisitor.Enumeration> ReadEnumerations( Utf8JsonReader reader )
    {
        List<JsonVisitor.Enumeration> array0 = new List<JsonVisitor.Enumeration>();
        reader.Read(); //OpenArray
        reader.Read(); //First element

        while( reader.TokenType != JsonTokenType.EndArray )
        {
            array0.Add( JsonVisitor.Enumeration.Read( reader ) );
            reader.Read();
        }
        return array0;
    }

    /// <summary>
    /// Can be used to store additional information about the tool.
    /// </summary>
    protected virtual void Visit_metadata( Utf8JsonReader reader )
    {
        Visit_metadata( reader );
    }
    /// <summary>
    /// Can be used to store additional information about the tool.
    /// </summary>
    /// <returns>The parsed value.</returns>
    protected _metadata Read_metadata( Utf8JsonReader reader )
    {

        reader.Read(); // Open Object.
        reader.Read(); // first property.
        while( reader.TokenType != JsonTokenType.EndArray )
        {
            string property = reader.GetString();
            switch( property )
            {
                //TODO.
                default:
                    throw new InvalidDataException( "Unknown property" );
            }
        }
    }
    /// <summary>
    /// Tool specification schema file by NUKE
    /// 
    /// </summary>
    public class DataClass
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual void Visit( Utf8JsonReader reader )
        {
            VisitDataClass( reader );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The parsed value.</returns>
        public DataClass Read( Utf8JsonReader reader )
        {

            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
        }

        /// <summary>
        /// Name of the data class.
        /// </summary>
        protected virtual void VisitName( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Name of the data class.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected string ReadName( Utf8JsonReader reader )
        {
            return reader.GetString();
        }

        /// <summary>
        /// The base class to inherit from.
        /// </summary>
        protected virtual void VisitBaseClass( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// The base class to inherit from.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected string ReadBaseClass( Utf8JsonReader reader )
        {
            return reader.GetString();
        }

        /// <summary>
        /// Enables generation of extension methods for modification.
        /// </summary>
        protected virtual void VisitExtensionMethods( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Enables generation of extension methods for modification.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected bool ReadExtensionMethods( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
        }

        /// <summary>
        /// Omits generation of the data class.
        /// </summary>
        protected virtual void VisitOmitDataClass( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Omits generation of the data class.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected bool ReadOmitDataClass( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
        }

        /// <summary>
        /// Properties of the data class.
        /// </summary>
        protected virtual void VisitProperties( Utf8JsonReader reader )
        {
            reader.Read(); //OpenArray
            reader.Read(); //First element

            while( reader.TokenType != JsonTokenType.EndArray )
            {
                JsonVisitor.Property.Visit( reader );
                reader.Read();
            }
        }
        /// <summary>
        /// Properties of the data class.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected IEnumerable<JsonVisitor.Property> ReadProperties( Utf8JsonReader reader )
        {
            List<JsonVisitor.Property> array0 = new List<JsonVisitor.Property>();
            reader.Read(); //OpenArray
            reader.Read(); //First element

            while( reader.TokenType != JsonTokenType.EndArray )
            {
                array0.Add( JsonVisitor.Property.Read( reader ) );
                reader.Read();
            }
            return array0;
        }

        /// <summary>
        /// Obsolete message. DataClass is marked as obsolete when specified.
        /// </summary>
        protected virtual void VisitDeprecationMessage( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Obsolete message. DataClass is marked as obsolete when specified.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected string ReadDeprecationMessage( Utf8JsonReader reader )
        {
            return reader.GetString();
        }
    }    /// <summary>
         /// Tool specification schema file by NUKE
         /// 
         /// </summary>
    public class Enumeration
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual void Visit( Utf8JsonReader reader )
        {
            VisitEnumeration( reader );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The parsed value.</returns>
        public Enumeration Read( Utf8JsonReader reader )
        {

            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
        }

        /// <summary>
        /// Name of the enumeration.
        /// </summary>
        protected virtual void VisitName( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Name of the enumeration.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected string ReadName( Utf8JsonReader reader )
        {
            return reader.GetString();
        }

        /// <summary>
        /// The enumeration values.
        /// </summary>
        protected virtual void VisitValues( Utf8JsonReader reader )
        {
            reader.Read(); //OpenArray
            reader.Read(); //First element

            while( reader.TokenType != JsonTokenType.EndArray )
            {
                reader.Read();
            }
        }
        /// <summary>
        /// The enumeration values.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected IEnumerable<string> ReadValues( Utf8JsonReader reader )
        {
            List<string> array0 = new List<string>();
            reader.Read(); //OpenArray
            reader.Read(); //First element

            while( reader.TokenType != JsonTokenType.EndArray )
            {
                array0.Add( reader.GetString() );
                reader.Read();
            }
            return array0;
        }

        /// <summary>
        /// Obsolete message. Enumeration is marked as obsolete when specified.
        /// </summary>
        protected virtual void VisitDeprecationMessage( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Obsolete message. Enumeration is marked as obsolete when specified.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected string ReadDeprecationMessage( Utf8JsonReader reader )
        {
            return reader.GetString();
        }
    }    /// <summary>
         /// Tool specification schema file by NUKE
         /// 
         /// </summary>
    public class Property
    {

    }    
    public class SettingsClass
    {

        /// <summary>
        /// The settings of the task.
        /// </summary>
        public virtual void Visit( Utf8JsonReader reader )
        {
            VisitSettingsClass( reader );
        }
        /// <summary>
        /// The settings of the task.
        /// </summary>
        /// <returns>The parsed value.</returns>
        public SettingsClass Read( Utf8JsonReader reader )
        {

            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
        }

        /// <summary>
        /// Name of the data class.
        /// </summary>
        protected virtual void VisitName( Utf8JsonReader reader )
        {
            VisitName( reader );
        }
        /// <summary>
        /// Name of the data class.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected TODO_UNION_TYPE ReadName( Utf8JsonReader reader )
        {

            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
        }

        /// <summary>
        /// The base class to inherit from.
        /// </summary>
        protected virtual void VisitBaseClass( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// The base class to inherit from.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected string ReadBaseClass( Utf8JsonReader reader )
        {
            return reader.GetString();
        }

        /// <summary>
        /// Enables generation of extension methods for modification.
        /// </summary>
        protected virtual void VisitExtensionMethods( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Enables generation of extension methods for modification.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected bool ReadExtensionMethods( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
        }

        /// <summary>
        /// Omits generation of the data class.
        /// </summary>
        protected virtual void VisitOmitDataClass( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Omits generation of the data class.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected bool ReadOmitDataClass( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
        }

        /// <summary>
        /// Properties of the data class.
        /// </summary>
        protected virtual void VisitProperties( Utf8JsonReader reader )
        {
            reader.Read(); //OpenArray
            reader.Read(); //First element

            while( reader.TokenType != JsonTokenType.EndArray )
            {
                JsonVisitor.Property.Visit( reader );
                reader.Read();
            }
        }
        /// <summary>
        /// Properties of the data class.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected IEnumerable<JsonVisitor.Property> ReadProperties( Utf8JsonReader reader )
        {
            List<JsonVisitor.Property> array0 = new List<JsonVisitor.Property>();
            reader.Read(); //OpenArray
            reader.Read(); //First element

            while( reader.TokenType != JsonTokenType.EndArray )
            {
                array0.Add( JsonVisitor.Property.Read( reader ) );
                reader.Read();
            }
            return array0;
        }

        /// <summary>
        /// Obsolete message. DataClass is marked as obsolete when specified.
        /// </summary>
        protected virtual void VisitDeprecationMessage( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Obsolete message. DataClass is marked as obsolete when specified.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected string ReadDeprecationMessage( Utf8JsonReader reader )
        {
            return reader.GetString();
        }
    }    /// <summary>
         /// Tool specification schema file by NUKE
         /// 
         /// </summary>
    public class Task
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual void Visit( Utf8JsonReader reader )
        {
            VisitTask( reader );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The parsed value.</returns>
        public Task Read( Utf8JsonReader reader )
        {

            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
        }

        /// <summary>
        /// Help or introduction text to for the tool. Supports 'a-href', 'c', 'em', 'b', 'ul', 'li' and 'para' tags for better formatting.
        /// </summary>
        protected virtual void VisitHelp( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Help or introduction text to for the tool. Supports 'a-href', 'c', 'em', 'b', 'ul', 'li' and 'para' tags for better formatting.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected string ReadHelp( Utf8JsonReader reader )
        {
            return reader.GetString();
        }

        /// <summary>
        /// Postfix for the task alias.
        /// </summary>
        protected virtual void VisitPostfix( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Postfix for the task alias.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected string ReadPostfix( Utf8JsonReader reader )
        {
            return reader.GetString();
        }

        /// <summary>
        /// Return type of the task.
        /// </summary>
        protected virtual void VisitReturnType( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Return type of the task.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected string ReadReturnType( Utf8JsonReader reader )
        {
            return reader.GetString();
        }

        /// <summary>
        /// Skips appending of common task properties.
        /// </summary>
        protected virtual void VisitOmitCommonProperties( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Skips appending of common task properties.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected bool ReadOmitCommonProperties( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
        }

        /// <summary>
        /// Appends the properties of the named property sets.
        /// </summary>
        protected virtual void VisitCommonPropertySets( Utf8JsonReader reader )
        {
            reader.Read(); //OpenArray
            reader.Read(); //First element

            while( reader.TokenType != JsonTokenType.EndArray )
            {
                reader.Read();
            }
        }
        /// <summary>
        /// Appends the properties of the named property sets.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected IEnumerable<string> ReadCommonPropertySets( Utf8JsonReader reader )
        {
            List<string> array0 = new List<string>();
            reader.Read(); //OpenArray
            reader.Read(); //First element

            while( reader.TokenType != JsonTokenType.EndArray )
            {
                array0.Add( reader.GetString() );
                reader.Read();
            }
            return array0;
        }

        /// <summary>
        /// Generates a pre-process hook
        /// </summary>
        protected virtual void VisitPreProcess( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Generates a pre-process hook
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected bool ReadPreProcess( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
        }

        /// <summary>
        /// Generates a post-process hook
        /// </summary>
        protected virtual void VisitPostProcess( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Generates a post-process hook
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected bool ReadPostProcess( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
        }

        /// <summary>
        /// Enables log level parsing
        /// </summary>
        protected virtual void VisitLogLevelParsing( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Enables log level parsing
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected bool ReadLogLevelParsing( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
        }

        /// <summary>
        /// Custom start implementation.
        /// </summary>
        protected virtual void VisitCustomStart( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Custom start implementation.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected bool ReadCustomStart( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
        }

        /// <summary>
        /// Custom process assertion implementation.
        /// </summary>
        protected virtual void VisitCustomAssertion( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Custom process assertion implementation.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected bool ReadCustomAssertion( Utf8JsonReader reader )
        {
            return reader.GetBoolean();
        }

        /// <summary>
        /// Argument that will always be printed independently of any set property.
        /// </summary>
        protected virtual void VisitDefiniteArgument( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Argument that will always be printed independently of any set property.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected string ReadDefiniteArgument( Utf8JsonReader reader )
        {
            return reader.GetString();
        }

        /// <summary>
        /// Url of the task. If not specified, the tool url will be used.
        /// </summary>
        protected virtual void VisitOfficialUrl( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Url of the task. If not specified, the tool url will be used.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected string ReadOfficialUrl( Utf8JsonReader reader )
        {
            return reader.GetString();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void VisitSettingsClass( Utf8JsonReader reader )
        {
            VisitSettingsClass( reader );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected JsonVisitor.SettingsClass ReadSettingsClass( Utf8JsonReader reader )
        {

            reader.Read(); // Open Object.
            reader.Read(); // first property.
            while( reader.TokenType != JsonTokenType.EndArray )
            {
                string property = reader.GetString();
                switch( property )
                {
                    //TODO.
                    default:
                        throw new InvalidDataException( "Unknown property" );
                }
            }
        }

        /// <summary>
        /// Obsolete message. Task is marked as obsolete when specified.
        /// </summary>
        protected virtual void VisitDeprecationMessage( Utf8JsonReader reader )
        {
            reader.Read();
        }
        /// <summary>
        /// Obsolete message. Task is marked as obsolete when specified.
        /// </summary>
        /// <returns>The parsed value.</returns>
        protected string ReadDeprecationMessage( Utf8JsonReader reader )
        {
            return reader.GetString();
        }
    }
}
