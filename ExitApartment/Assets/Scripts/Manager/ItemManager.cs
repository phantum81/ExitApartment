using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class ItemManager : MonoBehaviour
{
    private Dictionary<ElevatorPan, ElevatorNumData> elevatorFloorDic = new Dictionary<ElevatorPan, ElevatorNumData>();
    public Dictionary<ElevatorPan, ElevatorNumData> ElevatorFloorDic => elevatorFloorDic;

    [SerializeField]
    private List<GameObject> itemList= new List<GameObject>();

    private void Awake()
    {
        GameManager.Instance.itemMgr = this;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void InitInteractionItem(ref List<Material> _curM, ref List<Color> _originColor, Transform _obj)
    {
        List<Renderer> ren = new List<Renderer>();
        
        Renderer[] renderers = _obj.GetComponentsInChildren<Renderer>(true);

        for(int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].gameObject.CompareTag("Iinteraction"))
            {
                ren.Add(renderers[i]);
            }
        }

        foreach (Renderer renderer in ren)
        {
            foreach (Material material in renderer.materials)
            {
                _curM.Add(material);
                _originColor.Add(material.color);
            }
        }
    }

    public void PickItem(Transform _target, Vector3 _angle, ItemData _data)
    {
        GameManager.Instance.unitMgr.PlayerCtr.PickItem(_target, _angle, _data);
    }

    public void ThrowItem(Transform _target, float _time)
    {
        GameManager.Instance.unitMgr.PlayerCtr.ThrowItem(_target, _time);

    }

    public void ChangeSpeed(float _multiply, float _useTime)
    {
        StartCoroutine(GameManager.Instance.unitMgr.PlayerCtr.ChangeSpeed(_multiply, _useTime));
    }

    public IEnumerator DrinkAlcohol()
    {
        yield return StartCoroutine(GameManager.Instance.cameraMgr.PostProcess.LensDistortionRandomValue(true));
        StartCoroutine(GameManager.Instance.cameraMgr.PostProcess.LensDistortionRandomValue(false));
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
