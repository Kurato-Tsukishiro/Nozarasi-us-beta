using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using System;
using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnhollowerBaseLib;
using Nozarasius;
using Hazel;
using System.Threading;
using System.Threading.Tasks;
namespace Nozarasius {
    class NozLateTask {
        public string name;
        public float timer;
        public Action action;
        public static List<NozLateTask> Tasks = new List<NozLateTask>();
        public bool run(float deltaTime) {
            timer -= deltaTime;
            if(timer <= 0) {
                action();
                return true;
            }
            return false;
        }
        public NozLateTask(Action action, float time, string name = "No Name Task") {
            this.action = action;
            this.timer = time;
            this.name = name;
            Tasks.Add(this);

        }
        public static void Update(float deltaTime) {
            var TasksToRemove = new List<NozLateTask>();
            Tasks.ForEach((task) => {
                if(task.run(deltaTime)) {
                    TasksToRemove.Add(task);
                }
            });
            TasksToRemove.ForEach(task => Tasks.Remove(task));
        }
    }
}
