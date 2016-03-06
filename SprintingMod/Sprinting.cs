using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private static Int32 sprintingSpeed = 1;
        private static bool sprint;

        public override void Entry(params object[] objects)
        {
            base.Entry(objects);
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
