﻿using UnityEngine.SceneManagement;
using UnityEngine;
public class AssyncLoadSceneManager 
{
    private string SceneID;

    public void AddSceneToLoad(string SceneName)
    {
        SceneID = SceneName;
    }

    public AsyncOperation LoadScene()
    {
        AsyncOperation asyncOperation;

        if (SceneID != "")
            asyncOperation = SceneManager.LoadSceneAsync(SceneID);
        else
            return null;

        return asyncOperation;
    }
}
