using System;
using MizukiTool.Audio;
using MizukiTool.Box;
using UnityEngine.UI;

public class SettingUI : GeneralBox<SettingUI, SettingUIMessage, string>
{
    public Scrollbar BGMMusicSlider;
    public GeneralScrollbar BGMScrollbarController;
    public Scrollbar SoundEffectSlider;
    public GeneralScrollbar SoundEffectScrollbarController;
    public AudioEnum SettingBGM;
    public override void GetParams(SettingUIMessage param)
    {
        this.param = param;
    }
    public override string SendParams()
    {
        return "关闭UI";
    }
    public override void Close()
    {
        if (param.EndHander != null)
        {
            param.EndHander();
        }
        base.Close();
    }
    public void OnReturnBtnClicked()
    {
        Close();
    }
    void Start()
    {
        BGMMusicSlider.value = AudioMixerGroupManager.GetAudioMixerGroupValume(AudioMixerGroupEnum.BGM);
        SoundEffectSlider.value = AudioMixerGroupManager.GetAudioMixerGroupValume(AudioMixerGroupEnum.Effect);
        /*if (!AudioUtil.CheckEnumInLoopAudio(SettingBGM))
        {
            AudioUtil.ReturnAllLoopAudio();
            AudioUtil.Play(SettingBGM, AudioMixerGroupEnum.BGM, AudioPlayMod.Loop);
        }*/
        /*BGMScrollbarController.StartUse();
        SoundEffectScrollbarController.StartUse();*/
    }
    public void OnBGMMusicValueChanged()
    {
        AudioMixerGroupManager.SetAudioVolume(AudioMixerGroupEnum.BGM, BGMMusicSlider.value);
    }
    public void OnSoundEffectValueChanged()
    {
        AudioMixerGroupManager.SetAudioVolume(AudioMixerGroupEnum.Effect, SoundEffectSlider.value);
    }
}
public class SettingUIMessage
{
    public Action EndHander;
}
