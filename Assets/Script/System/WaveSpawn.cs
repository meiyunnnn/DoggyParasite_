using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ศัตรูที่ต้องการ Spawn
    public Transform spawnPoint; // จุดเกิดของศัตรู (ล็อก Y)
    public TextMeshProUGUI waveText; // UI แสดง Wave
    public TextMeshProUGUI enemyCountText; // UI แสดงจำนวนศัตรูที่เหลือ

    public int totalWaves = 3; // จำนวน Wave ทั้งหมด
    private int currentWave = 0; // Wave ปัจจุบัน
    private int enemiesRemaining; // ศัตรูที่เหลือใน Wave ปัจจุบัน

    public int[] enemiesPerWave = { 3, 5, 7 }; // จำนวนศัตรูในแต่ละ Wave
    public float spawnInterval = 2f; // ระยะเวลาระหว่างการ Spawn แต่ละตัว

    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        if (currentWave >= totalWaves)
        {
            Debug.Log("🎉 ทุก Wave จบแล้ว!");
            yield break; // หยุดการทำงานถ้าครบทุก Wave
        }

        // เริ่ม Wave ใหม่
        currentWave++;
        enemiesRemaining = enemiesPerWave[currentWave - 1];

        UpdateUI();
        Debug.Log("🚀 เริ่ม Wave: " + currentWave);

        for (int i = 0; i < enemiesPerWave[currentWave - 1]; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // รอระหว่างการ Spawn แต่ละตัว
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = new Vector3(Camera.main.transform.position.x + 10f, -2.598f, 0f); // Spawn ศัตรูด้านขวา
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<EnemyHealth>().onEnemyDeath += EnemyDefeated;
    }

    void EnemyDefeated()
    {
        enemiesRemaining--;
        UpdateUI();

        if (enemiesRemaining <= 0)
        {
            Debug.Log("🎉 Wave " + currentWave + " เคลียร์!");
            StartCoroutine(StartNextWave());
        }
    }

    void UpdateUI()
    {
        waveText.text = "Wave: " + currentWave + " / " + totalWaves;
        enemyCountText.text = enemiesRemaining + " Remaining";
    }
}
