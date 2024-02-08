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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



    }
    public void OnDamageVignette()
    {

        if (post != null && post.profile.TryGetSettings(out vignette))
        {
            vignette.active = true;
        }
        
        
    }
    public void OnMentalDamage()
    {
        if (post != null && post.profile.TryGetSettings(out grain))
        {
            grain.active = true;
        }
    }

    public void On12FDeadState()
    {
        OnDamageVignette();
        OnMentalDamage();
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
                OnMentalDamage();
                break;
            case 2:
                playerState = EplayerState.Damage;
                OnDamageVignette();
                break;
            case 3:
                playerState = EplayerState.Die;
                OnMentalDamage();
                OnDamageVignette();
                break;
            default:
                playerState = EplayerState.None;
                break;

        }
    }



}
