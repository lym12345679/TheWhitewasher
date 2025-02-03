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
    public static float ShowInterval = 0.05f;
    private float ShowTick = 0f;
    private Stack<string> LineStack = new Stack<string>();
    private Stack<char> WordStack = new Stack<char>();

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
        ShowOneLine();
    }
}
