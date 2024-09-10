using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Item : MonoBehaviour, IInteraction, IUseItem, IGravityChange
{
    protected List<Color> originColor = new List<Color>();
    protected List<Material> curMaterial = new List<Material>();
    protected Rigidbody rigd;
    protected SoundController soundCtr;

    [Header("아이템 데이터"),SerializeField]
    protected ItemData soItemData;
    public ItemData SOItemData => soItemData;

    protected Vector3 originPos = Vector3.zero;
    protected Quaternion originRotate;

    [Header("아이템 타입"),SerializeField]
    protected EItemType eItemType;
    public EItemType EItemType => eItemType;

    public bool isCoPlaying = false;

    public virtual void Init()
    {
        
        GameManager.Instance.itemMgr.InitInteractionItem(ref curMaterial, ref originColor, transform);
        rigd = GetComponent<Rigidbody>();
        soundCtr = gameObject.GetComponent<SoundController>();
        originPos = transform.position;
        originRotate = transform.rotation;
    }

    public virtual void OnRayHit(Color _color)
    {
        foreach (Material mat in curMaterial)
        {
            mat.color = _color;
        }

    }
    public virtual void OnInteraction(Vector3 _angle)
    {
        soundCtr.AudioPath = GameManager.Instance.soundMgr.SoundList[9];
        soundCtr.Play();
        GameManager.Instance.itemMgr.PickItem(this.transform, _angle, soItemData);
        
    }
    public virtual void OnRayOut()
    {
        for (int i = 0; i < curMaterial.Count; i++)
        {
            curMaterial[i].color = originColor[i];
        }
    }

    public virtual void OnUseItem()
    {        
        UiManager.Instance.inGameCtr.InvenCtr.RemoveItem(soItemData);
        GameManager.Instance.unitMgr.PlayerCtr.PlayerInven.RemoveList(transform);
        transform.gameObject.SetActive(false);
    }

    public virtual void OnThrowItem()
    {
        GameManager.Instance.itemMgr.ThrowItem(this.transform);
        UiManager.Instance.inGameCtr.InvenCtr.RemoveItem(soItemData);

    }

    public virtual EInteractionType OnGetType()
    {
        return EInteractionType.Press;
    }

    public virtual void OnGravityChange()
    {
        GameManager.Instance.unitMgr.OnChangeGravity(rigd, GameManager.Instance.unitMgr.ReserveGravity, 9f);
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer != 7)
            soundCtr?.Play();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        
        ISOEventContect col = other.GetComponent<ISOEventContect>();
        col?.OnContect(ESOEventType.OnClear12F);
    }


    public virtual IEnumerator CoInitPosition()
    {
        yield return null;
    }
}
