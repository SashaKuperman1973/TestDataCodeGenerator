/*
    Copyright 2016 Alexander Kuperman

    This file is part of TestDataCodeGenerator.

    TestDataCodeGenerator is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    TestDataCodeGenerator is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with TestDataFramework.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Configuration;
using System.Runtime.Serialization.Formatters.Binary;

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
        public bool IncludeDatabaseName { get; set; }
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
