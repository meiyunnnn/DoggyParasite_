using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class INVManagerFix : MonoBehaviour
 {
        public static INVManagerFix Instance;
        public ItemData[] quickSlots = new ItemData[3];
        public GameObject player;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public bool AddItem(ItemData item)
        {
            for (int i = 0; i < quickSlots.Length; i++)
            {
                if (quickSlots[i] == null)
                {
                    quickSlots[i] = item;
                    Debug.Log($"เก็บของ '{item.itemName}' ที่ Quick Slot {i + 1}");
                    return true;
                }
            }

            Debug.Log("Quick Slot เต็มแล้ว");
            return false;
        }

        public void UseItem(int slotIndex)
        {
            if (quickSlots[slotIndex] != null)
            {
            Debug.Log("ใช้ไอเทมในช่อง: " + slotIndex);
            Debug.Log("Player object: " + player.name);
            quickSlots[slotIndex].Use(player);
                quickSlots[slotIndex] = null;
            }
            else
            {
                Debug.Log($"ช่อง {slotIndex + 1} ว่างอยู่");
            }
        }
        public bool HasFreeSlot()
        {
        foreach (var slot in quickSlots)
        {
            if (slot == null)
                return true;
        }
        return false;
        }
}