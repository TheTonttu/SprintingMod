using System.Collections.Generic;
using System.IO;

namespace SprintingMod
{
    public class IniFileReader
    {
        private const string _IniFileReaderInfo = "[SprintingMod IniFileReader INFO] ";

        /// <summary>
        /// Parses an INI file; ignoring sections, newlines, pound signs, and semi-colons.
        /// </summary>
        /// <param name="fileName">
        /// A <see cref="string"/> that represents the name of the file.
        /// </param>
        /// <returns>
        /// If <paramref name="fileName"/> exists, returns list of key value pairs; otherwise returns an empty <see cref="Dictionary{string, string}"/>
        /// </returns>
        public Dictionary<string, string> GetSettings(string fileName)
        {
            StreamReader configReader;
            string line = "";
            Dictionary<string, string> settings = new Dictionary<string, string>();

            if (!File.Exists(fileName))
            {
                StardewModdingAPI.Log.Info(_IniFileReaderInfo + fileName + " does not exist. Returning empty dictionary.");
                return settings;
            }

            configReader = File.OpenText(fileName);

            while ((line = configReader.ReadLine()) != null)
            {
                if(line[0] == '[' || line[0] == '\n' || line[0] == '#' || line[0] == ';')
                {
                    continue;
                }

                line = line.Replace(" ", "");
                var keyValuePair = line.Split('=');
                settings[keyValuePair[0]] = keyValuePair[1];
            }

            configReader.Close();
            return settings;
        }
    }
}
