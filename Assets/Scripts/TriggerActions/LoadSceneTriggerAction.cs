using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LoadSceneTriggerAction", menuName = "TriggerActions/LoadScene")]
public class LoadSceneTriggerAction : ScriptableObject
{
    [Header("Settings")]
    public string SceneToLoad;

    public void StartLoadSceneAsync()
    {
        GameObject loader = new("SceneLoader");
        DontDestroyOnLoad(loader);
        CoroutineRunner coroutineRunner = loader.AddComponent<CoroutineRunner>();
        coroutineRunner.StartCoroutine(LoadSceneAsync(coroutineRunner.gameObject));
    }
    private IEnumerator LoadSceneAsync(GameObject loaderObj)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(SceneToLoad);

        if (load == null)
        {
            Debug.LogError($"Scene `{SceneToLoad}` doesn't exist");
            yield break;
        }

        LoadingScreenUI loadingUI = FindAnyObjectByType<LoadingScreenUI>(FindObjectsInactive.Include);
        if (loadingUI == null) Debug.Log("`LoadingScreenUI` component wasnt found in scene so no loading screen will be enabled");

        loadingUI?.Enable();

        while (!load.isDone)
        {
            loadingUI?.SetSliderValue(Mathf.Clamp01(load.progress / 0.9f));
            yield return null;
        }
        Destroy(loaderObj);
    }
}
