using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttributeList : ScriptableObject {

    [SerializeField]
    public List<ItemAttribute> itemAttributeList = new List<ItemAttribute>();
}
