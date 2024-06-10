using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public ESoundType eSoundType;
    private AudioSource audioSource;
    public AudioSource Sorce => audioSource;

    [Header("��������"),SerializeField]
    private bool isLoop =false;

    [Header("����"), SerializeField]
    private float volume = 1f;
    [Header("����� �̸�"),SerializeField]
    private string audioPath;
    public string AudioPath { get { return audioPath; } set { audioPath = value; } }

    void Start()
    {
        audioSource= GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        audioSource.loop = isLoop;
        audioSource.volume = volume;
    }

    public void Play()
    {
        GameManager.Instance.soundMgr.PlayAudio(audioPath, audioSource, eSoundType);
    }

    public void Stop()
    {
        audioSource.Stop();
    }
    public bool CheckPlaying()
    {
        return audioSource.isPlaying;
    }
}
