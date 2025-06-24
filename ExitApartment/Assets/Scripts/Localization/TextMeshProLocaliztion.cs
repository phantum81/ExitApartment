#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

//[ExecuteAlways]
public class TextMeshProLocalization : MonoBehaviour
{
    //public LocalizedString localizedString;
    //private TextMeshPro tmp;

//    void OnEnable()
//    {
//        tmp = GetComponent<TextMeshPro>();
//        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
//        UpdateText();
//    }

//    void OnDisable()
//    {
//        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
//    }

//#if UNITY_EDITOR
//    void OnValidate()
//    {
//        if (!Application.isPlaying)
//            UpdateText();
//    }
//#endif

//    private void OnLocaleChanged(UnityEngine.Localization.Locale newLocale)
//    {
//        UpdateText();
//    }

//    void UpdateText()
//    {
//#if UNITY_EDITOR
//        if (!Application.isPlaying && EditorApplication.isUpdating)
//            return;
//#endif

//        if (tmp == null || localizedString == null) return;

//        localizedString.GetLocalizedStringAsync().Completed += handle =>
//        {
//            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
//                tmp.text = handle.Result;
//        };
//    }
}
