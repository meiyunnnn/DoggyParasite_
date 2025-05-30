using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public int maxHealth = 20;
    public int currentHealth;
    public event Action onEnemyDeath;
    public EenemyHealthBarBehavior HealthBar;
    public GameObject coinPrefab;
    public AudioSource FoxHit;
    public float dropChance = 0.8f;
    void Start()
    {

        currentHealth = maxHealth;
        HealthBar.SetHealth(currentHealth, maxHealth);

    }

    public void TakeDamage(int damage)
    {
        if (FoxHit != null) 
        {
            FoxHit.Play();
        }
        
        currentHealth -= damage;
        HealthBar.SetHealth(currentHealth, maxHealth);


        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth; // รีเซ็ทค่าเลือดของศัตรู
        HealthBar.SetHealth(currentHealth, maxHealth);

    }
    void Die()
    {
        onEnemyDeath?.Invoke();
        Debug.Log("Enemy died!");
        float chance = UnityEngine.Random.value; // ใช้ UnityEngine.Random เพื่อความชัวร์
        Debug.Log("🎲 ค่า Random ที่ได้: " + chance);

        if (chance < dropChance)
        {
            if (coinPrefab != null)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
                Debug.Log("💰 เหรียญถูกดรอป!");
            }
            else
            {
                Debug.LogError("❌ coinPrefab ไม่ถูกกำหนดใน Inspector!");
            }
        }
        Destroy(gameObject);
    }

}