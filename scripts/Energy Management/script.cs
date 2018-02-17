#region pre-script
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using VRageMath;
using VRage.Game;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Ingame;
using Sandbox.Game.EntityComponents;
using VRage.Game.Components;
using VRage.Collections;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;

namespace IngameScript
{
    public class Program : MyGridProgram
    {
        #endregion
        public Program()
        {

            // The constructor, called only once every session and
            // always before any other method is called. Use it to
            // initialize your script. 
            //     
            // The constructor is optional and can be removed if not
            // needed.
            // 
            // It's recommended to set RuntimeInfo.UpdateFrequency 
            // here, which will allow your script to run itself without a 
            // timer block.

        }

        public void Save()
        {

            // Called when the program needs to save its state. Use
            // this method to save your state to the Storage field
            // or some other means. 
            // 
            // This method is optional and can be removed if not
            // needed.

        }

        public void Main(string argument, UpdateType updateSource)
        {
            
            // MODIFY
            // Minimum value for charging the batteries
            float solar_min = 0.03F;
            // Maximum value for discharging the batteries
            float solar_max = 0.014F;
            string text_name = "Text Panel 1";
            string solar_name = "Solar Panel ";
            string battery_name = "Battery ";
            string status = "Processing...";
            // DO NOT MODIFY
            float totalValue = 0;
            float solar_count = 0;
            float solar_average = 0;
            int device_count = 0;
            bool charge = false;
            bool discharge = false;
            int battery_count = 0;
            IMyTextPanel panel1 = GridTerminalSystem.GetBlockWithName(text_name) as IMyTextPanel;
            // Dirty and quick way to do this 
            for (int i = 0; i < 500; i++)
            {
                try
                {
                    IMySolarPanel solar_panel = GridTerminalSystem.GetBlockWithName(solar_name + i) as IMySolarPanel;
                    totalValue = totalValue + solar_panel.MaxOutput;
                    device_count++; solar_count++;
                }
                catch (Exception)
                {
                    i++;
                }
            }
            solar_average = (totalValue / solar_count);
            if (solar_average < solar_max)
                discharge = true;
            else
                charge = true;
            for (int i = 0; i < 500; i++)
            {
                try
                {
                    string battery_id = battery_name + i;
                    IMyBatteryBlock battery = GridTerminalSystem.GetBlockWithName(battery_id) as IMyBatteryBlock;
                    if (charge)
                    {
                        battery.SemiautoEnabled = false;
                        battery.OnlyDischarge = false;
                        battery.OnlyRecharge = true;
                        if(status != "Charging")
                            status = "Charging";
                    }
                    else if (discharge)
                    {
                        battery.SemiautoEnabled = false;
                        battery.OnlyDischarge = true;
                        battery.OnlyRecharge = false;
                        if(status != "Discharging")
                            status = "Discharging";
                    }
                    if(battery.GetId().ToString() != null)
                        battery_count++;
                }
                catch (Exception)
                {
                    i++;
                }
            }
            panel1.WritePublicText("Solar panels: " + device_count +
                "\nBetteries: " + battery_count +
                "\nEnergy: " + solar_average.ToString() +
                "\nStatus: " + status);
        }

#region post-script
    }
}
#endregion