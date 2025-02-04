using UnityEngine;
public class LevelSelectSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static LevelSelectSceneManager Instance;
    public bool LockScene = false;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        LevelSelectUI.Open("1");
        SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.Out));
    }
    private void Update()
    {
        if (KeyboardSet.IsKeyDown(KeyEnum.Click2) && !LockScene)
        {
            SceneChangeUI.Open(new SceneChangeMessage(SceneChangeType.In, (() =>
            {
                GamePlayManager.GoToMenu();
            })));
        }
    }

}
