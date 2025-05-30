using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Shop/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int healAmount;
    public int price;
    public bool canBuyMultipleTimes;

    public void Use(GameObject player)
    {
        PlayerHealth hp = player.GetComponent<PlayerHealth>();
        if (hp != null)
        {
            hp.Heal(healAmount);
            Debug.Log($"ใช้ {itemName} ฟื้น {healAmount} HP");
        }

        else
        {
        Debug.LogWarning("ไม่พบ PlayerHealth บน player object");
    } 
  }
}
