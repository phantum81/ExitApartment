using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerPostProcess : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume post;
    private EventManager eventMgr;


    private Vignette vignette;
    private Grain grain;
    void Start()
    {
        eventMgr = GameManager.Instance.eventMgr;

    }

    // Update is called once per frame
    void Update()
    {

        switch (eventMgr.eStageState)
        {
            case EstageEventState.None:
                break;
            case EstageEventState.GravityReverse:
                break;
            case EstageEventState.Die12F:
                On12FDead();
                break;
        }

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

    public void On12FDead()
    {
        OnDamageVignette();
        OnMentalDamage();
    }

    
}
