using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDrop : MonoBehaviour
{
    public int coinValue = 75;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ตรวจสอบว่า Player ชนเหรียญหรือไม่
        {
            CoinManager CoinManager = FindObjectOfType<CoinManager>(); // หา CoinManager ใน Scene
            if (CoinManager != null)
            {
                CoinManager.AddScore(coinValue); // เพิ่มจำนวนเหรียญ
            }
            Destroy(gameObject); // ทำลายเหรียญเมื่อเก็บแล้ว
        }
    }
}
