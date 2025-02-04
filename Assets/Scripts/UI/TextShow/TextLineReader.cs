using UnityEngine;

public class TextLineReader
{
    private string textLine;
    public DialogSortEnum dialogSortEnum;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public string dialogText;
    public GameCharacterEnum leftCharacter;
    public GameCharacterEnum rightCharacter;
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
        dialogText = lines[3];
    }

    private void SetLeftSprite(string word)
    {
        leftSprite = null;
        if (System.Enum.TryParse(word, true, out leftCharacter))
        {
            //Debug.Log("Parsed enum value: " + leftCharacter.ToString());
            if (leftCharacter != GameCharacterEnum.None)
            {
                leftSprite = SOManager.gameCharacterSpriteSO.GetSprite(leftCharacter);
            }
        }
        else
        {
            leftCharacter = GameCharacterEnum.None;
            Debug.Log("Failed to parse left enum value");
        }
    }

    private void SetRightSprite(string word)
    {
        rightSprite = null;
        if (System.Enum.TryParse(word, true, out rightCharacter))
        {
            //Debug.Log("Parsed enum value: " + rightCharacter.ToString());
            if (rightCharacter != GameCharacterEnum.None)
            {
                rightSprite = SOManager.gameCharacterSpriteSO.GetSprite(rightCharacter);
            }
        }
        else
        {
            rightCharacter = GameCharacterEnum.None;
            Debug.Log("Failed to parse right enum value");
        }
    }
    private void SetDialogShow(string word)
    {
        word = word.ToLower();
        dialogSortEnum = DialogSortEnum.none;
        if (System.Enum.TryParse(word, true, out dialogSortEnum))
        {
            //Debug.Log("Parsed enum value: " + dialogSortEnum.ToString());
        }
        else
        {
            dialogSortEnum = DialogSortEnum.none;
            Debug.Log("Failed to parse dialogShow enum value");
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
