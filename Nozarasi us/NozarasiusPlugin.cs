using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnhollowerBaseLib;
using Hazel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Nozarasius
{

    [BepInAutoPlugin]
    [BepInProcess("Among Us.exe")]
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public partial class NozarasiusPlugin : BasePlugin
    {
        public Harmony Harmony { get; } = new(Id);

        public ConfigEntry<string> ConfigName { get; private set; }

        public override void Load()
        {
            Harmony.PatchAll();
        }
        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}