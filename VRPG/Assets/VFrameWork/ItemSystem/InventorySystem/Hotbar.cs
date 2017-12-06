using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Linq;

public class Hotbar : MonoBehaviour {
    [SerializeField]
    public KeyCode[] keyCodesForSlots = new KeyCode[20];
    [SerializeField]
    public int slotsInTotal;

#if UNITY_EDITOR
    [MenuItem("Master System/Create/Hotbar")]
    public static void menuItemCreateInventory() {
        
        if (GameObject.FindGameObjectWithTag("Canvas") == null)
        {
            //1. Empty Object to hold on All Canvas and Inventories
            GameObject canvasObj = new GameObject();
            canvasObj.name = "CanvasObject";

            //2. Main Canvas
            GameObject Canvas = (GameObject)Instantiate(Resources.Load("Prefabs/Canvas_Main") as GameObject);
            Canvas.transform.SetParent(canvasObj.transform, true);
            GameObject eventObj = (GameObject)Instantiate(Resources.Load("Prefabs/EventSystem") as GameObject);
            eventObj.transform.SetParent(canvasObj.transform, true);
            //3. Load a new panel
            GameObject panel = (GameObject)Instantiate(Resources.Load("Prefabs/Panel") as GameObject);
            panel.name = "Hotbar";
            panel.transform.SetParent(Canvas.transform, true);
            panel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

            GameObject draggingItem = (GameObject)Instantiate(Resources.Load("Prefabs/DraggingItem") as GameObject);
            draggingItem.transform.SetParent(Canvas.transform, true);

            InventoryUISetting setting = panel.AddComponent<InventoryUISetting>();
            panel.AddComponent<Hotbar>();
            panel.AddComponent<InventoryUIDesign>().uiType = InventoryUIDesign.UIType.Hotbar;
            setting.uiType = InventoryUISetting.UIType.Hotbar;
            setting.ResetUILayout();
        }
        else {
            //3. Load a new panel
            GameObject panel = (GameObject)Instantiate(Resources.Load("Prefabs/Panel") as GameObject);
            panel.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
            panel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            InventoryUISetting setting = panel.AddComponent<InventoryUISetting>();
            panel.AddComponent<Hotbar>();
            panel.AddComponent<InventoryUIDesign>().uiType = InventoryUIDesign.UIType.Hotbar;
            setting.uiType = InventoryUISetting.UIType.Hotbar;
            setting.ResetUILayout();
        }
    }
#endif

    private void Update()
    {
        for (int i = 0; i < slotsInTotal; i++) {
            if (Input.GetKeyDown(keyCodesForSlots[i])) {
                if (transform.GetChild(1).GetChild(i).childCount != 0 && transform.GetChild(1).GetChild(0).GetComponent<ItemHandleOnGUI>().item.itemType != ItemType.UFPS_Ammo) {
                    //........
                }
            }
        }
    }

    public int getSlotInTotal() {
        return 0;
    }

}
