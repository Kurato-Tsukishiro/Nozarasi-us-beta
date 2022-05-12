using HarmonyLib;
using Nozarasius;
using TMPro;
using UnityEngine;

namespace Nozarasius
{
    //From The Other Roles source
    //https://github.com/Eisbison/TheOtherRoles/blob/Nozarasius/TheOtherRoles/Patches/CredentialsPatch.cs
    //ピングトラッカーパッチ
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    class PingTrackerPatch
    {
        //モッドスタンプの表示
        private static GameObject modStamp;
        static void Prefix(PingTracker __instance)
        {
            if (modStamp == null)
            {
                modStamp = new GameObject("ModStamp");
                var rend = modStamp.AddComponent<SpriteRenderer>();
                rend.color = new Color(1, 1, 1, 0.5f);
                modStamp.transform.parent = __instance.transform.parent;
                modStamp.transform.localScale *= 0.6f;
            }
            float offset = (AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started) ? 0.75f : 0f;
            modStamp.transform.position = HudManager.Instance.MapButton.transform.position + Vector3.down * offset;
        }

        static void Postfix(PingTracker __instance)
        {
            __instance.text.alignment = TMPro.TextAlignmentOptions.TopRight;
            __instance.text.text += Nozarasius.credentialsText;
            if (AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started)
            {
                if (PlayerControl.LocalPlayer.Data.IsDead)
                {
                    __instance.transform.localPosition = new Vector3(3.45f, __instance.transform.localPosition.y, __instance.transform.localPosition.z);
                }
                else
                {
                    __instance.transform.localPosition = new Vector3(4.2f, __instance.transform.localPosition.y, __instance.transform.localPosition.z);
                }
            }
            else
            {
                __instance.transform.localPosition = new Vector3(3.5f, __instance.transform.localPosition.y, __instance.transform.localPosition.z);
            }
        }
        //バージョン表示
        [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
        public class VersionShowerPatch
        {
            private static TMPro.TextMeshPro ErrorText;
            public static void Postfix(VersionShower __instance)
            {
                Nozarasius.credentialsText = "\r\n<color=" + Nozarasius.modColor + ">Nozarasi us</color>" + Nozarasius.PluginVersion + Nozarasius.VersionSuffix + "\r\nMode:" + "<color=" + Nozarasius.modmodeColor + ">" + Nozarasius.Modmode + "</color>";
                credentials = UnityEngine.Object.Instantiate(__instance.text);
                credentials.text = Nozarasius.credentialsText;
                credentials.alignment = (TextAlignmentOptions)260;
                credentials.transform.position = new Vector3(4.3f, __instance.transform.localPosition.y + 0.3f, 0f);
                bool flag = Nozarasius.hasArgumentException && !Nozarasius.ExceptionMessageIsShown;
                if (flag)
                {
                    Nozarasius.ExceptionMessageIsShown = true;
                    ErrorText = UnityEngine.Object.Instantiate(__instance.text);
                    ErrorText.transform.position = new Vector3(0f, 0.5f, 50f);
                    ErrorText.alignment = (TextAlignmentOptions)514;
                    ErrorText.text = "エラー:Lang系DictionaryにKeyの重複が発生しています!\r\n" + Nozarasius.ExceptionMessage;
                    ErrorText.color = Color.red;
                }
            }
            public static TextMeshPro credentials;
        }
    }
}
//ずっと読み込むやつ
[HarmonyPatch(typeof(ModManager), nameof(ModManager.LateUpdate))]
class AwakePatch
{
    public static void Prefix(ModManager __instance)
    {
        __instance.ShowModStamp();
        NozLateTask.Update(Time.deltaTime);
        hennsuu.Update(Time.deltaTime);
    }
}