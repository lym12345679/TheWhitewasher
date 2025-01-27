using System.Collections;
using System.Collections.Generic;
using MizukiTool.Box;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : GeneralBox<SettingUI, string, string>
{
    public Scrollbar BGMMusicSlider;
    public Scrollbar SoundEffectSlider;
    public override void GetParams(string param)
    {
        this.param = param;
    }
    public override string SendParams()
    {
        return "关闭UI";
    }
    public void OnReturnBtnClicked()
    {
        Close();
    }
    void Start()
    {
        BGMMusicSlider.value = StaticDatas.BGMMusicVolume;
        SoundEffectSlider.value = StaticDatas.SoundEffectVolume;
    }
    public void OnBGMMusicValueChanged()
    {
        //TODO:对接AudioManager
        StaticDatas.BGMMusicVolume = BGMMusicSlider.value;
    }
    public void OnSoundEffectValueChanged()
    {
        //TODO:对接AudioManager
        StaticDatas.SoundEffectVolume = BGMMusicSlider.value;
    }
}
