using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f; // เวลาที่ศัตรูเกิดแต่ละครั้ง
    public float spawnDistance = 2f; // ระยะที่ศัตรูเกิดจากขอบจอด้านขวา

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Debug.Log($"📌 Enemy Spawn ที่ตำแหน่ง: {spawnPosition}"); // Debug เช็คว่าศัตรูเกิดถูกที่ไหม
    }

    Vector3 GetSpawnPosition()
    {
        float camWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float spawnX = mainCamera.transform.position.x + camWidth + spawnDistance;
        float spawnY = -2.098f; // ล็อกตำแหน่ง Y

        return new Vector3(spawnX, spawnY, 0);
    }
}