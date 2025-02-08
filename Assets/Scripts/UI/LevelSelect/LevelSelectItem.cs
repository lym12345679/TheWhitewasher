using System;
using MizukiTool.Audio;
using MizukiTool.UIEffect;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectItem : MonoBehaviour
{
    public SceneEnum TargetScene;
    public Image NumImg;
    public TextMeshProUGUI NumText;
    public RectTransform NumRect;
    public Image BtnBackground;
    public RectTransform TinyPeople;
    public AudioEnum ClickedSoundEffect;
    public RectTransform selfRectTransform;
    private LevelSelectItemEffect levelSelectItemEffect;
    private Sprite sceneBackground;
    private float tinyPeopleOffset = 50;
    public delegate void OnLevelSelectItemClickedCallback();
    public OnLevelSelectItemClickedCallback OnLevelSelectItemClicked;
    private bool interactable = true;
    public Action<Sprite> OnPointEnterHandle;
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
        sceneBackground = message.SceneBackground;
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
        if (interactable && !LevelSelectSceneManager.Instance.LockScene)
        {
            if (OnPointEnterHandle != null)
            {
                OnPointEnterHandle(sceneBackground);
            }
            TinyPeople.gameObject.SetActive(true);
        }

    }
    public void OnPointExit()
    {
        if (interactable && !LevelSelectSceneManager.Instance.LockScene)
        {
            TinyPeople.gameObject.SetActive(false);
        }
        //levelSelectItemEffect.StartPositionDownEffect(null);
    }
    public void OnItemClicked()
    {
        LevelSelectSceneManager.Instance.LockScene = true;
        AudioUtil.Play(ClickedSoundEffect, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
        //levelSelectItemEffect.StartPositionUpEffect((PositionEffect e) =>
        //{
        OnLevelSelectItemClicked?.Invoke();
        //});

    }
    public void SetLevelText(string s)
    {
        NumText.text = s;
    }
    public void SetLock()
    {
        interactable = false;
        this.GetComponent<Button>().interactable = false;
        BtnBackground.color = new Color(0.5f, 0.5f, 0.5f, 1);
        NumImg.color = new Color(0.5f, 0.5f, 0.5f, 0);
        NumText.color = new Color(0.5f, 0.5f, 0.5f, 0);
        TinyPeople.gameObject.SetActive(false);
    }
}

