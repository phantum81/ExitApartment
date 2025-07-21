using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageManager : MonoBehaviour
{

    private ELanguage eLanguage = ELanguage.English;
    public ELanguage Elanguage => eLanguage;
    
    private bool isChanging = false;

    private const string InGameTable = "InGameTable";

    private Dictionary<EInteractionType, string> keyMap = new()
    {
        { EInteractionType.Pick,  "Pick" },
        { EInteractionType.See,   "See" },
        { EInteractionType.Use,   "Use" },
        { EInteractionType.Find,  "Find" },
        { EInteractionType.Pull,  "Pull" },
        { EInteractionType.Push,  "Push" },
        { EInteractionType.Open,  "Open" },
        { EInteractionType.Close, "Close" },
        { EInteractionType.Press, "Press" },
        { EInteractionType.Write, "Write" },
    };
    private Dictionary<EInteractionType, string> localizedCache = new Dictionary<EInteractionType, string>();
    public Dictionary<EInteractionType, string> LocalizedCache => localizedCache;



    private void Awake()
    {
        if(!GameManager.Instance.SetData.IsStart)
            Init();

        
    }


    public void Init()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                eLanguage = ELanguage.English;
                break;

            case SystemLanguage.Korean:
                eLanguage = ELanguage.Korean;
                break;

            default:
                eLanguage = ELanguage.English;
                break;
        }
        ChangeLocale((int)eLanguage);
        InitLocalization();

    }
    private async void InitLocalization()
    {
        await LocalizationSettings.InitializationOperation.Task; 

        localizedCache.Clear();
        Locale currentLanguage = LocalizationSettings.SelectedLocale;
    
        foreach (var pair in keyMap)
        {
            string result = LocalizationSettings.StringDatabase.GetLocalizedString(InGameTable, pair.Value, currentLanguage);
            localizedCache[pair.Key] = result;
        }
        
      
        

    }

    public void SetLanguage(ELanguage _eLanguage)
    {
        eLanguage = _eLanguage;
    }
    public async void ChangeLocale(int _index)
    {
        if (isChanging)
            return;

        isChanging = true;

        await LocalizationSettings.InitializationOperation.Task;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_index];
        InitLocalization();
        isChanging = false;
    }
}
