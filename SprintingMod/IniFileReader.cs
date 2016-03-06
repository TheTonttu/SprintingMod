using System.Collections.Generic;
using System.IO;

namespace SprintingMod
{
    public class IniFileReader
    {
        private static string _IniFileReaderInfo = "[SprintingMod IniFileReader INFO] ";
        public Dictionary<string, string> GetSettings(string fileName)
        {
            StreamReader configReader;
            string line = "";
            Dictionary<string, string> settings = new Dictionary<string, string>();
            try
            {
                configReader = File.OpenText(fileName);
            }
            catch
            {
                StardewModdingAPI.Program.LogInfo(_IniFileReaderInfo + "SprintingMod.ini file does not exist. Attempting to create a SprintingMod.ini file.");
                StreamWriter configWriter = new StreamWriter(fileName);
                configWriter.WriteLine("[Settings]");
                configWriter.WriteLine("SprintSpeed = 3");
                configWriter.WriteLine("KeyToPress = 17");
                configWriter.Close();

                configReader = File.OpenText(fileName);
            }

            while ((line = configReader.ReadLine()) != null)
            {
                if(line[0] == '[')
                {
                    continue;
                }
                else if(line[0] == '#' || line[0] == ';')
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
