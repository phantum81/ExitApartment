using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public ESoundType eSoundType;
    private AudioSource audioSource;
    public AudioSource Sorce => audioSource;

    [SerializeField]
    private string audioPath;

    void Start()
    {
        audioSource= GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        GameManager.Instance.soundMgr.PlayAudio(audioPath, audioSource, eSoundType);
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
