using System;
using System.Collections.Generic;
using System.Text;
using Nozarasius;
using Nozarasius.CustomOptions;
using UnityEngine;

namespace Nozarasius.Roles
{
    public static class NozModRoleBehavior
    {
        public static Color ImpostorRed = Palette.ImpostorRed;
        public static class MadMate
        {
            public static List<PlayerControl> MadMatePlayer;
            public static Color32 color = ImpostorRed;
            public static bool IsImpostorCheck;
            public static int ImpostorCheckTask;
            public static bool IsUseVent;
            public static bool IsImpostorLight;
            public static void clearAndReload()
            {
                MadMatePlayer = new List<PlayerControl>();
                IsImpostorCheck = CustomOption.MadMateIsCheckImpostor.getBool();
                IsUseVent = CustomOption.MadMateIsUseVent.getBool();
                IsImpostorLight = CustomOption.MadMateIsImpostorLight.getBool();
                int Common = (int)CustomOption.MadMateCommonTask.getFloat();
                int Long = (int)CustomOption.MadMateLongTask.getFloat();
                int Short = (int)CustomOption.MadMateShortTask.getFloat();
                int AllTask = Common + Long + Short;
                if (AllTask == 0)
                {
                    Common = PlayerControl.GameOptions.NumCommonTasks;
                    Long = PlayerControl.GameOptions.NumLongTasks;
                    Short = PlayerControl.GameOptions.NumShortTasks;
                }
                ImpostorCheckTask = (int)(AllTask * (int.Parse(CustomOptions.MadMateCheckImpostorTask.getString().Replace("%", "")) / 100f));
            }
        }
    }
}
