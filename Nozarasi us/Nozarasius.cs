using System.Globalization;
using System.Diagnostics;
using System.Text.RegularExpressions;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using System;
using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity;
using UnityEditor;
using UnityEngineInternal;
using UnhollowerBaseLib;
using Hazel;
using System.Linq;
using System.Runtime.CompilerServices;
using InnerNet;

namespace Nozarasius
{
    [BepInProcess("Among Us.exe")]
    public class Nozarasius : BasePlugin
    {
        //インスタンス用変数
        public static VersionShower insutansu;
        //バージョン
        public const string PluginVersion = "1.02";
        public static string credentialsText;
        public static string credentialsTitle;
        public const string BetaVersion = "1";
        public const string BetaName = "Name Revolution Beta";
        public static bool hasArgumentException = false;
        //バージョンに続く説明みたいなの
        public static string VersionSuffix = "β";
        public static bool ExceptionMessageIsShown = false;
        //ロガー
        public static BepInEx.Logging.ManualLogSource Logger;
        //MODの色
        public static string modColor = "#8cfc03";
        public static string ExceptionMessage;
        //MODモード
        public static string Modmode;
        public static int Modmodenum = 0;
        //MODモードの色
        public static string modmodeColor = "#99ffe7";
        //設定関連
        public static float TextCursorTimer;
        //プレイヤーのステータス
        public static PlayerState ps;
        public static int SnitchExposeTaskLeft = 1;
        //日本語強制設定だったはず
        public static bool forceJapanese = false;
        //設定のやつ
        public static bool OptionControllerIsEnable;
        public static string TextCursor => TextCursorVisible ? "_" : "";
        public static bool TextCursorVisible;
        //よくわからん奴
        public static ConfigEntry<bool> JapaneseRoleName { get; private set; }
        //使うかわからんデバック用のやつとMOD設定関連のやつ
        [HarmonyPatch(typeof(KeyboardJoystick), nameof(KeyboardJoystick.Update))]
        class DebugManager
        {
            static System.Random random = new System.Random();
            public static void Postfix(KeyboardJoystick __instance)
            {
                if (Input.GetKeyDown(KeyCode.Return) && Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.LeftShift) && AmongUsClient.Instance.AmHost)
                {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.EndGame, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                }
                if (Input.GetKeyDown(KeyCode.LeftShift) && GameStartManager._instance && AmongUsClient.Instance.AmHost)
                {
                    GameStartManager.Instance.countDownTimer = 0;
                }
                if (Input.GetKeyDown(KeyCode.C) && GameStartManager._instance && AmongUsClient.Instance.AmHost)
                {
                    GameStartManager.Instance.ResetStartState();
                }
                if (Input.GetKeyDown(KeyCode.N) && Input.GetKeyDown(KeyCode.LeftControl) && AmongUsClient.Instance.AmHost)
                {
                }
                if (Input.GetKeyDown(KeyCode.X) && AmongUsClient.Instance.GameMode == GameModes.FreePlay)
                {
                    PlayerControl.LocalPlayer.Data.Object.SetKillTimer(0f);
                }
                if(Input.GetKeyDown(KeyCode.O))
                {
                    Modmodenum += 1;
                }
                //===============
                // テスト用キーコマンド
                //Key Command for test
                //注意 おそらく一部のやつは使えません!
                //You don't may be able to use it!
                // | キー | 条件 | 動作 |
                // |  Key |  if  | play |
                // | ---- | ---- | ---- |
                // | X | フリープレイ中 | キルクール0 |
                //     |   Free play    | set kill CoolDown 0 |
                // | Y | ホスト | カスタム設定同期 |
                // | O | いつでも | モード変更 |
                // | G | フリープレイ中 | 開始画面表示 |
                // | = | フリープレイ中 | VisibleTaskCountを切り替え |
                // | P | フリープレイ中 | トイレのドアを一気に開ける |
                // | U | オンライン以外 | 自分の投票をClearする |
                // | N | プレイヤーを生成 |
                //====================
            }
        }
        public static ConfigEntry<string> WebhookURL { get; private set; }

        public override void Load()
        {
            throw new NotImplementedException();
        }
    }
    //RPC
    static class RPCProcedure
    {
        public static void SyncCustomSettings(
            //宣言

            )
        {
            //紐づけ
        }
    }
    enum CustomRPC
    {
        EndGame
    }
}