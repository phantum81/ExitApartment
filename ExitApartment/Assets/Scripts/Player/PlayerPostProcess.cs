using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private List<Coroutine> curCoroutine = new List<Coroutine>();
    public List<Coroutine> CurCoroutine => curCoroutine;


    public void InitPostProcess()
    {
        StopAllCoroutinesInList();

        VignetteOn(false);
        curCoroutine.Add(StartCoroutine(PostProccessEffectOff(EpostProcessType.Grain)));
        curCoroutine.Add(StartCoroutine(PostProccessEffectOff(EpostProcessType.ChromaticAberration)));
        curCoroutine.Add(StartCoroutine(PostProccessEffectOff(EpostProcessType.LensDistortion)));

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
                yield return StartCoroutine(LerpValue(value => grain.intensity.value = value, grain.intensity.value, 0, 2f, 1f));
                grain.active = _bool;
            }
            else
            {
                grain.active = _bool;
                yield return StartCoroutine(LerpValue(value => grain.intensity.value = value, grain.intensity.value, 1, 0.5f, 1f));
                
            }
        }
        
    }

    public IEnumerator LensDistortion(bool _bool)
    {
        
        if (post != null && post.profile.TryGetSettings(out distortion))
        {
            
            if (!_bool)
            {
                yield return StartCoroutine(LerpValue(value => distortion.intensity.value = value, distortion.intensity.value, 0, 2f, 1f));

                distortion.active = _bool;

            }
            else
            {
                distortion.active = _bool;                
                yield return StartCoroutine(LerpValue(value => distortion.intensity.value = value, distortion.intensity.value, -40, 0.5f, 1f));
                
            }
                                   
        }
        
    }




    public IEnumerator ChromaticAberrationOn(bool _bool)
    {
        
        if (post != null && post.profile.TryGetSettings(out chromaticAber))
        {
            
            if (!_bool)
            {
                yield return StartCoroutine(LerpValue(value => chromaticAber.intensity.value = value, chromaticAber.intensity.value, 0, 2f, 1f));
                
                chromaticAber.active = _bool;
           
            }
            else
            {
                chromaticAber.active = _bool;
                yield return StartCoroutine(LerpValue(value => chromaticAber.intensity.value = value, chromaticAber.intensity.value, 1, 0.5f, 1f));
                
            }
            

        }
    }



    IEnumerator LerpValue(Action<float> _value, float _min, float _max, float _inverseSpeed, float _lerpRatio)
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

    public IEnumerator PostProccessEffectOff(EpostProcessType _type, float _max =0f, float _inverseSpeed = 2f, float _lerpRatio =1f)
    {
        

        if (post != null)
        {

            switch (_type)
            {
                case EpostProcessType.Vignette:
                    break;
                case EpostProcessType.Grain:
                    

                    if (post.profile.TryGetSettings(out grain))
                    {                        
                        yield return StartCoroutine(LerpValue(value => grain.intensity.value = value, grain.intensity.value, _max, _inverseSpeed, _lerpRatio));
                        grain.active = false;
                    }                        
                    break;

                case EpostProcessType.ChromaticAberration:
                    
                    
                    if (post.profile.TryGetSettings(out chromaticAber))
                    {
                        yield return StartCoroutine(LerpValue(value => chromaticAber.intensity.value = value, chromaticAber.intensity.value, _max, _inverseSpeed, _lerpRatio));
                        chromaticAber.active = false;
                    }                        
                    break;

                case EpostProcessType.LensDistortion:

                    
                    if (post.profile.TryGetSettings(out distortion))
                    {
                        yield return StartCoroutine(LerpValue(value => distortion.intensity.value = value, distortion.intensity.value, _max, _inverseSpeed, _lerpRatio));
                        distortion.active = false;
                    }                        
                    break;
            }

            
        }
    }


    public IEnumerator PostProccessEffectOn(EpostProcessType _type, float _max =1f, float _inverseSpeed = 0.5f, float _lerpRatio = 1f)
    {
        

        if (post != null)
        {

            switch (_type)
            {
                case EpostProcessType.Vignette:
                    break;
                case EpostProcessType.Grain:
                                        
                    if (post.profile.TryGetSettings(out grain))
                    {                        
                        grain.active = true;
                        yield return StartCoroutine(LerpValue(value => grain.intensity.value = value, grain.intensity.value, _max, _inverseSpeed, _lerpRatio));
                    }
                    break;

                case EpostProcessType.ChromaticAberration:

                    if (post.profile.TryGetSettings(out chromaticAber))
                    {
                        chromaticAber.active = true;
                        yield return StartCoroutine(LerpValue(value => chromaticAber.intensity.value = value, chromaticAber.intensity.value, _max, _inverseSpeed, _lerpRatio));
                    }
                    break;

                case EpostProcessType.LensDistortion:
                                     
                    if  (post.profile.TryGetSettings(out distortion))
                    {
                        distortion.active = true;
                        yield return StartCoroutine(LerpValue(value => distortion.intensity.value = value, distortion.intensity.value, _max, _inverseSpeed, _lerpRatio));
                    }                                            
                    break;
            }

            
        }
    }


    public void On12FDeadState()
    {
        VignetteOn(true);
        GrainOn(true);
    }



    public void StopAllCoroutinesInList()
    {
        foreach(var coroutine in curCoroutine)
        {
            StopCoroutine(coroutine);
        }

        curCoroutine.Clear();
    }

}
