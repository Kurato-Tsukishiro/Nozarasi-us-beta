﻿using System;
using System.Reflection;
using UnhollowerBaseLib;
using UnityEngine;

namespace Nozarasius
{

    // https://github.com/Eisbison/TheOtherRoles/blob/main/TheOtherRoles/Helpers.cs
    public static class Helpers {

        public static Sprite LoadSpriteFromResources(string path, float pixelsPerUnit) {
            
            try {
                var texture = LoadTextureFromResources(path);
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
            } catch {
                NozLogger.error($"Error loading sprite from path: {path}");
            }
            return null;
        }

        public static Texture2D LoadTextureFromResources(string path) {
            
            try {
                var texture = new Texture2D(2, 2, TextureFormat.ARGB32, true);
                var assembly = Assembly.GetExecutingAssembly();
                var stream = assembly.GetManifestResourceStream(path);
                var byteTexture = new byte[stream.Length];
                _ = stream.Read(byteTexture, 0, (int) stream.Length);
                LoadImage(texture, byteTexture, false);
                return texture;
            } catch {
                NozLogger.error($"Error loading texture from resources: {path}");
            }
            return null;
        }

        private delegate bool DelegateLoadImage(IntPtr tex, IntPtr data, bool markNonReadable);
        private static DelegateLoadImage _callLoadImage;
        
        private static bool LoadImage(Texture2D tex, byte[] data, bool markNonReadable) {
            
            _callLoadImage ??= IL2CPP.ResolveICall<DelegateLoadImage>("UnityEngine.ImageConversion::LoadImage");
            var il2cppArray = (Il2CppStructArray<byte>) data;
            
            return _callLoadImage.Invoke(tex.Pointer, il2cppArray.Pointer, markNonReadable);
        }
    }

    public class PlayerVersion
    {
        public readonly Version version;
        public readonly int beta_ver;
        public readonly string tag;
        public PlayerVersion(int major, int minor, int patch, int revision, int beta, string tag_str)
        {
            version = new Version(major,minor,patch == -1?0:patch,revision == -1?0:revision);
            beta_ver = beta;
            tag = tag_str;
        }
        public PlayerVersion(Version ver, int beta, string tag_str)
        {
            version = new Version(ver.Major,ver.Minor,ver.Build == -1?0:ver.Build,ver.Revision == -1?0:ver.Revision);
            beta_ver = beta;
            tag = tag_str;
        }
        public bool isEqual(PlayerVersion pv)
        {
            return (pv.version == version && pv.beta_ver == beta_ver && pv.tag == tag);
        }
    }
}