using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
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
    private MotionBlur motionBlur;
    private UnitManager unitMgr;
    
    private List<Coroutine> curCoroutine = new List<Coroutine>();
    public List<Coroutine> CurCoroutine => curCoroutine;


    private void Start()
    {
        unitMgr = GameManager.Instance.unitMgr;
    }

    public void InitPostProcess()
    {
        StopAllCoroutinesInList();

        curCoroutine.Add(StartCoroutine(PostProccessEffectOff(EpostProcessType.Vignette)));
        curCoroutine.Add(StartCoroutine(PostProccessEffectOff(EpostProcessType.Grain)));
        curCoroutine.Add(StartCoroutine(PostProccessEffectOff(EpostProcessType.ChromaticAberration)));
        curCoroutine.Add(StartCoroutine(PostProccessEffectOff(EpostProcessType.LensDistortion)));

    }
    public IEnumerator VignetteOn(float _val=0.4f , float _speed =1f,Color _col = default(Color))
    {
        if (_col == default(Color))
        {
            _col = Color.red;
        }

        if (post != null && post.profile.TryGetSettings(out vignette))
        {
            vignette.active = true;
            vignette.color.value = _col;
            vignette.center.value = new Vector2(0.5f, 0.5f);
            curCoroutine.Add(StartCoroutine(unitMgr.LerpValue(value => vignette.intensity.value = value, vignette.intensity.value, _val, _speed, 1f)));

            yield return null;
        }


    }
    public IEnumerator GrainOn(bool _bool)
    {
        

        if (post != null && post.profile.TryGetSettings(out grain))
        {
            
            if (!_bool)
            {
                yield return StartCoroutine(unitMgr.LerpValue(value => grain.intensity.value = value, grain.intensity.value, 0, 2f, 1f));
                grain.active = _bool;
            }
            else
            {
                grain.active = _bool;
                yield return StartCoroutine(unitMgr.LerpValue(value => grain.intensity.value = value, grain.intensity.value, 1, 0.5f, 1f));
                
            }
        }
        
    }

    public IEnumerator LensDistortion(bool _bool)
    {
        
        if (post != null && post.profile.TryGetSettings(out distortion))
        {
            
            if (!_bool)
            {
                yield return StartCoroutine(unitMgr.LerpValue(value => distortion.intensity.value = value, distortion.intensity.value, 0, 2f, 1f));

                distortion.active = _bool;

            }
            else
            {
                distortion.active = _bool;                
                yield return StartCoroutine(unitMgr.LerpValue(value => distortion.intensity.value = value, distortion.intensity.value, -40, 0.5f, 1f));
                
            }
                                   
        }
        
    }
    public IEnumerator LensDistortionRandomValue(bool _bool)
    {

        if (post != null && post.profile.TryGetSettings(out distortion))
        {

            if (!_bool)
            {
                yield return StartCoroutine(unitMgr.LerpValue(value => distortion.intensity.value = value, distortion.intensity.value, 0f, 2f, 1f));

                distortion.active = _bool;

            }
            else
            {
                distortion.active = _bool;
                yield return StartCoroutine(unitMgr.RandomLerpValue(value => distortion.intensity.value = value, -70f, 22f, 0.5f, 1f));

            }

        }

    }



    public IEnumerator ChromaticAberrationOn(bool _bool)
    {
        
        if (post != null && post.profile.TryGetSettings(out chromaticAber))
        {
            
            if (!_bool)
            {
                yield return StartCoroutine(unitMgr.LerpValue(value => chromaticAber.intensity.value = value, chromaticAber.intensity.value, 0, 2f, 1f));
                
                chromaticAber.active = _bool;
           
            }
            else
            {
                chromaticAber.active = _bool;
                yield return StartCoroutine(unitMgr.LerpValue(value => chromaticAber.intensity.value = value, chromaticAber.intensity.value, 1, 0.5f, 1f));
                
            }
            

        }
    }



    //IEnumerator LerpValue(Action<float> _value, float _min, float _max, float _inverseSpeed, float _lerpRatio)
    //{
    //    yield return null;
    //    float elapsedTime = 0f;
    //    while (true)
    //    {
    //        _value(Mathf.Lerp(_min, _max, elapsedTime / (_inverseSpeed * _lerpRatio)));

            
    //        if (elapsedTime >= _inverseSpeed)
    //        {
    //            break;
    //        }

    //        elapsedTime += Time.deltaTime;

    //        yield return null;
    //    }
    //}

    public IEnumerator PostProccessEffectOff(EpostProcessType _type, float _max =0f, float _inverseSpeed = 2f, float _lerpRatio =1f)
    {
        

        if (post != null)
        {

            switch (_type)
            {
                case EpostProcessType.Vignette:
                    if(post.profile.TryGetSettings(out vignette))
                    {

                    }
                        //curCoroutine.Add(StartCoroutine( LerpValue(value => vignette.intensity.value = value, vignette.intensity.value, _max, _inverseSpeed, _lerpRatio)));
                    break;
                case EpostProcessType.Grain:
                    

                    if (post.profile.TryGetSettings(out grain))
                    {
                        curCoroutine.Add(StartCoroutine(unitMgr.LerpValue(value => grain.intensity.value = value, grain.intensity.value, _max, _inverseSpeed, _lerpRatio)));
                        
                    }                        
                    break;

                case EpostProcessType.ChromaticAberration:
                    
                    
                    if (post.profile.TryGetSettings(out chromaticAber))
                    {
                        curCoroutine.Add(StartCoroutine(unitMgr.LerpValue(value => chromaticAber.intensity.value = value, chromaticAber.intensity.value, _max, _inverseSpeed, _lerpRatio)));
                        
                    }                        
                    break;

                case EpostProcessType.LensDistortion:

                    
                    if (post.profile.TryGetSettings(out distortion))
                    {
                        curCoroutine.Add(StartCoroutine(unitMgr.LerpValue(value => distortion.intensity.value = value, distortion.intensity.value, _max, _inverseSpeed, _lerpRatio)));

                       
                    }                        
                    break;
            }
            yield return null;
            
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
                        curCoroutine.Add(StartCoroutine(unitMgr.LerpValue(value => grain.intensity.value = value, grain.intensity.value, _max, _inverseSpeed, _lerpRatio)   ));
                    }
                    break;

                case EpostProcessType.ChromaticAberration:

                    if (post.profile.TryGetSettings(out chromaticAber))
                    {
                        chromaticAber.active = true;
                        curCoroutine.Add(StartCoroutine(unitMgr.LerpValue(value => chromaticAber.intensity.value = value, chromaticAber.intensity.value, _max, _inverseSpeed, _lerpRatio)));
                    }
                    break;

                case EpostProcessType.LensDistortion:
                                     
                    if  (post.profile.TryGetSettings(out distortion))
                    {
                        distortion.active = true;
                        curCoroutine.Add(StartCoroutine(unitMgr.LerpValue(value => distortion.intensity.value = value, distortion.intensity.value, _max, _inverseSpeed, _lerpRatio)));
                    }                                            
                    break;
            }
            yield return null;
            
        }
    }



    public IEnumerator CameraChange()
    {
        yield return StartCoroutine(CloseCameraVignette());
       // StartCoroutine(VignetteOn(1f,Color.black));
    }

    public IEnumerator CloseCameraVignette(float _speed = 1f, Color _col = default(Color))
    {

        if (_col == default(Color))
        {
            _col = Color.black; 
        }

        if (post != null && post.profile.TryGetSettings(out vignette))
        {
            while (true)
            {
                vignette.active = true;
                vignette.color.value = _col;
                vignette.intensity.value += Time.deltaTime * _speed;
                if (vignette.intensity.value >= 1)
                {
                    vignette.center.value = new Vector2(3f, 3f);
                    break;
                }

                yield return null;
            }
        }
        

    }


    public void AllBlackCloseCamera()
    {
        if (post != null && post.profile.TryGetSettings(out vignette))
        {
            vignette.active = true;
            vignette.intensity.value = 1f;
            vignette.center.value = new Vector2(3f, 3f);
        }
    }


    public void MentalDamagePostProccess()
    {
        StopAllCoroutinesInList();
        CurCoroutine.Add(StartCoroutine(PostProccessEffectOn(EpostProcessType.Grain)));
        CurCoroutine.Add(StartCoroutine(PostProccessEffectOn(EpostProcessType.LensDistortion, -40f)));
        CurCoroutine.Add(StartCoroutine(PostProccessEffectOn(EpostProcessType.ChromaticAberration)));
    }

    public void On12FDeadState()
    {
        
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
    public void SetMotionBlur(bool _bool)
    {
        if (post != null && post.profile.TryGetSettings(out motionBlur))
        {
            motionBlur.active = _bool;
        }
    }
}
