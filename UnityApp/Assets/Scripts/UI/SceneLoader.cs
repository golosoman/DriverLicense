using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private SceneName sceneName;

    public void LoadScene()
    {
        int index = (int)sceneName;
        SceneManager.LoadScene(index);
    }
}
