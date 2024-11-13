using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static GameManager;

public class Diamond : MonoBehaviour, IInteraction
{
    [Header("매직 스톤이벤트")]
    public UnityEvent onMagicStone;
    [SerializeField]
    private GameObject [] turnOffObj = new GameObject[2];
    private UnitManager unitMgr;
    private SoundController soundCtr;
    private bool isDone = false;
    

    void Start()
    {
        unitMgr = GameManager.Instance.unitMgr;
        soundCtr = GetComponent<SoundController>();
        GameManager.Instance.onGetForestHumanity += SetIsClear;
        
    }
    public void Init()
    {

    }

    public virtual void OnRayHit(Color _color)
    {


    }
    public virtual void OnInteraction(Vector3 _angle)
    {


        if(!isDone)
        {
            StartCoroutine(ShowObject());
            for (int i = 0; turnOffObj.Length > i; i++)
            {
                turnOffObj[i].SetActive(false);
            }
            onMagicStone.Invoke();
            transform.GetComponent<MeshRenderer>().enabled = false;
            soundCtr.Stop();
            soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[70];
            soundCtr.SetLoop(false);
            soundCtr.Play();

            if (GameManager.Instance.onGetForestHumanity != null)
            {
                GameManager.Instance.onGetForestHumanity();

            }
            transform.GetComponent<Collider>().enabled = false;
            isDone = true;
        }

        

        
    }
    public virtual void OnRayOut()
    {

    }
    public EInteractionType OnGetType()
    {
        return EInteractionType.Use;
    }

    public void ChangeMaterialColor()
    {
        StartCoroutine(GameManager.Instance.unitMgr.ChangeMaterialColor(unitMgr.SkyBox,unitMgr.SkyboxOringinColor, Color.red, 4f));
       
    }
    public void SetIsClear()
    {
        GameManager.Instance.SetForestClearFloor(true);
    }
    IEnumerator ShowObject()
    {
        yield return new WaitForSeconds(1f);
        unitMgr.ShowObject(unitMgr.MobDic[EMobType.SkinLess].transform, true);
    }
}
