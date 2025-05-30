using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public ItemData bandageItem;

    public void SpawnBandage()
    {
        bool added = INVManagerFix.Instance.AddItem(bandageItem);
        if (added)
            Debug.Log("เสก Bandage เข้า Quick Slot แล้ว");
        else
            Debug.Log("Quick Slot เต็ม");
    }
}
