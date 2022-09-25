﻿using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TrombLoader.Class_Patches;

[HarmonyPatch(typeof(CurtainController))]
[HarmonyPatch(nameof(CurtainController.loadNextScene))]
public class CurtainControllerPatch
{


    static bool Prefix(CurtainController __instance)
    {
        if (__instance.gamec.levelnum == GlobalVariables.data_trackrefs.Length - 1 && !GlobalVariables.localsave.progression_trombone_champ)
        {
            LeanTween.cancelAll();
            Cursor.lockState = CursorLockMode.None;
            if (__instance.gamec.quitting && !__instance.gamec.retrying && GlobalVariables.scene_destination != "freeplay")
            {
                SceneManager.LoadScene("levelselect");
                return false;
            }
            if (__instance.gamec.quitting && __instance.gamec.retrying)
            {
                SceneManager.LoadScene("gameplay");
                return false;
            }
            if (GlobalVariables.scene_destination == "freeplay")
            {
                SceneManager.LoadScene("home");
                return false;
            }
            if (GlobalVariables.data_trackrefs[__instance.gamec.levelnum] == "einefinal" && !GlobalVariables.localsave.progression_trombone_champ)
            {
                SceneManager.LoadScene("finallevel_fail");
                return false;
            }
            SceneManager.LoadScene("points");
        }
        return false;
    }
}