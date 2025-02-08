using System;
using System.Collections.Generic;
using MizukiTool.Audio;
using MizukiTool.RecyclePool;
using MizukiTool.UIEffect;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum TextShowEnum
{
    Item1
}
public class TextShowController : MonoBehaviour
{
    public RectTransform Panel;
    public Image LeftImg;
    public Image RightImg;
    public Image ShownItemImg;
    private CGShownItemEnum currentShownItemEnum;
    public static TextShowController Instance;
    private static float originalShowInterval = 0.05f;
    private static float correctedShowInterval = 0f;
    public static float ShowInterval = originalShowInterval;
    private float ShowTick = 0f;
    private Stack<TextLineReader> LineStack = new Stack<TextLineReader>();
    private Stack<char> WordStack = new Stack<char>();
    private bool IsShowing = true;
    private Action OnTextOver;
    private Image selfImg;
    private GridLayoutGroup gridLayoutGroup;
    private TextShowUIEffect textShowUIEffect;
    private int leftLongPadding = 125;
    private int leftShortPadding = 20;
    private int rightLongPadding = 275;
    private int rightShortPadding = 20;
    void Awake()
    {
        Instance = this;
        gridLayoutGroup = Panel.GetComponent<GridLayoutGroup>();
        selfImg = GetComponent<Image>();
        textShowUIEffect = GetComponent<TextShowUIEffect>();
        currentShownItemEnum = CGShownItemEnum.None;
    }

    void FixedUpdate()
    {
        if (ShowTick > 0)
        {
            ShowTick -= Time.fixedDeltaTime;
        }
        else
        {
            ShowTick = ShowInterval;
            ShowOneWord();
        }
    }
    private void ShowOneWord()
    {
        if (WordStack.Count > 0)
        {
            char word = WordStack.Pop();
            RecyclePoolUtil.Request(TextShowEnum.Item1, (RecycleCollection r) =>
            {
                r.GetMainComponent<TextMeshProUGUI>().text = word.ToString();
            }, Panel);
            if (ShowInterval > correctedShowInterval)
            {
                AudioUtil.Play(AudioEnum.SE_Word_Shown, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
            }
        }
        else
        {
            IsShowing = false;
        }
    }
    public void GetTextAsset(TextAsset textAsset)
    {
        string[] textLines = textAsset.text.Split('\n');
        for (int i = textLines.Length - 1; i >= 0; i--)
        {
            LineStack.Push(new TextLineReader(textLines[i]));
        }
        ShowOneLine();
    }
    #region 单行处理
    private bool ShowOneLine()
    {
        ClearPanel();
        if (LineStack.Count > 0)
        {
            TextLineReader line = LineStack.Pop();
            while (!line.canRead)
            {
                if (LineStack.Count > 0)
                {
                    line = LineStack.Pop();
                }
                else
                {
                    return false;
                }
            }
            SetLeftSprite(line.LeftCharacter, line.LeftSprite);
            SetRightSprite(line.RightCharacter, line.RightSprite);
            SetShownItemSprite(line.ShownItem, line.ShownItemSprite);
            SortImg(line.DialogSortEnum);

            char[] words = line.dialogText.ToCharArray();
            for (int i = words.Length - 1; i >= 0; i--)
            {
                WordStack.Push(words[i]);
            }
            if (CheckDialogText(line.dialogText))
            {
                ShowNextLine();
                return true;
            }
            return true;
        }
        return false;
    }
    //显示下一行
    public void ShowNextLine()
    {
        if (IsShowing)
        {
            ShowInterval = correctedShowInterval;
            return;
        }
        ShowInterval = originalShowInterval;
        IsShowing = true;
        if (!ShowOneLine())
        {
            if (OnTextOver != null)
            {
                //Debug.Log("该文本已经读完，执行后续操作");
                OnTextOver.Invoke();
            }
            else
            {
                Debug.Log("该文本已经读完，但你没有设置任何后续操作");
            }
        }
    }
    private void SetLeftSprite(GameCharacterEnum gameCharacterEnum, Sprite sprite)
    {
        if (gameCharacterEnum == GameCharacterEnum.None)
        {
            LeftImg.gameObject.SetActive(false);
        }
        else
        {
            LeftImg.gameObject.SetActive(true);
            LeftImg.sprite = sprite;
        }
    }
    private void SetRightSprite(GameCharacterEnum gameCharacterEnum, Sprite sprite)
    {
        if (gameCharacterEnum == GameCharacterEnum.None)
        {
            RightImg.gameObject.SetActive(false);
        }
        else
        {
            RightImg.gameObject.SetActive(true);
            RightImg.sprite = sprite;
        }
    }
    private void SetShownItemSprite(CGShownItemEnum shownItemEnum, Sprite sprite)
    {
        if (shownItemEnum == currentShownItemEnum)
        {
            return;
        }
        if (shownItemEnum == CGShownItemEnum.None)
        {
            ItemFadeOut((FadeEffect<Image> e) =>
            {

            });
            currentShownItemEnum = CGShownItemEnum.None;
        }
        else if (currentShownItemEnum == CGShownItemEnum.None)
        {

            ItemFadeIn();
            ShownItemImg.sprite = sprite;
            currentShownItemEnum = shownItemEnum;
        }
        else
        {
            ItemFadeIn();
            ShownItemImg.sprite = sprite;
        }
    }
    private void ItemFadeIn(Action<FadeEffect<Image>> endHander = null)
    {
        textShowUIEffect.StartShownItemFadeIn(ShownItemImg, endHander);
        textShowUIEffect.StartbackgroundFadeIn(selfImg);

    }
    private void ItemFadeOut(Action<FadeEffect<Image>> endHander = null)
    {
        textShowUIEffect.StartShownItemFadeOut(ShownItemImg, endHander);
        textShowUIEffect.StartbackgroundFadeOut(selfImg);
    }
    //排序
    private void SortImg(DialogSortEnum dialogSortEnum)
    {
        switch (dialogSortEnum)
        {
            case DialogSortEnum.left:
                {
                    gridLayoutGroup.padding.left = leftLongPadding;
                    gridLayoutGroup.padding.right = rightShortPadding;
                    LockCharacter(RightImg);
                    UnLockCharacter(LeftImg);
                    SortSilbing(LeftImg.gameObject, Panel.gameObject, RightImg.gameObject);
                }
                break;
            case DialogSortEnum.right:
                {
                    gridLayoutGroup.padding.left = leftShortPadding;
                    gridLayoutGroup.padding.right = rightLongPadding;
                    LockCharacter(LeftImg);
                    UnLockCharacter(RightImg);
                    SortSilbing(RightImg.gameObject, Panel.gameObject, LeftImg.gameObject);
                }
                break;
            case DialogSortEnum.none:
                {
                    gridLayoutGroup.padding.left = leftShortPadding;
                    gridLayoutGroup.padding.right = rightShortPadding;
                    LockCharacter(LeftImg);
                    LockCharacter(RightImg);
                    SortSilbing(Panel.gameObject, LeftImg.gameObject, RightImg.gameObject);
                }
                break;
            case DialogSortEnum.all:
                {
                    gridLayoutGroup.padding.left = leftLongPadding;
                    gridLayoutGroup.padding.right = rightLongPadding;
                    UnLockCharacter(LeftImg);
                    UnLockCharacter(RightImg);
                    SortSilbing(LeftImg.gameObject, RightImg.gameObject, Panel.gameObject);
                }
                break;
            default:
                break;
        }
    }
    //排序
    private void SortSilbing(GameObject first, GameObject second, GameObject third)
    {
        first.transform.SetAsFirstSibling();
        second.transform.SetAsFirstSibling();
        third.transform.SetAsFirstSibling();
    }
    //锁定
    private void LockCharacter(Image img)
    {
        img.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }
    //解锁
    private void UnLockCharacter(Image img)
    {
        img.color = new Color(1, 1, 1, 1);
    }
    #endregion
    #region 其他
    private void ClearPanel()
    {
        for (int i = Panel.childCount - 1; i >= 0; i--)
        {
            RecyclePoolUtil.ReturnToPool(Panel.GetChild(i).gameObject);
        }
    }
    public void SetEndHander(Action endHander)
    {
        OnTextOver = endHander;
    }
    public void OpenSettingUI()
    {
        GamePlayManager.PauseGame();
        SettingUI.Open(new SettingUIMessage()
        {
            EndHander = () =>
            {
                GamePlayManager.ContinueGame();
            }
        });
    }
    private bool CheckDialogText(string text)
    {
        if (text.Length <= 2)
        {
            Panel.gameObject.SetActive(false);
        }
        else
        {
            Panel.gameObject.SetActive(true);
        }
        if (text.Length <= 2 && text[0] == '1')
        {
            //Debug.Log("检测到特殊字符:" + text[0]);
            IsShowing = false;
            return true;
        }
        return false;
    }
    #endregion
}
