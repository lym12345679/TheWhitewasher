using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SceneEnum
{
    Menu,
    LevelSelect,
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
    public static SceneEnum CurrentScene
    {
        get
        {
            return StaticDatas.CurrentScene;
        }
        set
        {
            StaticDatas.CurrentScene = value;
        }
    }

    public static int MaxLevel
    {
        get
        {
            return StaticDatas.MaxLevel;
        }
        set
        {
            StaticDatas.MaxLevel = value;
        }
    }
    public static bool IsPlayerFinishAllLevel
    {
        get
        {
            return StaticDatas.IsPlayerFinishAllLevel;
        }
    }
    public static void PauseGame()
    {
        Time.timeScale = 0;
    }
    public static void ContinueGame()
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
        SaveSystem.SaveData();
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
    }
    public static void ResetGame()
    {
        LoadScene(CurrentScene);
    }
    public static void GoToMenu()
    {
        LoadScene(SceneEnum.Menu);
    }
    public static void GoToLevelSelect()
    {
        LoadScene(SceneEnum.LevelSelect);
    }
    public static void ExitGame()
    {
        Application.Quit();
    }
    public static void UnlockAllLevel()
    {
        MaxLevel = 9;
    }
}
