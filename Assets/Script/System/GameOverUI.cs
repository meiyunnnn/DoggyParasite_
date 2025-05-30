using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;  // UI Panel ที่จะโชว์เมื่อ Player ตาย
    public Button restartButton;      // UI Button สำหรับ Restart เกม

    void Start()
    {
        gameOverPanel.SetActive(false); // ซ่อน UI Panel ตอนเริ่มต้น
        restartButton.onClick.AddListener(RestartGame); // ตั้งค่าให้ปุ่ม restart ทำการโหลด Scene ใหม่
    }

    public void ShowGameOverUI()
    {
        gameOverPanel.SetActive(true);  // แสดง UI Panel
    }

    void RestartGame()
    {
        Time.timeScale = 1;
        // โหลด Scene ใหม่ (ตรวจสอบให้แน่ใจว่า Scene ที่จะโหลดมีชื่อถูกต้อง)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
