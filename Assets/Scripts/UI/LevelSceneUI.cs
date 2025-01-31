using System.Collections;
using System.Collections.Generic;
using MizukiTool.Audio;
using MizukiTool.Box;
using UnityEngine;

public class LevelSceneUI : GeneralBox<LevelSceneUI, string, string>
{
    public static LevelSceneUI Instance;
    public void Awake()
    {
        Instance = this;
    }
    public override void GetParams(string param)
    {
        this.param = param;
    }
    public override string SendParams()
    {
        return "关闭UI";
    }

    public void OnPauseBtnClicked()
    {
        AudioUtil.Play(AudioEnum.Button_Clicked, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
        PauseUI.Open("1");
    }
    public void OnRestartBtnClicked()
    {
        AudioUtil.Play(AudioEnum.Button_Clicked, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
        GamePlayManager.ResetGame();
    }
    public void OnGoToMenuBtnClicked()
    {
        AudioUtil.Play(AudioEnum.Button_Clicked, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
        GamePlayManager.GoToMenu();
    }


}
