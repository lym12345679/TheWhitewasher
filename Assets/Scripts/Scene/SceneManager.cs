using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [HideInInspector]
    public static SceneManager Instance;
    public SceneEnum ThisScene;
    public SceneEnum NextScene;
    void Awake()
    {
        Instance = this;

    }
    public void LoadNextScene()
    {
        GamePlayManager.LoadScene(NextScene);
    }
    public void Reset()
    {
        GamePlayManager.LoadScene(ThisScene);
    }

    public void OnPlayerWin()
    {
        GamePlayManager.LoadScene(NextScene);
    }
    public void OnPlayerLose()
    {
        GamePlayManager.LoadScene(ThisScene);
    }
}
