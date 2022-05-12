using System;
using System.Collections.Generic;
using System.Text;
using Nozarasius;

namespace Nozarasius
{
    class hennsuu
    {
        public static void Update(float deltaTime)
        {
            if (Nozarasius.Modmodenum == 0)
            {
                Nozarasius.Modmode = "Among us";
            }
            if (Nozarasius.Modmodenum == 1)
            {
                Nozarasius.Modmode = "Nozarasi us";
            }
            if (Nozarasius.Modmodenum == 2)
            {
                Nozarasius.Modmode = "Nozang us";
            }
            if (Nozarasius.Modmodenum == 3)
            {
                Nozarasius.Modmode = "Nozarasi";
            }
            if (Nozarasius.Modmodenum == 4)
            {
                Nozarasius.Modmodenum = 0;
            }
            if (PingTrackerPatch.VersionShowerPatch.credentials != null) 
            {
                PingTrackerPatch.VersionShowerPatch.credentials.text = Nozarasius.credentialsText;
            }
            Nozarasius.credentialsText = "\r\n<color=" + Nozarasius.modColor + ">Nozarasi us</color>" + Nozarasius.PluginVersion + Nozarasius.VersionSuffix + "\r\nMode:" + "<color=" + Nozarasius.modmodeColor + ">" + Nozarasius.Modmode + "</color>";
        }
    }
}
