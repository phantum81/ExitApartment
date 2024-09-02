using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyPlayerController : MonoBehaviour
{
    private List<Material> materials = new List<Material>();
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform startPos;
    [Header("속력"),SerializeField]
    private float speed = 5f;

    [Header("회전속력"), SerializeField]
    private float rotSpeed = 5f;
    private float distance;
    private InputManager inputMgr;

    private Animator anim;
    private Rigidbody rigd;
    private SoundController soundCtr;
    private SoundManager soundMgr;
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShader();
        Move();
        if (inputMgr.InputDir != Vector3.zero)
            anim.SetBool("Walk", true);
        else
            anim.SetBool("Walk", false);
    }

    private void Init()
    {
        materials = GetComponentsInChildren<Renderer>()   
                .SelectMany(renderer => renderer.materials) 
                .ToList();
        distance = Vector3.Distance(startPos.position, target.position);
        inputMgr = GameManager.Instance.inputMgr;
        rigd = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        soundCtr = GetComponent<SoundController>();
        soundMgr= GameManager.Instance.soundMgr;

        soundCtr.AudioPath = soundMgr.SoundList[0];
    }

    private void UpdateShader()
    {
        float myDistance = 0f;
        float normalDistance = 0f;
        
        myDistance = Vector3.Distance(transform.position, target.position);
        normalDistance = Normalize(myDistance, 0f, distance);
        
        for(int i =0; i < materials.Count; i++)
        {
            materials[i].SetFloat("_Transparency", 1f - normalDistance );
            //materials[i].SetColor("_Color", Color.Lerp(Color.white, Color.black, 1f- normalDistance));
            materials[i].SetColor("_RimColor", Color.Lerp(Color.white, Color.black, normalDistance));
            materials[i].SetColor("_Color", Color.black);

        }


    }

    
    public float Normalize(float value, float min, float max)
    {
        if (max - min == 0)
        {
            Debug.LogWarning("min과 max가 동일하여 0으로 나눌 수 없습니다.");
            return 0;
        }

        return (value - min) / (max - min);
    }


    private void Move()
    {
        Vector3 inputDir = inputMgr.InputDir.normalized;

        float fallSpeed = rigd.velocity.y;
        if (inputDir != Vector3.zero)
        {
            if(!soundCtr.CheckPlaying())
                soundCtr.Play();
            inputDir *= speed;
            Quaternion targetRotation = Quaternion.LookRotation(inputDir);

            // 캐릭터를 부드럽게 회전 (회전 속도를 원하는 대로 조절 가능)
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotSpeed);
        }
        else
            inputDir = Vector3.zero;

        inputDir.y = fallSpeed; // 떨어지는 속도 초기화
        rigd.velocity = inputDir;
        
    }


}
