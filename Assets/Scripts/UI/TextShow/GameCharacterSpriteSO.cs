using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameCharacterSpriteSO", menuName = "ScriptableObjects/GameCharacterSpriteSO")]
public class GameCharacterSpriteSO : ScriptableObject
{
    public List<GameCharacterSprite> GameCharacterSpriteList = new List<GameCharacterSprite>();
    private void OnValidate()
    {
        foreach (var item in GameCharacterSpriteList)
        {
            item.Name = item.GameCharacterEnum.ToString();
        }
    }
    public Sprite GetSprite(GameCharacterEnum gameCharacterEnum)
    {
        foreach (var item in GameCharacterSpriteList)
        {
            if (item.GameCharacterEnum == gameCharacterEnum)
            {
                return item.Sprite;
            }
        }
        return null;
    }
}
[Serializable]
public class GameCharacterSprite
{
    [HideInInspector]
    public string Name;
    public GameCharacterEnum GameCharacterEnum;
    public Sprite Sprite;
}

public enum GameCharacterEnum
{
    None,
    墨灵,
    画师,
}