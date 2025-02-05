using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "LevelSelectItemMessageSO", menuName = "ScriptableObjects/LevelSelectItemMessageSO")]
public class LevelSelectItemMessageSO : ScriptableObject
{
    public List<LevelSelectItemMessage> LevelSelectItemMessages = new List<LevelSelectItemMessage>();
    private void OnValidate()
    {
        for (int i = 0; i < LevelSelectItemMessages.Count; i++)
        {
            LevelSelectItemMessages[i].Name = LevelSelectItemMessages[i].LevelScene.ToString();
        }
    }
    public LevelSelectItemMessage GetLevelSelectItemMessage(SceneEnum sceneEnum)
    {
        return LevelSelectItemMessages.Find(x => x.LevelScene == sceneEnum);
    }
}
[Serializable]
public class LevelSelectItemMessage
{
    [HideInInspector]
    public string Name;
    public SceneEnum LevelScene;
    public Sprite BtnBackground;
    public Sprite SceneBackground;
    public Sprite NumSprite;
    public Sprite BottomSprite;
    public Sprite TopSprite;
}
