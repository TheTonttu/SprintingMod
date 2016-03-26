using StardewModdingAPI;
using StardewModdingAPI.Inheritance;
using StardewModdingAPI.Events;
using StardewValley;
using System;

namespace SprintingMod
{
    public class Sprinting : Mod
    {
        public static SprintingModConfig Config { get; set; }
        private const string _debuggerInfo = "[SprintingMod INFO] ";
        private Buff SprintingBuff { get; set; }
        private int timeSinceLastDrain = 0;

        public override void Entry(params object[] objects)
        {
            Config = new SprintingModConfig().InitializeConfig(BaseConfigPath);
            BuffInit();
            KeyboardInput.KeyDown += KeyboardInput_KeyDown;
            KeyboardInput.KeyUp += KeyboardInput_KeyUp;
            ControlEvents.ControllerButtonPressed += ControllerButtonPressed;
            ControlEvents.ControllerButtonReleased += ControllerButtonReleased;
            GameEvents.OneSecondTick += GameEvents_OneSecondTick;
            
        }

        private void BuffInit()
        {
            SprintingBuff = new Buff(0, 0, 0, 0, 0, 0, 0, 0, 0, Config.SprintSpeed, 0, 0, 1000, "Sprint Mod");
            SprintingBuff.which = Buff.speed;
            SprintingBuff.sheetIndex = Buff.speed;
        }

        private void GameEvents_OneSecondTick(object sender, EventArgs e)
        {
            if (SprintingBuff_Exists())
            {
                SGame.buffsDisplay.otherBuffs[SGame.buffsDisplay.otherBuffs.IndexOf(SprintingBuff)].millisecondsDuration = 5555;
            }
            
            if (SGame.player.isMoving() && SprintingBuff_Exists())
            {
                timeSinceLastDrain += 1;
                if (timeSinceLastDrain >= Config.StaminaDrainRate)
                {
                    SGame.player.Stamina -= Config.StaminaDrain;
                    timeSinceLastDrain = 0;
                }
            }
            else
            {
                timeSinceLastDrain = 0;
            }
        }

        private void KeyboardInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString().Equals(Config.SprintKey))
            {
                if (Config.HoldToSprint)
                    Player_Sprint();
                else
                    Player_Toggle_Sprint();
            }
        }

        private void KeyboardInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString().Equals(Config.SprintKey))
            {
                if (Config.HoldToSprint)
                {
                    Player_Walk();
                }
            }
        }

        private void ControllerButtonPressed(object sender, EventArgsControllerButtonPressed e)
        {
            if (e.ButtonPressed.ToString().Equals(Config.SprintKeyForControllers))
            {
                if (Config.HoldToSprint)
                    Player_Sprint();
                else
                    Player_Toggle_Sprint();
            }
        }

        private void ControllerButtonReleased(object sender, EventArgsControllerButtonReleased e)
        {
            if (e.ButtonReleased.ToString().Equals(Config.SprintKeyForControllers))
            {
                if (Config.HoldToSprint)
                {
                    Player_Walk();
                }
            }
        }

        private void Player_Sprint()
        {
            if (!SprintingBuff_Exists())
                SGame.buffsDisplay.addOtherBuff(SprintingBuff);
        }
        
        private void Player_Walk()
        {
            if (SprintingBuff_Exists())
            {
                SGame.buffsDisplay.otherBuffs.Remove(SprintingBuff);
                SprintingBuff.removeBuff();
                SGame.buffsDisplay.syncIcons();
            }
        }

        private void Player_Toggle_Sprint()
        {
            if (SprintingBuff_Exists())
            {
                SGame.buffsDisplay.otherBuffs.Remove(SprintingBuff);
                SprintingBuff.removeBuff();
                SGame.buffsDisplay.syncIcons();
            }
            else
            {
                SGame.buffsDisplay.addOtherBuff(SprintingBuff);
            }
        }

        private bool SprintingBuff_Exists()
        {
            if (SprintingBuff == null)
                return false;

            return SGame.buffsDisplay.otherBuffs.Contains(SprintingBuff);
        }
    }

    public class SprintingModConfig : Config
    {
        public bool HoldToSprint { get; set; }
        public int SprintSpeed { get; set; }
        public string SprintKey { get; set; }
        public int StaminaDrain { get; set; }
        public int StaminaDrainRate { get; set; }
        public string SprintKeyForControllers { get; set; }

        public override T GenerateDefaultConfig<T>()
        {
            HoldToSprint = true;
            SprintSpeed = 3;
            SprintKey = "17";
            SprintKeyForControllers = "LeftStick";
            StaminaDrain = 1;
            StaminaDrainRate = 5;
            return this as T;
        }
    }
}
