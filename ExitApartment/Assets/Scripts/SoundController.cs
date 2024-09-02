using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public ESoundType eSoundType;
    private AudioSource audioSource;
    public AudioSource Sorce => audioSource;

    [Header("루프여부"),SerializeField]
    private bool isLoop =false;

    [Header("볼륨"), SerializeField]
    private float volume = 1f;
    [Header("오디오 이름"),SerializeField]
    private string audioPath;
    public string AudioPath { get { return audioPath; } set { audioPath = value; } }
    private bool isPlaying = false;
    public bool IsPlaying => isPlaying;
    private float audioTime;
    public float AudioTime => audioTime;

    [Header("어웨이크 실행"), SerializeField]
    private bool isAwakePlay = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }
    void Start()
    {

        
        if (isAwakePlay)
        {
            Play();
        }
    }

    private void Update()
    {
        audioSource.loop = isLoop;
        audioSource.volume = volume;
        isPlaying = audioSource.isPlaying;
        audioTime = audioSource.time;
        

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
    public IEnumerator PlayingRandomTimeSound(float _min, float _max)
    {
        float randomTime = Random.Range(_min, _max);
        while (true)
        {
            yield return new WaitForSeconds(randomTime);
            if (!isPlaying)
                Play();
            randomTime = Random.Range(_min, _max);
        }
    }

    public void UpdateSound(string _newSoundPath)
    {
        if (AudioPath != _newSoundPath)
        {
            Stop();
            AudioPath = _newSoundPath;
        }

        if (!CheckPlaying())
        {
            Play();
        }
    }
    public void SetLoop(bool _loop)
    {
        if (_loop)
        {
            eSoundType = ESoundType.Bgm;
            isLoop = _loop;
        }
        else
        {
            eSoundType = ESoundType.Effect;
            isLoop = _loop;

        }

    }

    public void SetVolume(float _val)
    {
        volume = _val;
    }


    public void ChangeSound(string _path, bool _loop = false)
    {
        Stop();
        audioPath = _path;
        SetLoop(_loop);
        Play();
    }



}
