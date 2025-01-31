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
    }
    public override void GetParams(string param)
    {
        this.param = param;
    }
    public override string SendParams()
    {
        return "关闭UI";
    }
    public void OnStartBtnClicked()
    {
        GamePlayManager.LoadScene(SceneEnum.Level1);
        AudioUtil.Play(AudioEnum.Button_Clicked, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
        Close();
    }
    public void OnSettingBtnClicked()
    {
        SettingUI.Open("1");
        AudioUtil.Play(AudioEnum.Button_Clicked, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
        Close();
    }
    public void OnLevelSelectBtnClicked()
    {
        GamePlayManager.GoToLevelSelect();
        AudioUtil.Play(AudioEnum.Button_Clicked, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
        Close();
    }
    public void OnExitBtnClicked()
    {
        GamePlayManager.ExitGame();
        AudioUtil.Play(AudioEnum.Button_Clicked, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
        Close();
    }
}
