using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LocalzationTextureChanger : MonoBehaviour
{
    
    private Renderer targetRenderer;
    [Header("로컬라이즈에셋"),SerializeField]
    private LocalizedAsset<Sprite> localizedSprite; 
    private Material _materialInstance;
    private string _tableName;
    public string TableName => _tableName;


    private void Start()
    {
        targetRenderer = GetComponent<Renderer>();
        _tableName = localizedSprite.TableReference;
    }
    private void OnEnable()
    {
        localizedSprite.AssetChanged += OnSpriteChanged;
        localizedSprite.LoadAssetAsync();  // 트리거
    }

    private void OnDisable()
    {
        localizedSprite.AssetChanged -= OnSpriteChanged;
    }

    private void OnSpriteChanged(Sprite _newSprite)
    {
        if (_newSprite == null || targetRenderer == null) return;

        
        if (_materialInstance == null)
            _materialInstance = targetRenderer.material;

        _materialInstance.SetTexture("_MainTex", _newSprite.texture);
    }

    public void ChangeTableKey(string _table, string _key)
    {
       
        localizedSprite.AssetChanged -= OnSpriteChanged;
        localizedSprite.SetReference(_table, _key);

        localizedSprite.LoadAssetAsync().Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Sprite sprite = handle.Result;
                OnSpriteChanged(sprite);
                localizedSprite.AssetChanged += OnSpriteChanged;
            }
        };



    }


    

}
