using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SceneEnum
{
    Menu,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    Level6,
    Level7,
    Level8,
}
public static class GamePlayManager
{
    public static SceneEnum CurrentScene;
    public static void Pause()
    {
        Time.timeScale = 0;
    }
    public static void Resume()
    {
        Time.timeScale = 1;
    }
    public static void SetCurrentScene(SceneEnum scene)
    {
        CurrentScene = scene;
    }
    public static void LoadScene(SceneEnum scene)
    {
        CurrentScene = scene;
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
    }
}
