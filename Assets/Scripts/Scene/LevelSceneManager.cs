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
    void Start()
    {
        LevelSceneUI.Open("1");
        SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.Out));
    }
    public void LoadNextScene()
    {
        SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.In, () =>
        {
            GamePlayManager.LoadScene(NextScene);
        }));
    }
    public void Reset()
    {
        SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.In, () =>
        {
            GamePlayManager.LoadScene(ThisScene);
        }));

    }

    public void OnPlayerWin()
    {
        LoadNextScene();
    }
    public void OnPlayerLose()
    {
        Reset();
    }
}
