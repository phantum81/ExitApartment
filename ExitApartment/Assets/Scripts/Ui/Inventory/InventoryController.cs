using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [Header("½½·Ô"), SerializeField]
    private List<Slot> slotList = new List<Slot>();
    public List<Slot> SlotList => slotList;

    private void OnValidate()
    {
        if(slotList == null)
        {
            for (int i = 0; i < transform.GetComponentsInChildren<Slot>().Length; i++)
            {
                slotList.Add(transform.GetComponentsInChildren<Slot>()[i]);
            }
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem()
    {
        //slotList.Add()
    }
}
