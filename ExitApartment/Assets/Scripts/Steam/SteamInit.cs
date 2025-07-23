using Steamworks;
using UnityEngine;

public class SteamInit : MonoBehaviour
{
    private static SteamInit instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); 
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        SteamClient.Init(3888790, false);
        Debug.Log("Steam Initialized: " + SteamClient.IsValid);
    }

    void OnEnable()
    {
        Application.quitting += OnApplicationQuit;
    }

    void OnDisable()
    {
        Application.quitting -= OnApplicationQuit;
    }

    private void OnApplicationQuit()
    {
        SteamClient.Shutdown();
    }
}
