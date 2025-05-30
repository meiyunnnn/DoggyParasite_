using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIbutton : MonoBehaviour
{
    public GameObject panel; // UI ที่จะเปิด/ปิด
    private bool isOpen = false;


    public void Toggle()
    {
        isOpen = !isOpen; // สลับสถานะ
        panel.SetActive(isOpen);
    }
}
