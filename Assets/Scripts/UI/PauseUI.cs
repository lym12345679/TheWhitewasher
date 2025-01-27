using MizukiTool.Box;
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
        GamePlayManager.ContinueGame();
        Close();
    }
    public void OnGoToLevelBtnClicked()
    {
        GamePlayManager.ContinueGame();
        GamePlayManager.GoToLevelSelect();
        Close();
    }
    public void OnGoToMenuBtnClicked()
    {
        GamePlayManager.ContinueGame();
        GamePlayManager.GoToMenu();
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

