using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ItemManager : MonoBehaviour
{
    private Dictionary<ElevatorPan, ElevatorNumData> elevatorFloorDic = new Dictionary<ElevatorPan, ElevatorNumData>();
    public Dictionary<ElevatorPan, ElevatorNumData> ElevatorFloorDic => elevatorFloorDic;

    [SerializeField]
    private List<GameObject> itemList= new List<GameObject>();


    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void InitInteractionItem(out Material _curM, out Color _originColor, Transform _obj)
    {
        _curM = _obj.GetComponent<Renderer>().material;
        _originColor = _curM.color;
    }




    public void Init()
    {
        GameObject[] objsInteraction = GameObject.FindGameObjectsWithTag("Iinteraction");

        for (int i = 0; i < objsInteraction.Length; i++)
        {
            objsInteraction[i].GetComponent<IInteraction>()?.Init();
            itemList.Add(objsInteraction[i]);
        }


        for (int i = 0; i < itemList.Count; i++)
        {

            ElevatorPan pan = itemList[i].GetComponent<ElevatorPan>();
            if (pan != null)
            {
                elevatorFloorDic.Add(pan, new ElevatorNumData(itemList[i].transform.GetComponentInChildren<TextMeshPro>().text));
            }


        }
    }
}
