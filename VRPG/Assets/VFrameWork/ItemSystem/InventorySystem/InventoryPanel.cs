using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryPanel : MonoBehaviour {

    ItemHandleOnGUI[] allHandles;
    //TargetLE player;
    

    // Use this for initialization
    private void OnEnable()
    {
        allHandles = GetComponentsInChildren<ItemHandleOnGUI>();
        //player = FindObjectOfType<TargetLE>();
    }

    private void Update()
    {
      
    }

    public void ReSetItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            allHandles[i].ResetItem(items[i]);
        }
    }

    public void AddItem(Item item)
    {
        allHandles.First(h => h.IsEmpty).ResetItem(item); 
    }
}
