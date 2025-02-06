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
    public AudioEnum GameFailAudio;
    public SpriteRenderer Background;
    private Vector3 cameraPosition
    {
        get
        {
            return CameraController.Instance.transform.position;
        }
    }
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
        LevelSelectItemMessage levelSelectItemMessage = SOManager.levelSelectItemMessageSO.GetLevelSelectItemMessage(ThisScene);

        LevelSceneUI.Open(new LevelSceneMessage()
        {
            BottomBackground = levelSelectItemMessage.BottomSprite,
            currentLevel = int.Parse(ThisScene.ToString().Split("Level")[1])
        });
        SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.Out));
        Background.sprite = levelSelectItemMessage.SceneBackground;
    }
    void Update()
    {
        Background.transform.position = new Vector3(cameraPosition.x, cameraPosition.y, Background.transform.position.z);
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
        AudioUtil.Play(AudioEnum.SE_Player_Win, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
        LoadNextScene();
    }
    public void OnPlayerLose()
    {
        Reset();
    }
}
