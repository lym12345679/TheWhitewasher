using UnityEngine;

public class LevelMessageItem : MonoBehaviour
{
    public GameObject LockedImg;
    public GameObject UnlockedImg;
    public void SetLocked()
    {
        LockedImg.SetActive(true);
        UnlockedImg.SetActive(false);
    }
    public void SetUnlocked()
    {
        LockedImg.SetActive(false);
        UnlockedImg.SetActive(true);
    }
}
