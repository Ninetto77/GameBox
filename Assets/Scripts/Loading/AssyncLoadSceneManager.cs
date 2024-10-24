using UnityEngine.SceneManagement;
using UnityEngine;
public static class AssyncLoadSceneManager 
{
    private static string SceneID;

    public static void AddSceneToLoad(string SceneName)
    {
        SceneID = SceneName;
    }

    public static AsyncOperation LoadScene()
    {
        AsyncOperation asyncOperation;

        if (SceneID != "")
            asyncOperation = SceneManager.LoadSceneAsync(SceneID);
        else
            return null;

        return asyncOperation;
    }
}
