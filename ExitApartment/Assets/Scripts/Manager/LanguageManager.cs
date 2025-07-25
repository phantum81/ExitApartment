using System;
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




    private Dictionary<EErrorType, string> errorKeyMap = new()
    {
        { EErrorType.NotClose,  "Ele_close" },
        { EErrorType.NotGo,   "Ele_not_go" },
        { EErrorType.NotWork,   "Ele_not_move" },
        { EErrorType.NotPress,  "Ele_not_press" },
        { EErrorType.NotWrite,  "Paper_wirte" },
        { EErrorType.Same,  "Ele_same" },

    };
    private Dictionary<EErrorType, string> errorlocalizedCache = new Dictionary<EErrorType, string>();
    public Dictionary<EErrorType, string> ErrorLocalizedCache => errorlocalizedCache;

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
        InitLocalization(InGameTable, keyMap, localizedCache);
        InitLocalization(InGameTable, errorKeyMap, errorlocalizedCache);
    }
    private async void InitLocalization<T>(string _table, Dictionary<T, string> _key, Dictionary<T, string> _cache) where T : Enum
    {
        await LocalizationSettings.InitializationOperation.Task;

        _cache.Clear();
        Locale currentLanguage = LocalizationSettings.SelectedLocale;
    
        foreach (var pair in _key)
        {
            string result = LocalizationSettings.StringDatabase.GetLocalizedString(_table, pair.Value, currentLanguage);
            _cache[pair.Key] = result;
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
        InitLocalization(InGameTable, keyMap, localizedCache);
        InitLocalization(InGameTable, errorKeyMap, errorlocalizedCache);
        isChanging = false;
    }
}
