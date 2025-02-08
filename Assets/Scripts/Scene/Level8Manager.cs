using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8Manager : MonoBehaviour
{
    public static Level8Manager Instance;
    public List<CollectionController> Destinations = new List<CollectionController>();
    public static void EnsureInstance()
    {
        if (Instance == null)
        {
            GameObject go = new GameObject("Level8Manager");
            Instance = go.AddComponent<Level8Manager>();
        }
    }

    public void RigisterDestination(CollectionController destination)
    {
        Destinations.Add(destination);
        Destinations.Sort((a, b) => a.Type.CompareTo(b.Type));
        SetAllDestinationUnActive();
        SetNextDestinationActive();
    }
    public void GetDestination(CollectionController destination)
    {
        Destinations.Remove(destination);
        if (Destinations.Count == 0)
        {
            LevelSceneManager.Instance.OnPlayerWin();
        }
        else
        {
            SetNextDestinationActive();
        }
    }

    public void SetNextDestinationActive()
    {
        Debug.Log("SetNextDestinationActive");
        if (Destinations.Count > 0)
        {
            Destinations[0].SetSelected();
        }
    }
    public void SetAllDestinationUnActive()
    {
        foreach (var item in Destinations)
        {
            item.SetUnSelected();
        }
    }
}
