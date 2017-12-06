using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ItemInfo : MonoBehaviour {
    public Item item;

    public Image itemInfoBackground;
    public Text itemInfoNameText;
    public Text itemInfoDesText;

    [SerializeField]
    private GameObject tooltip;
    [SerializeField]
    private RectTransform tooltipRectTransform;
    [SerializeField]
    private GameObject itemInfoTextNameObj;
    [SerializeField]
    private GameObject itemInfoTextDescObj;
    [SerializeField]
    private GameObject itemInfoImageIconObj;

    public int itemInfoHeight;

    public RectTransform TooltipRectTransform
    {
        get
        {
            return tooltipRectTransform;
        }

        set
        {
            tooltipRectTransform = value;
        }
    }


    // Use this for initialization
    void Start () {
        DeactiveItemInfo();
	}
    public void LoadVariables() {
        tooltipRectTransform = this.GetComponent<RectTransform>();

        itemInfoBackground = transform.GetChild(0).GetComponent<Image>();

        itemInfoImageIconObj = this.transform.GetChild(1).gameObject;
        itemInfoImageIconObj.SetActive(false);

        itemInfoTextNameObj = this.transform.GetChild(2).gameObject;
        itemInfoNameText = this.transform.GetChild(2).gameObject.GetComponent<Text>();
        itemInfoTextNameObj.SetActive(false);

        itemInfoTextDescObj = this.transform.GetChild(3).gameObject;
        itemInfoDesText = this.transform.GetChild(3).gameObject.GetComponent<Text>();
        itemInfoTextDescObj.SetActive(false);

        itemInfoHeight = 250;
    }
    public void DeactiveItemInfo() {
        itemInfoTextNameObj.SetActive(false);
        itemInfoImageIconObj.SetActive(false);
        itemInfoTextDescObj.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void ActivateItemInfo() {
        itemInfoTextNameObj.SetActive(true);
        itemInfoImageIconObj.SetActive(true);
        itemInfoTextDescObj.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).GetComponent<Image>().sprite = item.itemIcon;
        transform.GetChild(2).GetComponent<Text>().text = item.itemName;
        transform.GetChild(3).GetComponent<Text>().text = item.itemDesc;
    }
#if UNITY_EDITOR
    [MenuItem("Master System/Create/ItemInfo")]
    public static void menuItemCreateItemInfo() {
        if (GameObject.FindGameObjectWithTag("ItemInfo") == null) {
            GameObject toolTip = Instantiate(Resources.Load("Prefabs/ItemInfo") as GameObject);
            toolTip.GetComponent<RectTransform>().localPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            toolTip.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
            toolTip.AddComponent<ItemInfo>().LoadVariables();
        }
    }
#endif
}
