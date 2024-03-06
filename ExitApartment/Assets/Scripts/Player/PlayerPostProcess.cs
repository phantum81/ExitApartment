using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerPostProcess : MonoBehaviour
{
    [Header("포스트 프로세싱"),SerializeField]
    private PostProcessVolume post;

    private Vignette vignette;
    private Grain grain;
    private LensDistortion distortion;    
    private ChromaticAberration chromaticAber;


    


    public void InitPostProcess()
    {
        VignetteOn(false);
        StartCoroutine(GrainOn(false));
        StartCoroutine(ChromaticAberrationOn(false));
        StartCoroutine(LensDistortion(false));

    }
    public void VignetteOn(bool _bool)
    {

        if (post != null && post.profile.TryGetSettings(out vignette))
        {
            vignette.active = _bool;
        }
        
        
    }
    public IEnumerator GrainOn(bool _bool)
    {
        

        if (post != null && post.profile.TryGetSettings(out grain))
        {
            
            if (!_bool)
            {
                yield return StartCoroutine(LerpVlaue(value => grain.intensity.value = value, grain.intensity.value, 0, 2f, 1f));
                grain.active = _bool;
            }
            else
            {
                grain.active = _bool;
                yield return StartCoroutine(LerpVlaue(value => grain.intensity.value = value, grain.intensity.value, 1, 0.5f, 1f));
                
            }
        }
        
    }

    public IEnumerator LensDistortion(bool _bool)
    {
        
        if (post != null && post.profile.TryGetSettings(out distortion))
        {
            
            if (!_bool)
            {
                yield return StartCoroutine(LerpVlaue(value => distortion.intensity.value = value, distortion.intensity.value, 0, 2f, 1f));

                distortion.active = _bool;

            }
            else
            {
                distortion.active = _bool;                
                yield return StartCoroutine(LerpVlaue(value => distortion.intensity.value = value, distortion.intensity.value, -40, 0.5f, 1f));
                
            }
                                   
        }
        
    }




    public IEnumerator ChromaticAberrationOn(bool _bool)
    {
        
        if (post != null && post.profile.TryGetSettings(out chromaticAber))
        {
            
            if (!_bool)
            {
                yield return StartCoroutine(LerpVlaue(value => chromaticAber.intensity.value = value, chromaticAber.intensity.value, 0, 2f, 1f));
                
                chromaticAber.active = _bool;
           
            }
            else
            {
                chromaticAber.active = _bool;
                yield return StartCoroutine(LerpVlaue(value => chromaticAber.intensity.value = value, chromaticAber.intensity.value, 1, 0.5f, 1f));
                
            }
            

        }
    }



    IEnumerator LerpVlaue(Action<float> _value, float _min, float _max, float _inverseSpeed, float _lerpRatio)
    {
        float elapsedTime = 0f;
        while (true)
        {
            _value(Mathf.Lerp(_min, _max, elapsedTime / (_inverseSpeed * _lerpRatio)));


            if (elapsedTime >= _inverseSpeed)
            {
                break;
            }

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void On12FDeadState()
    {
        VignetteOn(true);
        GrainOn(true);
    }





}
