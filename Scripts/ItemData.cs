using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public List<PocketItem> itemList = new List<PocketItem>();
}
