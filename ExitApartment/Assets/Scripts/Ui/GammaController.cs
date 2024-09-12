using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static UnityEngine.Rendering.DebugUI;

public class GammaController : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume settingPost;
    [SerializeField]
    private PostProcessVolume menuPost;
    [SerializeField]
    private PostProcessVolume inGamePost;

    private ColorGrading settingColorGrading;
    private ColorGrading menuColorGrading;
    private ColorGrading inGameColorGrading;




    public void SetGammaValue(float _value)
    {
        if (settingPost != null && settingPost.profile.TryGetSettings(out settingColorGrading))
        {

            if (settingColorGrading != null)
            {
                // 감마 값을 업데이트
                settingColorGrading.gamma.value = new Vector4(settingColorGrading.gamma.value.x, settingColorGrading.gamma.value.y, settingColorGrading.gamma.value.z, _value);
               
            }



        }

        if(menuPost != null && menuPost.profile.TryGetSettings(out menuColorGrading))
        {
            if (menuColorGrading != null)
            {
                // 감마 값을 업데이트
                menuColorGrading.gamma.value = new Vector4(settingColorGrading.gamma.value.x, settingColorGrading.gamma.value.y, settingColorGrading.gamma.value.z, _value);

            }
        }
        if (inGamePost != null && inGamePost.profile.TryGetSettings(out inGameColorGrading))
        {
            if (inGameColorGrading != null)
            {
                // 감마 값을 업데이트
                inGameColorGrading.gamma.value = new Vector4(inGameColorGrading.gamma.value.x, settingColorGrading.gamma.value.y, settingColorGrading.gamma.value.z, _value);

            }
        }
    }
    public float GetGammaValue()
    {
        if (settingPost != null && settingPost.profile.TryGetSettings(out settingColorGrading))
        {

            return settingColorGrading.gamma.value.w;


        }
        return -1f;
    }



}
