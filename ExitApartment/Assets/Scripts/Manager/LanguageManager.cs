using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    private ELanguage eLanguage = ELanguage.English;
    public ELanguage Elanguage => eLanguage;
    private void Awake()
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
    }

    public void SetLanguage(ELanguage _eLanguage)
    {
        eLanguage = _eLanguage;
    }
    
}
