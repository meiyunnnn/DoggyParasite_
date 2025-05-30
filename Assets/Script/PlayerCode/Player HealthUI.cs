using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText; // ที่จะเอาไปอ้างอิง UI Text
    public PlayerHealth playerHealth;  // อ้างอิง PlayerHealth.cs เพื่อดึงค่า HP

    void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        }
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth not found! Please make sure the Player has the PlayerHealth script.");
        }
    }

    void Update()
    {
        if (playerHealth != null)
        {
            // แสดงค่า HP บน UI
            healthText.text = $"HP: {playerHealth.currentHealth}/{playerHealth.maxHealth}";
        }
    }
}
