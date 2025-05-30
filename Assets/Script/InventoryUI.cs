using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image[] slotIcons; // ช่องสำหรับโชว์ sprite ของแต่ละ slot
    private INVManagerFix inv;

    private void Start()
    {
        inv = INVManagerFix.Instance;
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI(); // อัปเดตตลอดเวลา หรือจะเปลี่ยนเป็นเรียกเมื่อมีการเปลี่ยนก็ได้
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slotIcons.Length; i++)
        {
            if (inv.quickSlots[i] != null)
            {
                slotIcons[i].sprite = inv.quickSlots[i].icon;
                slotIcons[i].color = Color.white;
            }
            else
            {
                slotIcons[i].sprite = null;
                slotIcons[i].color = new Color(1, 1, 1, 0);
            }
        }
    }
}
