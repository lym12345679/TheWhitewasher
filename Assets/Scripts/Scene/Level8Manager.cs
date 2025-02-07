using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8Manager : MonoBehaviour
{
    public static Level8Manager Instance;
    public List<DestinationController> Destinations = new List<DestinationController>();
    public static void EnsureInstance()
    {
        if (Instance == null)
        {
            GameObject go = new GameObject("Level8Manager");
            Instance = go.AddComponent<Level8Manager>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RigisterDestination(DestinationController destination)
    {
        Destinations.Add(destination);
        Destinations.Sort((a, b) => a.Type.CompareTo(b.Type));
        SetAllDestinationUnActive();
        SetNextDestinationActive();
    }
    public void GetDestination(DestinationController destination)
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
        if (Destinations.Count > 0)
        {
            Destinations[0].gameObject.SetActive(true);
        }
    }
    public void SetAllDestinationUnActive()
    {
        foreach (var item in Destinations)
        {
            item.gameObject.SetActive(false);
        }
    }
}
