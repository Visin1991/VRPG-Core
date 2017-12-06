using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace V
{
    public class LEInventory : EditorTimeBuildInSerializableData
    {

        public List<Item> items = new List<Item>();

        InventoryPanel inventoryPanel;

        //protected override void Start()
        //{
        //    base.Start();

        //    ItemHandleOnObj[] handles = transform.GetComponentsInChildren<ItemHandleOnObj>();
        //    foreach (ItemHandleOnObj handle in handles)
        //    {
        //        Item item = handle.GetItem();
        //        items.Add(item);
        //        GameUIPr.Instance.AddItemToInventory(item);
        //    }
        //}

        public void AddItem(Item item)
        {
            items.Add(item);
            AddToGUI(item);
        }

        public void AddToGUI(Item item)
        {
            GameUIPr.Instance.AddItemToInventory(item);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public override void SetUpDataType()
        {
            type = typeof(LEInventory).FullName;
        }

        protected override void DeSerializeDataInternal(BinaryReader reader)
        {

            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {

                int id = reader.ReadInt32();
                Item item = GameDataManager.Instance.itemDatabase.getItemCopyByID(id);

                GameObject obj = Instantiate(item.itemPrefab);
                ItemHandleOnObj handle = obj.GetComponentInChildren<ItemHandleOnObj>();
                handle.ReloadItemInfoFromSave(item, this);

                Debug.Log(reader.ReadString());
            }

        }

        protected override void SerializeDataInternal(BinaryWriter writer)
        {
            int count = items.Count;
            writer.Write(count);

            foreach (Item item in items)
            {
                writer.Write(item.itemID);
                writer.Write(item.itemName);
            }
        }
    }
}
