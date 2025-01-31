using System.Collections;
using System.Collections.Generic;
using MizukiTool.Audio;
using UnityEngine;

public class LevelSceneManager : MonoBehaviour
{
    [HideInInspector]
    public static LevelSceneManager Instance;
    public SceneEnum ThisScene;
    public SceneEnum NextScene;
    public AudioEnum BGMEnum;
    void Awake()
    {
        Instance = this;
        GamePlayManager.SetCurrentScene(ThisScene);
        if (!AudioUtil.CheckEnumInLoopAudio(BGMEnum))
        {
            AudioUtil.ReturnAllLoopAudio();
            AudioUtil.Play(BGMEnum, AudioMixerGroupEnum.BGM, AudioPlayMod.Loop);
        }
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
