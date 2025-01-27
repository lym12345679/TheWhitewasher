using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneManager : MonoBehaviour
{
    [HideInInspector]
    public static LevelSceneManager Instance;
    public SceneEnum ThisScene;
    public SceneEnum NextScene;
    void Awake()
    {
        Instance = this;
        GamePlayManager.SetCurrentScene(ThisScene);
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
