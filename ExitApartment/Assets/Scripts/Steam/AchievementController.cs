using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementController : MonoBehaviour
{



    public void Unlock(string _Id)
    {
        if (!SteamClient.IsValid)
        {
            Debug.LogWarning("Steam not initialized!");
            return;
        }

        var achievement = new Steamworks.Data.Achievement(_Id);
        achievement.Trigger();
        Debug.Log($"Achievement unlocked: {_Id}");
    }

    public void ResetAchivement(string _Id)
    {
        if (!SteamClient.IsValid)
        {
            Debug.LogWarning("Steam not initialized!");
            return;
        }
        var achievement = new Steamworks.Data.Achievement(_Id);
        achievement.Clear();
        Debug.Log($"Achievement reset: {_Id}");
    }
}
