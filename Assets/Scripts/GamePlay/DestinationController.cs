using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DestinationController : MonoBehaviour
{
    public float StayTime = 2f;
    private float StayTimeCounter = 2f;
    private bool IsPlayerOn = false;
    private bool isGoingToNextScene = false;
    public Vector3 PositionFixer = new Vector3(0, 0, 0);
    public TextMeshProUGUI WinText;

    void Start()
    {
        StayTimeCounter = StayTime;
        WinText.gameObject.SetActive(false);


    }
    void Update()
    {
        if (IsPlayerOn)
        {
            WinText.gameObject.SetActive(true);
            WinText.text = StayTimeCounter.ToString("F1");
        }
        else
        {
            WinText.gameObject.SetActive(false);
        }
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
                WinText.transform.position = Camera.main.WorldToScreenPoint(transform.position + PositionFixer);
                StayTimeCounter -= Time.fixedDeltaTime;
            }
            else
            {
                StayTimeCounter = StayTime;
            }
        }
        else
        {
            if (!isGoingToNextScene)
            {
                isGoingToNextScene = true;
                OnGameWin();
            }
        }
    }

    public void OnGameWin()
    {

        LevelSceneManager.Instance.PlayerArrive();
        Destroy(this.gameObject);
    }
}
