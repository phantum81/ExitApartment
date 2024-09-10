using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static UnityEngine.Rendering.DebugUI;

public class GammaController : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume post;
    private ColorGrading colorGrading;

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    public void SetGammaValue(float _value)
    {
        if (post != null && post.profile.TryGetSettings(out colorGrading))
        {

            if (colorGrading != null)
            {
                // 감마 값을 업데이트
                colorGrading.gamma.value = new Vector4(colorGrading.gamma.value.x, colorGrading.gamma.value.y, colorGrading.gamma.value.z, _value);
               
            }



        }
        else
        {
            Debug.LogWarning("Post-processing profile or ColorGrading settings are missing.");
        }
    }
    public float GetGammaValue()
    {
        if (post != null && post.profile.TryGetSettings(out colorGrading))
        {

            return colorGrading.gamma.value.w;


        }
        return -1f;
    }
}
