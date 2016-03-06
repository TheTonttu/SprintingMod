using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;

namespace SprintingMod
{
    public class Sprinting : Mod
    {
        public override string Name
        {
            get { return "Sprinting Mod"; }
        }

        public override string Authour
        {
            get { return "Patrick Oliver"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        public override string Description
        {
            get { return "Allows the user to increase speed when pressing a button."; }
        }

        private static int sprintingSpeed = 1;
        private static string sprintingButton = "17";
        private const string _debuggerInfo = "[SprintingMod INFO] ";

        public override void Entry(params object[] objects)
        {
            Program.LogInfo(_debuggerInfo + "Getting settings from SprintingMod.ini file.");
            UpdateSettings();
            Program.LogInfo(_debuggerInfo + "Initializing KeyboardInput listeners.");
            KeyboardInput.KeyDown += KeyboardInput_KeyDown;
            KeyboardInput.KeyUp += KeyboardInput_KeyUp;
        }

        private void UpdateSettings()
        {
            var parser = new IniFileReader();
            var settings = parser.GetSettings("SprintingMod.ini");
            sprintingSpeed = settings["SprintSpeed"].AsInt32();
            sprintingButton = settings["KeyToPress"];
        }

        private void KeyboardInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == sprintingButton)
            {
                player_sprint();
            }
        }

        private void KeyboardInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == sprintingButton)
            {
                player_walk();
            }
        }

        static void player_sprint()
        {
            Game1.player.addedSpeed = sprintingSpeed;
        }
        
        static void player_walk()
        {
            Game1.player.addedSpeed = 0;
        }
    }
}
