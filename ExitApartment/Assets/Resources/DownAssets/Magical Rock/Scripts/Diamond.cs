using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Diamond : MonoBehaviour, IInteraction
{
    [Header("매직 스톤이벤트")]
    public UnityEvent onMagicStone;
    [SerializeField]
    private GameObject [] turnOffObj = new GameObject[2];
    private UnitManager unitMgr;

    void Start()
    {
        unitMgr = GameManager.Instance.unitMgr;
    }
    public void Init()
    {

    }

    public virtual void OnRayHit(Color _color)
    {


    }
    public virtual void OnInteraction(Vector3 _angle)
    {

        StartCoroutine(ShowObject());
        for(int i = 0; turnOffObj.Length > i; i++)
        {
            turnOffObj[i].SetActive(false);
        }
        onMagicStone.Invoke();
        transform.GetComponent<MeshRenderer>().enabled= false;
        transform.GetComponent<AudioSource>().enabled= false;
        
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

    IEnumerator ShowObject()
    {
        yield return new WaitForSeconds(1f);
        unitMgr.ShowObject(unitMgr.MobDic[EMobType.SkinLess].transform, true);
    }
}
