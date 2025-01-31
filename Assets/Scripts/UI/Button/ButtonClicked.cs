
using MizukiTool.Audio;
using UnityEngine;

public class ButtonClicked : MonoBehaviour
{
    public AudioEnum ClickedSoundEffect;
    public void OnButtonClicked()
    {
        AudioUtil.Play(ClickedSoundEffect, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
    }
}
