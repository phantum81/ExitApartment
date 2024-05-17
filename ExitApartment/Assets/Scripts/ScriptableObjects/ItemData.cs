using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableData/ItemData", order = int.MaxValue)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private string itemName;
    public string ItemName => itemName;
    [SerializeField]
    private Sprite itemImage;
    public Sprite ItemImage => itemImage;

}
