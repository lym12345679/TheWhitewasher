using System.Collections;
using System.Collections.Generic;
using MizukiTool.Audio;
using MizukiTool.Box;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : GeneralBox<LevelSelectUI, string, string>
{
    public RectTransform LevelScelectContent;
    public AudioEnum LevelSelectBGM;
    public Image Background;
    private List<LevelSelectItemMessage> levelSelectItemMessageList;
    private float btnHight = 111;
    void Start()
    {
        levelSelectItemMessageList = SOManager.levelSelectItemMessageSO.LevelSelectItemMessages;
        InitLevelSelectUI();
        if (!AudioUtil.CheckEnumInLoopAudio(LevelSelectBGM))
        {
            AudioUtil.ReturnAllLoopAudio();
            AudioUtil.Play(LevelSelectBGM, AudioMixerGroupEnum.BGM, AudioPlayMod.Loop);
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
    public void OnReturnBtnClicked()
    {
        SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.In, () =>
        {
            GamePlayManager.GoToMenu();
        }));
    }
    public void InitLevelSelectUI()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefeb/UIPrefeb/LevelSelectItem");
        //初始化关卡选择界面
        for (int i = 0; i < levelSelectItemMessageList.Count; i++)
        {
            GameObject levelBtn = Instantiate(prefab);
            LevelSelectItem levelScelectItem = levelBtn.GetComponent<LevelSelectItem>();
            levelBtn.transform.SetParent(LevelScelectContent);
            levelScelectItem.SetLevelMessage(levelSelectItemMessageList[i], btnHight * (i + 1));
            levelScelectItem.OnPointEnterHandle = (sprite) =>
            {
                ChangeBackground(sprite);
            };
            levelScelectItem.OnLevelSelectItemClicked += () =>
            {
                SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.In, () =>
                {
                    GamePlayManager.LoadScene(levelScelectItem.TargetScene);
                }));
            };
            if (i == GamePlayManager.TopUnlockedLevel - 1)
            {
                ChangeBackground(levelSelectItemMessageList[i].SceneBackground);
            }
            if (i >= GamePlayManager.TopUnlockedLevel)
            {
                levelScelectItem.SetLock();
            }
        }
    }
    public void ChangeBackground(Sprite sprite)
    {
        Background.sprite = sprite;
    }
}
