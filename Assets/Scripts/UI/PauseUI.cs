using System;
using MizukiTool.Audio;
using MizukiTool.Box;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : GeneralBox<PauseUI, string, string>
{
    public Scrollbar BGMMusicSlider;
    public Scrollbar SoundEffectSlider;
    void Start()
    {
        GamePlayManager.PauseGame();
        AudioUtil.PauseAllLoopAudio();
        BGMMusicSlider.value = AudioMixerGroupManager.GetAudioMixerGroupValume(AudioMixerGroupEnum.BGM);
        SoundEffectSlider.value = AudioMixerGroupManager.GetAudioMixerGroupValume(AudioMixerGroupEnum.Effect);
    }
    public override void GetParams(string param)
    {
        this.param = param;
    }
    public override string SendParams()
    {
        return "关闭UI";
    }
    public void OnContinueBtnClicked()
    {
        GamePlayManager.ContinueGame();
        AudioUtil.ContinueAllLoopAudio();
        Close();
    }
    public void OnGoToLevelBtnClicked()
    {
        GamePlayManager.ContinueGame();
        SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.In, () =>
        {
            GamePlayManager.GoToLevelSelect();
        }));
        Close();
    }
    public void OnGoToMenuBtnClicked()
    {
        GamePlayManager.ContinueGame();
        SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.In, () =>
        {
            GamePlayManager.GoToMenu();
        }));

        Close();
    }
    public void OnBGMMusicValueChanged()
    {
        //TODO：对接AudioManager
        AudioMixerGroupManager.SetAudioVolume(AudioMixerGroupEnum.BGM, BGMMusicSlider.value);
    }
    public void OnSoundEffectValueChanged()
    {
        //TODO：对接AudioManager
        AudioMixerGroupManager.SetAudioVolume(AudioMixerGroupEnum.Effect, SoundEffectSlider.value);
    }
}

