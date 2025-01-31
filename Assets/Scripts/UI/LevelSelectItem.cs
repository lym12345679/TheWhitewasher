using MizukiTool.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectItem : MonoBehaviour
{
    public SceneEnum TargetScene;
    public TextMeshProUGUI LevelText;
    public delegate void OnLevelSelectItemClickedCallback();
    public OnLevelSelectItemClickedCallback OnLevelSelectItemClicked;
    public void SetLevelText(string text)
    {
        LevelText.text = text;
    }
    public void SetTargetScene(SceneEnum scene)
    {
        TargetScene = scene;
    }
    public void GoToTargetScene()
    {
        GamePlayManager.LoadScene(TargetScene);
    }
    public void OnItemClicked()
    {
        GoToTargetScene();
        AudioUtil.Play(AudioEnum.Button_Clicked, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
        OnLevelSelectItemClicked?.Invoke();
    }
    public void SetLock()
    {
        this.GetComponent<Button>().interactable = false;
    }
}
