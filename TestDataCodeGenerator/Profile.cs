using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TestDataCodeGenerator
{
    public enum GenerationOption
    {
        Declarative,
        Poco
    }

    [Serializable]
    public class TableRow
    {
        public string Schema { get; set; }
        public string TableName { get; set; }
        public string NamespaceOverride { get; set; }
    }

    [Serializable]
    public class Profile
    {
        public string ProfileName { get; set; }

        public string ConnectionString { get; set; }
        public string OutputFolder { get; set; }
        public string GeneratedClassName { get; set; }
        public GenerationOption GenerationOption { get; set; }
        public string DefaultNameSpace { get; set; }
        public List<TableRow> TableList { get; set; }
    }

    [Serializable]
    public class ProfileCollection
    {
        public Profile LastEnteredProfile { get; set; }
        public List<Profile> ProfileList { get; set; }
    }

    public class ProfileStorage
    {
        public static ProfileCollection Deserialize()
        {
            using (var stream =
                new FileStream("ProfileSettings.bin",
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite,
                    FileShare.ReadWrite))
            {
                var formatter = new BinaryFormatter();

                if (stream.Length == 0)
                {
                    return null;
                }

                var result = (ProfileCollection)formatter.Deserialize(stream);
                return result;
            }
        }

        public static void Serialize(ProfileCollection profileCollection)
        {
            using (var stream =
                new FileStream("ProfileSettings.bin",
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.ReadWrite))
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(stream, profileCollection);
            }
        }
    }
}
