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
        BGMMusicSlider.value = StaticDatas.BGMMusicVolume;
        SoundEffectSlider.value = StaticDatas.SoundEffectVolume;
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
        AudioUtil.Play(AudioEnum.BGM_Arknight_Babel1, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal, (entry) =>
        {
            AudioUtil.Play(AudioEnum.BGM_Arknight_Babel2, AudioMixerGroupEnum.Effect, AudioPlayMod.Loop);
        });
        GamePlayManager.ContinueGame();
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
        StaticDatas.BGMMusicVolume = BGMMusicSlider.value;
    }
    public void OnSoundEffectValueChanged()
    {
        //TODO：对接AudioManager
        StaticDatas.SoundEffectVolume = SoundEffectSlider.value;
    }
}

