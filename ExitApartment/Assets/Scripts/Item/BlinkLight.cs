using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlinkLight : MonoBehaviour
{
        
    [Header("Enable"),SerializeField]
    private bool isEnable = true;
    [Header("BlinkTime"), SerializeField]
    private float blinkTime = 1f;

    private float originIntensity;
    private float curIntensity;
    private float randomIntensity;
    private float curRange;
    private Coroutine curCoroutine;
    private Light transLight;

    private void Awake()
    {

        transLight = GetComponent<Light>();
        curIntensity = transLight.intensity;
        originIntensity = transLight.intensity;
        curRange = transLight.range;
    }



    
    void Update()
    {
        if(isEnable && curCoroutine == null)
        {
            curCoroutine = StartCoroutine(Blink());
        }
        else if(!isEnable && curCoroutine !=null)
        {
            StopCoroutine(curCoroutine);
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
                    randomIntensity = Random.Range(0.2f, 0.8f);
                }

                transLight.intensity = curIntensity;
                transLight.range = curIntensity + curRange;

                yield return null;
            }
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



}
