using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerPostProcess : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume post;
    private EplayerState playerState;

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
            
            if (!_bool)
            {
                grain.active = _bool;
                //grain.intensity.value = 0;
                return;
            }
            else
            {
                grain.active = _bool;
                //while (grain.intensity.value <= 1f)
                //{
                //    grain.intensity.value += Time.deltaTime * 0.3f;
                //}
            }
        }
    }

    public IEnumerator LensDistortion(bool _bool)
    {
        
        if (post != null && post.profile.TryGetSettings(out distortion))
        {
            
            if (!_bool)
            {
                distortion.active = _bool;
                distortion.intensity.value = 0;
                
            }
            else
            {
                distortion.active = _bool;
                yield return StartCoroutine(LerpVlaue(0, -40, 1f, 1f));
            }
           
                        
        }
        

    }


    IEnumerator LerpVlaue(float _min, float _max, float _time, float _speed)
    {
        float elapsedTime = 0f;
        while (true)
        {
            distortion.intensity.value = Mathf.Lerp(_min, _max, elapsedTime / (_time* _speed));

            
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

    public void ChangePlayerState(int _state)
    {
        switch (_state)
        {
            case 0:
                playerState = EplayerState.None;
                break;
            case 1:
                playerState = EplayerState.MentalDamage;
                
               
                break;
            case 2:
                playerState = EplayerState.Damage;
                
                break;
            case 3:
                playerState = EplayerState.Die;

                break;
            default:
                playerState = EplayerState.None;
                break;

        }
    }



}
