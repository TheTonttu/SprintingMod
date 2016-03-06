using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewValley;
using System;

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
            get { return "Registers a command that allows for sprinting faster."; }
        }

        private static Int32 sprintingSpeed = 2;
        private static bool sprint;

        public override void Entry(params object[] objects)
        {
            KeyboardInput.KeyDown += KeyboardInput_KeyDown;
            KeyboardInput.KeyUp += KeyboardInput_KeyUp;
        }

        private void KeyboardInput_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.LeftControl)
            {
                player_sprint();
            }
        }

        private void KeyboardInput_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.LeftControl)
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
