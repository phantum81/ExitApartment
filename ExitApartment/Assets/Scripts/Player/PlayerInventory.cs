
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventory : MonoBehaviour
{
    [Header("줍는 위치"), SerializeField]
    private Transform pickTransform;
    public Transform PickTransform => pickTransform;

    private List<GameObject> inventoryItemList = new List<GameObject>();
    public List<GameObject> InventoryItemList => inventoryItemList;

    private Transform curItem;
    public Transform CurItem { get {return curItem ; } set { curItem = value; } }

    private InputManager inputMgr;
    

    private void Start()
    {
        inputMgr = GameManager.Instance.inputMgr;
        
    }

    private void Update()
    {
        SetCurItem();
    }


    public void AddList(Transform _item)
    {
        inventoryItemList.Add(_item.gameObject);
        Item pickItme = _item.GetComponent<Item>();
        if(!pickItme.isCoPlaying)
            StartCoroutine(pickItme.CoInitPosition());
    }


    public void RemoveList(Transform _item)
    {
        for(int i =0; i < inventoryItemList.Count; i++)
        {
            if(inventoryItemList[i].gameObject == _item.gameObject)
            {
                inventoryItemList[i].transform.parent = null;
                
                inventoryItemList.RemoveAt(i);
            }
        }
    }

    public void ChangeItem(int _num)
    {
        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            if(UiManager.Instance.inGameCtr.InvenCtr.SlotList[_num].Data == inventoryItemList[i].GetComponent<Item>().SOItemData)
            {
                if(curItem != null)
                {
                    
                    curItem.gameObject.SetActive(false);
                }

                curItem = inventoryItemList[i].transform;
                
                curItem.gameObject.SetActive(true);
                

            }
            else if (!UiManager.Instance.inGameCtr.InvenCtr.SlotList[_num].Data)
            {
                if (curItem != null)
                {                    
                    curItem.gameObject.SetActive(false);
                }
            }
            
        }
        


    }

    private void SetCurItem()
    {

        for (int i = 0; i < 9; i++)
        {
            EuserAction action = (EuserAction)(i + 10); // EuserAction.One부터 EuserAction.Nine까지
            if (inputMgr.InputDic.ContainsKey(action) && inputMgr.InputDic[action])
            {
                ChangeItem(i);
                
                break; 
            }
        }


    }


}
