
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    private ItemData data;
    public ItemData Data 
    {
        get { return data; } 
        set 
        { 
            data = value;
            if (data != null)
            {
                image.sprite = data.ItemImage;
                image.color = new Color(1, 1, 1, 1);

            }
            else
                image.color = new Color(1, 1, 1, 0);
        } 
    }

    private EItemType eItemType;
    public EItemType EItemType => eItemType;

}
