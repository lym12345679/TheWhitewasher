using System.Collections;
using System.Collections.Generic;
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
            SceneChangeEffectScript.SetSceneInEffect();
        }
        else if (param.type == SceneChangeType.Out)
        {
            SceneChangeEffectScript.SetSceneOutEffect();
        }
    }
}
public class SceneChangeMessage
{
    public SceneChangeMessage(SceneChangeType type)
    {
        this.type = type;
    }
    public SceneChangeType type;
}
public enum SceneChangeType
{
    In,
    Out
}
