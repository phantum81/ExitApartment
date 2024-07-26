using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    public string sceneToLoad;
    public GameData gameData;

    void Start()
    {
        // ��: PlayerPrefs�� ����Ͽ� �ε��� �� �̸� ����

        sceneToLoad = gameData.sceneToLoad;
        // �񵿱� �� �ε� �ڷ�ƾ ����
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        // �񵿱� �� �ε� ����
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;

        // �ε� ����� ������Ʈ
        while (!asyncOperation.isDone)
        {
            // �ε��� ���� �Ϸ�Ǹ� �� Ȱ��ȭ
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
