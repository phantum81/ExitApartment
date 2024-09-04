using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> AudioDic => audioDic;


    private Dictionary<int, string> soundList = new Dictionary<int, string>();
    /// <summary>
    /// 0~11 Player Channel
    /// </summary>
    public Dictionary<int, string> SoundList => soundList;

    [Header("Bgm 컨트롤러"),SerializeField]
    private BgmController bgmCtr;
    public BgmController BgmCtr => bgmCtr;

    private void Awake()
    {

        GameManager.Instance.soundMgr = this;
        //DontDestroyOnLoad(gameObject);
        SoundInit();
        
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }



    public AudioClip GetOrAddAudioClip(string _path)
    {
        AudioClip audioClip = null;

        if (_path.Contains("Sounds/") == false)
            _path = $"Sounds/{_path}";


        if (audioDic.TryGetValue(_path, out audioClip) == false)
        {
            audioClip = Resources.Load<AudioClip>(_path);
            audioDic.Add(_path, audioClip);
        }


        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {_path}");

        return audioClip;
    }

    public void PlayAudio(string _path, AudioSource _sorce, ESoundType _type ,float _pitch = 1f)
    {
        AudioClip _clip = GetOrAddAudioClip(_path);
        if (_clip == null)
            return;

        if(_type == ESoundType.Effect)
        {
            _sorce.pitch = _pitch;
            _sorce.PlayOneShot(_clip, _pitch);
        }
        else
        {
            if (_sorce.isPlaying)
                _sorce.Stop();

            _sorce.pitch = _pitch;
            _sorce.clip = _clip;
            _sorce.Play();
        }

       
    }
    

    private void SoundInit()
    {
        soundList.Add(0, "InsideRoomWalkSound");
        soundList.Add(1, "InsideRoomRunSound");
        soundList.Add(2, "LongGrassWalkSound");
        soundList.Add(3, "LongGrassRunSound");
        soundList.Add(4, "GrassLandWalkSound");
        soundList.Add(5, "GrassLandRunSound");
        soundList.Add(6, "LongGrassWalkSound");
        soundList.Add(7, "LongGrassRunSound");
        soundList.Add(8, "Drink");
        soundList.Add(9, "PutOnItem");
        //----움직임---


        soundList.Add(30, "OpenDoorSound");
        soundList.Add(31, "CloseDoorSound");
        soundList.Add(32, "OpenTheClosetSound");
        soundList.Add(33, "CloseTheClosetSound");
        soundList.Add(34, "ButtonPressSound");
        soundList.Add(35, "ElevatorSound");
        soundList.Add(36, "JumpScarePumpkin");
        soundList.Add(37, "BackRoomSound");
        //----효과음엘베문----



        soundList.Add(51, "GlassSound");
        soundList.Add(52, "LightBlink");
        soundList.Add(53, "AnalogSound");
        soundList.Add(54, "WriteSound");

        //---아이템 관련----

        soundList.Add(70, "Ahhh");
        soundList.Add(71, "ZombieSound");
        soundList.Add(72, "ChaseSound");
        soundList.Add(73, "ZombieAttackSound");
        


        soundList.Add(80, "ChaseBgm");
        soundList.Add(81, "Mob12FSound");
        soundList.Add(82, "Mob12FChase");
        

        //----몬스터 관련 bgm-----

        soundList.Add(101, "FallDead");
        soundList.Add(102, "FallScream");
        soundList.Add(103, "ManBreathe");
        soundList.Add(104, "ManHurtBreathe");
        soundList.Add(105, "CrabStepSound");
        soundList.Add(106, "MonsterGrassSound");


        //----몬스터 관련 bgm-----


        soundList.Add(150, "BatSound");
        soundList.Add(151, "NockDoorSound");

        //-----플레이어 효과음-----



        soundList.Add(200, "MenuBgmSound");
        soundList.Add(201, "RainSound");


        //----- Ui 사운드-----
        soundList.Add(250, "EscapeSound");

        //---맵 bgm ---
    }
    public void Init()
    {
        if(bgmCtr == null)
        {
            GameObject go = GameObject.Find("BgmSound");
            if(go != null)
            {
                bgmCtr = go.GetComponent<BgmController>();
            }
        }
    }
}

