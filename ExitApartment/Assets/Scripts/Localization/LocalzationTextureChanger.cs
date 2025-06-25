using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LocalzationTextureChanger : MonoBehaviour
{
    public Renderer targetRenderer;
    public LocalizedAsset<Sprite> localizedSprite; 
    private Material _materialInstance;

    private void Start()
    {
        targetRenderer = GetComponent<Renderer>();
        
    }
    private void OnEnable()
    {
        localizedSprite.AssetChanged += OnSpriteChanged;
        localizedSprite.LoadAssetAsync();  // Ʈ����
    }

    private void OnDisable()
    {
        localizedSprite.AssetChanged -= OnSpriteChanged;
    }

    private void OnSpriteChanged(Sprite newSprite)
    {
        if (newSprite == null || targetRenderer == null) return;

        // ���׸��� �ν��Ͻ�ȭ (����: ���� ���׸����� ���� �ٲٸ� ��ü�� �ٲ�)
        if (_materialInstance == null)
            _materialInstance = targetRenderer.material;

        _materialInstance.SetTexture("_MainTex", newSprite.texture);
    }
}
