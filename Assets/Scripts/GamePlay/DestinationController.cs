using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationController : MonoBehaviour
{
    public static float StayTime = 3f;
    public static float StayTimeCounter = 3f;
    private bool IsPlayerOn = false;
    void Start()
    {
        StayTimeCounter = StayTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Destination Reached");
            IsPlayerOn = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            IsPlayerOn = false;
        }
    }
    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        if (StayTimeCounter > 0)
        {
            if (IsPlayerOn)
            {
                StayTimeCounter -= Time.fixedDeltaTime;
            }
            else
            {
                StayTimeCounter = StayTime;
            }
        }
        else
        {
            OnGameWin();
        }
    }

    public void OnGameWin()
    {
        Debug.Log("Game Win");
    }
}
