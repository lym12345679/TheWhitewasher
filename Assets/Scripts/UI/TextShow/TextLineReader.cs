using UnityEngine;

public class TextLineReader
{
    private string textLine;
    public DialogSortEnum DialogSortEnum;
    public Sprite LeftSprite;
    public Sprite RightSprite;
    public Sprite ShownItemSprite;
    public string dialogText;
    public GameCharacterEnum LeftCharacter;
    public GameCharacterEnum RightCharacter;
    public CGShownItemEnum ShownItem;
    public bool canRead = true;
    public TextLineReader(string textLine)
    {
        ReadTextLine(textLine);
    }

    public void ReadTextLine(string textLine)
    {
        this.textLine = textLine;
        //Debug.Log("Reading textLine:" + textLine);
        string[] lines = textLine.Split('|');
        if (lines.Length < 4)
        {
            //Debug.Log("lines.Length < 4");
            canRead = false;
            return;
        }
        /*foreach (string line in lines)
        {
            Debug.Log("line:" + line);
        }*/
        SetLeftSprite(lines[0]);
        SetRightSprite(lines[1]);
        SetDialogShow(lines[2]);
        SetShownItemSprite(lines[3]);
        dialogText = lines[4];
    }

    private void SetLeftSprite(string word)
    {
        LeftSprite = null;
        if (System.Enum.TryParse(word, true, out LeftCharacter))
        {
            //Debug.Log("Parsed enum value: " + leftCharacter.ToString());
            if (LeftCharacter != GameCharacterEnum.None)
            {
                LeftSprite = SOManager.gameCharacterSpriteSO.GetSprite(LeftCharacter);
            }
        }
        else
        {
            LeftCharacter = GameCharacterEnum.None;
            //Debug.Log("Failed to parse left enum value");
        }
    }

    private void SetRightSprite(string word)
    {
        RightSprite = null;
        if (System.Enum.TryParse(word, true, out RightCharacter))
        {
            //Debug.Log("Parsed enum value: " + rightCharacter.ToString());
            if (RightCharacter != GameCharacterEnum.None)
            {
                RightSprite = SOManager.gameCharacterSpriteSO.GetSprite(RightCharacter);
            }
        }
        else
        {
            RightCharacter = GameCharacterEnum.None;
            //Debug.Log("Failed to parse right enum value");
        }
    }
    private void SetDialogShow(string word)
    {
        word = word.ToLower();
        DialogSortEnum = DialogSortEnum.none;
        if (System.Enum.TryParse(word, true, out DialogSortEnum))
        {
            //Debug.Log("Parsed enum value: " + dialogSortEnum.ToString());
        }
        else
        {
            DialogSortEnum = DialogSortEnum.none;
            //Debug.Log("Failed to parse dialogShow enum value");
        }
    }
    private void SetShownItemSprite(string word)
    {
        ShownItemSprite = null;
        if (System.Enum.TryParse(word, true, out ShownItem))
        {
            //Debug.Log("Parsed enum value: " + ShownItem.ToString());
            if (ShownItem != CGShownItemEnum.None)
            {
                ShownItemSprite = SOManager.cgShownItemSO.GetSprite(ShownItem);
            }
        }
        else
        {
            ShownItem = CGShownItemEnum.None;
            //Debug.Log("Failed to parse shownItem enum value");
        }
    }
}
public enum DialogSortEnum
{
    left,
    right,
    none,
    all
}
