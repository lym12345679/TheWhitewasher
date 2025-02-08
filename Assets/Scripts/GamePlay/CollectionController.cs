using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectionController : MonoBehaviour
{
    public float StayTime = 2f;
    private float StayTimeCounter = 2f;
    private bool IsPlayerOn = false;
    private bool isTrigered = false;
    public Vector3 PositionFixer = new Vector3(0, 0, 0);
    public TextMeshProUGUI WinText;
    public CollectionType Type;
    public SpriteRenderer StarRenderer;
    public SpriteRenderer CollectionRenderer;
    public CGShownItemEnum Collection;
    private bool isSelected = false;
    void Start()
    {
        StayTimeCounter = StayTime;
        WinText.gameObject.SetActive(false);

        Level8Manager.EnsureInstance();
        Level8Manager.Instance.RigisterDestination(this);

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
    void OnValidate()
    {
        if (Collection != CGShownItemEnum.None)
        {
            CollectionRenderer.sprite = SOManager.cgShownItemSO.GetPixelSprite(Collection);
        }
        else
        {
            CollectionRenderer.sprite = null;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && isSelected)
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
        if (isSelected)
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
                if (!isTrigered)
                {
                    isTrigered = true;
                    OnCollected();
                }
            }
        }

    }

    public void OnCollected()
    {
        Level8Manager.Instance.GetDestination(this);
        SetCollected();
    }
    public void SetUnSelected()
    {
        isSelected = false;
        StarRenderer.gameObject.SetActive(false);
        CollectionRenderer.gameObject.SetActive(false);
    }
    public void SetSelected()
    {
        isSelected = true;
        StarRenderer.gameObject.SetActive(true);
        CollectionRenderer.gameObject.SetActive(false);
    }
    public void SetCollected()
    {
        isSelected = false;
        IsPlayerOn = false;
        StarRenderer.gameObject.SetActive(false);
        CollectionRenderer.gameObject.SetActive(true);
        if (Collection != CGShownItemEnum.None)
        {
            CollectionRenderer.sprite = SOManager.cgShownItemSO.GetPixelSprite(Collection);
        }
    }
}
public enum CollectionType
{
    FirstCollection,
    SecondCollection,
    ThirdCollection,
    FourthCollection,
    FifthCollection,
    SixthCollection,
    SeventhCollection,
    EighthCollection,
    Destination,
}
