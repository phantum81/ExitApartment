using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorImage;
    private Texture2D resizedCursor;
    private Vector2 hotSpot = Vector2.zero;
    private int previousScreenWidth = 0;
    private int previousScreenHeight = 0;
    void Start()
    {
        previousScreenWidth = Screen.width;
        previousScreenHeight = Screen.height;
        UpdateCursor();

        //Cursor.SetCursor(cursorImage, hotSpot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.eGameState == EgameState.InGame)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (Screen.width != previousScreenWidth || Screen.height != previousScreenHeight)
            {
                UpdateCursor();
            }
        }
    }


    void UpdateCursor()
    {
        previousScreenWidth = Screen.width;
        previousScreenHeight = Screen.height;

        // ȭ�� ũ�⿡ ����Ͽ� Ŀ�� ũ�� ���� (��: ȭ�� ������ 5%)
        float cursorSize = Screen.height * 0.05f;
        int width = (int)(cursorImage.width * (cursorSize / cursorImage.height));
        int height = (int)cursorSize;

        //if (resizedCursor != null)
        //{
        //    Destroy(resizedCursor); // ������ ������ �ؽ�ó�� �����Ͽ� �޸� ������ ����
        //}

        resizedCursor = ResizeTexture(cursorImage, width, height);

        // �ֽ����� ���ο� Ŀ�� ũ�⿡ �°� ����
        Vector2 newHotSpot = new Vector2(hotSpot.x * ((float)width / cursorImage.width), hotSpot.y * ((float)height / cursorImage.height));

        // ���ο� Ŀ�� ����
        Cursor.SetCursor(resizedCursor, newHotSpot, CursorMode.Auto);
    }

    Texture2D ResizeTexture(Texture2D source, int width, int height)
    {
        RenderTexture rt = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        RenderTexture.active = rt;
        Graphics.Blit(source, rt);
        Texture2D result = new Texture2D(width, height, TextureFormat.ARGB32, false);
        result.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        result.Apply();
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);
        return result;
    }
}
