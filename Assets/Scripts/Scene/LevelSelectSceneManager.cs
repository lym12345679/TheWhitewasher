using UnityEngine;
public class LevelSelectScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelSelectUI.Open("1");
        SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.Out));
    }

}
