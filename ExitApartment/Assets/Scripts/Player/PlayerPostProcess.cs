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


    private float lensVlaue = 0f;


    public void InitPostProcess()
    {
        VignetteOn(false);
        GrainOn(false);
        ChromaticAberrationOn(false);
        StartCoroutine(LensDistortion(false));

    }
    public void VignetteOn(bool _bool)
    {

        if (post != null && post.profile.TryGetSettings(out vignette))
        {
            vignette.active = _bool;
        }
        
        
    }
    public void GrainOn(bool _bool)
    {
        if (post != null && post.profile.TryGetSettings(out grain))
        {

            grain.active = _bool;
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
                yield return StartCoroutine(LerpVlaue(value => distortion.intensity.value = value ,0, -40, 1f, 1f));
            }
           
                        
        }
        

    }


    IEnumerator LerpVlaue(Action<float> _value ,float _min, float _max, float _time, float _speed)
    {
        float elapsedTime = 0f;
        while (true)
        {
            _value(Mathf.Lerp(_min, _max, elapsedTime / (_time * _speed)));

            
            if (elapsedTime >= _time)
            {
                break;
            }

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void ChromaticAberrationOn(bool _bool)
    {
        if (post != null && post.profile.TryGetSettings(out chromaticAber))
        {
            if(!_bool)
            {
                chromaticAber.active = _bool;
                chromaticAber.intensity.value = 0;
                return;
            }
            else
            {
                chromaticAber.active = _bool;
                while (chromaticAber.intensity.value <= 1f)
                {
                    chromaticAber.intensity.value += Time.deltaTime * 0.3f;
                }
            }
            

        }
    }


    public void On12FDeadState()
    {
        VignetteOn(true);
        GrainOn(true);
    }





}
