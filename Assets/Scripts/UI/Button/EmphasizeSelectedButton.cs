
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmphasizeSelectedButton : MonoBehaviour
{
    public RectTransform LeftCursor;
    public RectTransform RightCursor;
    public Image ButtonText;
    public ColorEnum SelectedColor;
    private Color orignColor;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        LeftCursor.GetComponent<Image>().color = SOManager.colorSO.GetColor(SelectedColor);
        RightCursor.GetComponent<Image>().color = SOManager.colorSO.GetColor(SelectedColor);
        orignColor = ButtonText.color;
        SetButtonEvent();
    }
    public void SetButtonEvent()
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((eventData) => { OnButtonHighlighted((PointerEventData)eventData); });
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((eventData) => { OnButtonUnhighlighted((PointerEventData)eventData); });
        trigger.triggers.Add(entryExit);
    }
    public void OnButtonHighlighted(PointerEventData eventData)
    {
        ButtonText.color = SOManager.colorSO.GetColor(SelectedColor);
        LeftCursor.gameObject.SetActive(true);
        RightCursor.gameObject.SetActive(true);
    }
    public void OnButtonUnhighlighted(PointerEventData eventData)
    {
        ButtonText.color = orignColor;
        LeftCursor.gameObject.SetActive(false);
        RightCursor.gameObject.SetActive(false);
    }
}
