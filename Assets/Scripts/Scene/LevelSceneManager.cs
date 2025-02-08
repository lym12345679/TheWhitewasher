using System.Collections;
using System.Collections.Generic;
using MizukiTool.Audio;
using UnityEngine;
using UnityEngine.UI;


public class LevelSceneManager : MonoBehaviour
{
    [HideInInspector]
    public static LevelSceneManager Instance;
    public SceneEnum ThisScene;
    public SceneEnum NextScene;
    public AudioEnum BGMEnum;
    public AudioEnum GameFailAudio;
    public SpriteRenderer Background;
    public Image BGimg;
    public GameObject Destination;
    //public GameObject Camera;
    //private Camera camera;
    public Canvas BGcanvas;

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
        BGimg.sprite = levelSelectItemMessage.SceneBackground;
        BGimg.color = new Color(1, 1, 1, 1);
        //BGimg.gameObject.SetActive(true);
        //Camera = GameObject.Find("Main Camera");
        //camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //得到主场景的背景canvas
        BGcanvas.worldCamera = Camera.main;
    }
    void FixedUpdate()
    {
        //transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, Background.transform.position.z);
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
        PlayerController.Instance.IsPlayerWin = true;
        Debug.Log("Player Win");
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
            StaticDatas.MaxLevel = 9;
        }
        else
        {
            AudioUtil.Play(AudioEnum.SE_Player_Win, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
            LevelUp();
            LoadNextScene();
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
