using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightItem : Item
{
    private GameObject lightGo;

    private EFloorType eFloorType = EFloorType.Home15EB;

    public override void Init()
    {
        base.Init();
        eItemType = EItemType.FlashLight;
        lightGo = transform.GetComponentInChildren<Light>().gameObject;
        lightGo.SetActive(false);
    }

    public override void OnRayHit(Color _color)
    {
        base.OnRayHit(_color);

    }
    public override void OnInteraction(Vector3 _angle)
    {
        base.OnInteraction(new Vector3(90f,0f,0f));

    }
    public override EInteractionType OnGetType()
    {
        return EInteractionType.Pick;
    }
    public override void OnRayOut()
    {
        base.OnRayOut();
    }

    public override void OnUseItem()
    {

        lightGo.SetActive(!lightGo.activeSelf);

    }

    public override void OnThrowItem()
    {
        base.OnThrowItem();
    }


    public override void OnGravityChange()
    {
        base.OnGravityChange();
    }
    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);


    }

    private void InitPosition()
    {
        if (transform.position != originPos && transform.parent == null)
        {
            transform.position = originPos;
            transform.rotation = originRotate;
            transform.gameObject.SetActive(true);
        }
    }

    public override IEnumerator CoInitPosition()
    {
        isCoPlaying = true;
        while (true)
        {
            yield return new WaitUntil(() => eFloorType != GameManager.Instance.unitMgr.ElevatorCtr.eCurFloor);
            InitPosition();
            eFloorType = GameManager.Instance.unitMgr.ElevatorCtr.eCurFloor;
        }
    }
    
}
