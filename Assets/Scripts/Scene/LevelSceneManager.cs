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
    public GameObject Destination;
    private int targetCount = 0;
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
    void FixedUpdate()
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
    public void AddDestination(GameObject destination)
    {
        Destination = destination;
        if (ThisScene == SceneEnum.Level8)
        {
            targetCount = 0;
            Destination.SetActive(false);
        }
    }
    public void ShowDestination()
    {
        if (Destination != null)
        {
            Destination.SetActive(true);
        }
    }
    public void PlayerArrive()
    {
        OnPlayerWin();
    }
    public void OnPlayerWin()
    {
        if (ThisScene == SceneEnum.Level8 && !StaticDatas.IsEndCGShown)
        {
            StaticDatas.IsEndCGShown = true;
            if (!StaticDatas.IsDialogCGShown)
            {
                StaticDatas.IsDialogCGShown = true;
                CGUI.Open(new CGGroup()
                {
                    CGEnum = CGEnum.Dialog,
                    EndHander = () =>
                    {
                        ShowEndCG();
                    }
                });
            }
            else
            {
                ShowEndCG();
            }

        }
        else
        {
            AudioUtil.Play(AudioEnum.SE_Player_Win, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
            LevelUp();
            LoadNextScene();
        }

    }
    public void PlayerCollect()
    {
        targetCount++;
        if (targetCount == 7)
        {
            ShowDestination();
        }
    }
    public void OnPlayerLose()
    {
        Reset();
    }
    public void ShowEndCG()
    {
        CGUI.Open(new CGGroup()
        {
            CGEnum = CGEnum.End,
            EndHander = () =>
            {
                AudioUtil.Play(AudioEnum.SE_Player_Win, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
                LevelUp();
                LoadNextScene();
            }
        });
    }
    public void LevelUp()
    {
        string sceneName = ThisScene.ToString();
        int currentLevel = int.Parse(sceneName.Split("Level")[1]);
        if (currentLevel == StaticDatas.MaxLevel)
        {
            StaticDatas.MaxLevel++;
        }
    }
}
