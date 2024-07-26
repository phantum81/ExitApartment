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
        // 예: PlayerPrefs를 사용하여 로드할 씬 이름 설정

        sceneToLoad = gameData.sceneToLoad;
        // 비동기 씬 로드 코루틴 시작
        StartCoroutine(LoadAsyncScene());
    }

    IEnumerator LoadAsyncScene()
    {
        // 비동기 씬 로드 시작
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;

        // 로딩 진행률 업데이트
        while (!asyncOperation.isDone)
        {
            // 로딩이 거의 완료되면 씬 활성화
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
