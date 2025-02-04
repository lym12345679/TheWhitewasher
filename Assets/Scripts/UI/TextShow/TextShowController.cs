using System;
using System.Collections.Generic;
using MizukiTool.Audio;
using MizukiTool.RecyclePool;
using TMPro;
using UnityEngine;

public enum TextShowEnum
{
    Item1
}
public class TextShowController : MonoBehaviour
{
    public RectTransform Panel;
    public static TextShowController Instance;
    private static float originalShowInterval = 0.05f;
    private static float correctedShowInterval = 0f;
    public static float ShowInterval = originalShowInterval;
    private float ShowTick = 0f;
    private Stack<string> LineStack = new Stack<string>();
    private Stack<char> WordStack = new Stack<char>();
    private bool IsShowing = true;
    private Action OnTextOver;

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
    public void GetTextAsset(TextAsset textAsset)
    {
        string[] textLines = textAsset.text.Split('\n');
        for (int i = textLines.Length - 1; i >= 0; i--)
        {
            //Debug.Log(textLines[i]);
            LineStack.Push(textLines[i]);
        }
        ShowOneLine();
    }
    private bool ShowOneLine()
    {
        ClearPanel();
        if (LineStack.Count > 0)
        {
            string line = LineStack.Pop();
            char[] words = line.ToCharArray();
            for (int i = words.Length - 1; i >= 0; i--)
            {
                WordStack.Push(words[i]);
            }
            return true;
        }
        return false;
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
    private void ClearPanel()
    {
        for (int i = Panel.childCount - 1; i >= 0; i--)
        {
            RecyclePoolUtil.ReturnToPool(Panel.GetChild(i).gameObject);
        }
    }
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
                OnTextOver.Invoke();
            }
            else
            {
                Debug.Log("该文本已经读完，但你没有设置任何后续操作");
            }
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
}
