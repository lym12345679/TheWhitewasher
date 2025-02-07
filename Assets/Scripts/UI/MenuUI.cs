using System.Collections;
using System.Collections.Generic;
using MizukiTool.Audio;
using MizukiTool.Box;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : GeneralBox<MenuUI, string, string>
{
    public RectTransform BackGround1;
    public RectTransform BackGround2;
    public RectTransform Panel;
    public AudioEnum MenuBGM;
    public bool IsPlayerFinishAllLevel
    {
        get
        {
            return GamePlayManager.IsPlayerFinishAllLevel;
        }
    }
    void Start()
    {
        if (IsPlayerFinishAllLevel)
        {
            BackGround1.gameObject.SetActive(false);
            BackGround2.gameObject.SetActive(true);
        }
        else
        {
            BackGround1.gameObject.SetActive(true);
            BackGround2.gameObject.SetActive(false);
        }
        if (!AudioUtil.CheckEnumInLoopAudio(MenuBGM))
        {
            AudioUtil.ReturnAllLoopAudio();
            AudioUtil.Play(MenuBGM, AudioMixerGroupEnum.BGM, AudioPlayMod.Loop);
        }
    }
    public override void GetParams(string param)
    {
        this.param = param;
    }
    public override string SendParams()
    {
        return "关闭UI";
    }
    public void OnSettingBtnClicked()
    {
        Panel.gameObject.SetActive(false);
        SettingUI.Open(new SettingUIMessage()
        {
            EndHander = () =>
            {
                Panel.gameObject.SetActive(true);
            }
        });

    }
    public void OnLevelSelectBtnClicked()
    {
        if (!StaticDatas.IsBeginCGShown)
        {
            StaticDatas.IsBeginCGShown = true;
            CGUI.Open(new CGGroup()
            {
                CGEnum = CGEnum.Begin,
                EndHander = () =>
                {
                    SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.In, () =>
                    {
                        GamePlayManager.GoToLevelSelect();
                    }));
                }
            });
            Close();
        }
        else
        {
            SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.In, () =>
            {
                Close();
                GamePlayManager.GoToLevelSelect();
            }));
        }

        /*SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.In, () =>
        {
            GamePlayManager.GoToLevelSelect();
        }));*/
    }
    public void OnExitBtnClicked()
    {
        GamePlayManager.ExitGame();
        Close();
    }
    public void OnUnlockAllLevelBtnClicked()
    {
        GamePlayManager.UnlockAllLevel();
    }
}
