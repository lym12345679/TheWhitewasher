using System;
using MizukiTool.Box;

public class SceneChangeUI : GeneralBox<SceneChangeUI, SceneChangeMessage, string>
{
    public SceneChangeEffect SceneChangeEffectScript;
    public override void GetParams(SceneChangeMessage param)
    {
        base.GetParams(param);
        CheckSceneChangeType();
    }
    public void CheckSceneChangeType()
    {
        if (param.type == SceneChangeType.In)
        {
            SceneChangeEffectScript.SetSceneInEffect(() =>
            {
                param.OnSceneChangeEnd?.Invoke();
            });
        }
        else if (param.type == SceneChangeType.Out)
        {
            SceneChangeEffectScript.SetSceneOutEffect(() =>
            {
                param.OnSceneChangeEnd?.Invoke();
                Close();
            });
        }
    }
}
public class SceneChangeMessage
{
    public SceneChangeMessage(SceneChangeType type, Action OnSceneChangeEnd = null)
    {
        this.type = type;
        this.OnSceneChangeEnd = OnSceneChangeEnd;
    }
    public Action OnSceneChangeEnd;
    public SceneChangeType type;
}
public enum SceneChangeType
{
    In,
    Out
}
