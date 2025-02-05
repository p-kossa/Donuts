﻿using System;
using System.Reflection;
using Aki.Reflection.Patching;
using EFT;
using HarmonyLib;

namespace dvize.Donuts
{
    internal class PatchStandbyTeleport : ModulePatch
    {
        private static MethodInfo _method1;

        protected override MethodBase GetTargetMethod()
        {
            Type standbyClassType = typeof(GClass353);
            _method1 = AccessTools.Method(standbyClassType, "method_1");

            return AccessTools.Method(standbyClassType, "UpdateNode");
        }

        [PatchPrefix]
        public static bool Prefix(GClass353 __instance, BotStandByType ___botStandByType_0, BotOwner ___botOwner_0)
        {
            if (!___botOwner_0.Settings.FileSettings.Mind.CAN_STAND_BY)
            {
                return false;
            }

            if (!__instance.CanDoStandBy)
            {
                return false;
            }

            if (___botStandByType_0 == BotStandByType.goToSave)
            {
                _method1.Invoke(__instance, new object[] { });
            }

            return false;
        }
    }
}
