using MizukiTool.Audio;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectItem : MonoBehaviour
{
    public SceneEnum TargetScene;
    public Image NumImg;
    public RectTransform NumRect;
    public Image BtnBackground;
    public RectTransform TinyPeople;
    public AudioEnum ClickedSoundEffect;
    public RectTransform selfRectTransform;
    private LevelSelectItemEffect levelSelectItemEffect;
    private float tinyPeopleOffset = 30;
    public delegate void OnLevelSelectItemClickedCallback();
    public OnLevelSelectItemClickedCallback OnLevelSelectItemClicked;
    void Awake()
    {
        selfRectTransform = this.GetComponent<RectTransform>();
        levelSelectItemEffect = this.GetComponent<LevelSelectItemEffect>();
    }
    public void SetLevelMessage(LevelSelectItemMessage message, float btnHight)
    {
        BtnBackground.sprite = message.BtnBackground;
        NumImg.sprite = message.NumSprite;
        TargetScene = message.LevelScene;
        selfRectTransform.sizeDelta = new Vector2(selfRectTransform.sizeDelta.x, btnHight);
        Vector3 posOffset = new Vector3(0, btnHight / 2 + tinyPeopleOffset, 0);
        TinyPeople.localPosition = posOffset;
    }
    public void SetTargetScene(SceneEnum scene)
    {
        TargetScene = scene;
    }
    public void GoToTargetScene()
    {
        GamePlayManager.LoadScene(TargetScene);
    }
    public void OnPointerEnter()
    {
        levelSelectItemEffect.StartPositionUpEffect(null);
    }
    public void OnPointExit()
    {
        levelSelectItemEffect.StartPositionDownEffect(null);
    }
    public void OnItemClicked()
    {
        AudioUtil.Play(ClickedSoundEffect, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
        OnLevelSelectItemClicked?.Invoke();
    }
    public void SetLock()
    {
        this.GetComponent<Button>().interactable = false;
        BtnBackground.color = new Color(0.5f, 0.5f, 0.5f, 1);
        NumImg.color = new Color(0.5f, 0.5f, 0.5f, 0);
        TinyPeople.gameObject.SetActive(false);
    }
}

