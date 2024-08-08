using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUiController : MonoBehaviour
{
    [SerializeField]
    private InGameUiShower inGameShower; 
    public InGameUiShower InGameUiShower => inGameShower;

    [SerializeField]
    private InventoryController invenCtr;
    public InventoryController InvenCtr => invenCtr;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     

}
