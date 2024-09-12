using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Light))]
[RequireComponent(typeof(SoundController))]
[RequireComponent(typeof(AudioSource))]
public class BlinkLight : MonoBehaviour
{
        
    [Header("Enable"),SerializeField]
    private bool isEnable = true;
    [Header("BlinkTime"), SerializeField]
    private float blinkTime = 1f;
    [Header("SoundTime"), SerializeField]
    private float soundTime = 0.1f;

    //[Header("Random Intensity Min"),SerializeField]
    //private float randomMin = 0.2f;
    //[Header("Random Intensity Max"), SerializeField]
    //private float randomMax = 0.8f;


    private float originIntensity;
    private float curIntensity;
    private float randomIntensity;
    private float curRange;
    private Coroutine curCoroutine;
    private Coroutine soundCoroutine;
    private Light transLight;
    private SoundController soundCtr;

    [Header("Intensity"), SerializeField]
    private float intensity = 1f;
    

    private void Awake()
    {

        transLight = GetComponent<Light>();
        curIntensity = transLight.intensity;
        originIntensity = transLight.intensity;
        curRange = transLight.range;
        soundCtr= GetComponent<SoundController>();
    }

    private void Start()
    {
        
        soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[52];
        
    }


    void Update()
    {
        if(isEnable && curCoroutine == null)
        {
            curCoroutine = StartCoroutine(Blink());
            soundCoroutine = StartCoroutine(PlaySound());
        }
        else if(!isEnable && curCoroutine !=null)
        {
            StopCoroutine(soundCoroutine);
            StopCoroutine(curCoroutine);
            soundCtr.Stop();
            curCoroutine = null;
            transLight.intensity = originIntensity;
        }
    }

    IEnumerator Blink()
    {
        if(transLight != null)
        {
            while(true)
            {
                if (Mathf.Abs(randomIntensity - curIntensity) > 0.01f)
                {                    

                    if (curIntensity <= randomIntensity)
                    {
                        curIntensity += Time.deltaTime * blinkTime;
                        
                    }
                    else
                    {
                        curIntensity -= Time.deltaTime * blinkTime;
                    }
                }
                else
                {
                    randomIntensity = Random.Range(originIntensity / 2, originIntensity * intensity);
                }
                
                transLight.intensity = curIntensity;
                transLight.range = curIntensity + curRange;

                yield return null;
            }
        }
    }

    private IEnumerator PlaySound()
    {
        while(true)
        {
            soundCtr.Play();
            yield return new WaitForSeconds(soundTime);
        }
    }

    public void SetEnable(bool _enable)
    {
        isEnable = _enable;
    }

    public void SetBlinkTime(float _blinkTime)
    {
        blinkTime= _blinkTime;
    }

    public void TurnOff()
    {
        transLight.enabled = false;
        isEnable = false;
    }
    public void TurnOn()
    {
        transLight.enabled = true;
        isEnable = true;
    }
}
