using UnityEngine;
public class PropsUI : MonoBehaviour
{
    [HideInInspector]
    public static PropsUI Instance;
    public RectTransform Content;
    public static float ContentCellWidth = 150;
    private int maxShownItemOnConotent = 4;
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        UpdateContentWidth(Content.childCount);
        PreLoadProp();
        CheckContentItemCount();
    }
    public void UpdateContentWidth(int currentChildCount)
    {
        if (currentChildCount < maxShownItemOnConotent)
        {
            Content.sizeDelta = new Vector2(ContentCellWidth * maxShownItemOnConotent, Content.sizeDelta.y);
        }
        else
        { Content.sizeDelta = new Vector2(ContentCellWidth * currentChildCount, Content.sizeDelta.y); }
        //Debug.Log("ContentWidth:" + Content.sizeDelta.x);
    }

    public void PreLoadProp()
    {
        GameObject propUI1 = Instantiate(SOManager.porpSO.GetPropUI(PorpEnum.PaintBrushWasher));
        GameObject propUI2 = Instantiate(SOManager.porpSO.GetPropUI(PorpEnum.Stainer));
        Destroy(propUI1);
        Destroy(propUI2);
    }
    public void AddProp(PropClass propClass)
    {
        GameObject propUI = Instantiate(SOManager.porpSO.GetPropUI(propClass.Porp), Content);
        propUI.GetComponent<PropUIController>().SetColorMod(propClass.ColorMod);
        propUI.transform.SetAsFirstSibling();
        int childCount = Content.childCount;
        if (RemoveBlankItem())
        {
            UpdateContentWidth(childCount - 1);
        }
        else
        {
            UpdateContentWidth(childCount);
        }
    }
    public void AddBlank()
    {
        GameObject gameObject = Instantiate(SOManager.porpSO.GetPropUI(PorpEnum.Blank), Content);
        UpdateContentWidth(Content.childCount);
    }
    public void CheckContentItemCount()
    {
        while (Content.childCount < maxShownItemOnConotent)
        {
            AddBlank();
            //Debug.Log("childCount:" + Content.childCount);
        }
    }

    public bool RemoveBlankItem()
    {
        RectTransform[] children = Content.GetComponentsInChildren<RectTransform>();

        foreach (RectTransform child in children)
        {

            //Debug.Log(child.name);
            PropUIController propUIController;
            if (child.gameObject.TryGetComponent<PropUIController>(out propUIController))
            {
                if (propUIController.Prop == PorpEnum.Blank)
                {
                    Destroy(child.gameObject);
                    return true;
                }
            }
        }
        return false;
    }

    public void OnPropDeserve()
    {
        if (Content.childCount - 1 < maxShownItemOnConotent)
        {
            GameObject gameObject = Instantiate(SOManager.porpSO.GetPropUI(PorpEnum.Blank), Content);
            UpdateContentWidth(Content.childCount - 1);
        }
    }
    public void ClearProp()
    {
        RectTransform[] children = Content.GetComponentsInChildren<RectTransform>();
        foreach (var child in children)
        {
            if (child != Content)
            {
                Destroy(child.gameObject);
            }
        }
        UpdateContentWidth(maxShownItemOnConotent);
    }
}
