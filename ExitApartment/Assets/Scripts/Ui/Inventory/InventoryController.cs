using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("½½·Ô"), SerializeField]
    private List<Slot> slotList = new List<Slot>();
    public List<Slot> SlotList => slotList;


    private Slot curSlot;



    public void AddItem(ItemData _data)
    {
        if (!CheckInventoryFull())
        {
            for (int i = 0; i < slotList.Count; i++)
            {
                if (!slotList[i].Data)
                {
                    slotList[i].Data = _data;
                    break;
                }
            }
        }

    }

    public void RemoveItem(ItemData _data)
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            if(_data == slotList[i].Data)
            {
                slotList[i].Data= null;
                break;
            }
        }
    }



    public bool CheckInventoryEmpty()
    {
        for(int i =0; i < slotList.Count; i++)
        {
            if (slotList[i].Data)
            {
                return false;
            }
            
        }
        return true;
    }
    public bool CheckInventoryFull()
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            if (!slotList[i].Data)
            {
                return false;
            }

        }
        return true;
    }
}
