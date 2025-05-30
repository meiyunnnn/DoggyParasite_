using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public AudioClip lobbyMusic;
    public AudioClip InGameScene;

    private AudioSource audioSource;
    private string currentScene;

    [Range(0f, 1f)] 
    public float volume = 1f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        PlayMusicForScene(currentScene);
        SceneManager.sceneLoaded += OnSceneLoaded;
        ApplyVolume();
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != currentScene)
        {
            currentScene = scene.name;
            PlayMusicForScene(currentScene);
        }
    }

    void PlayMusicForScene(string sceneName)
    {
        AudioClip clipToPlay = null;

        if (sceneName == "StartMenu") // เปลี่ยนเป็นชื่อฉากล็อบบี้ของคุณ
        {
            clipToPlay = lobbyMusic;
        }
        else if (sceneName == "InGameScene") // เปลี่ยนเป็นชื่อฉากเล่นเกม
        {
            clipToPlay = InGameScene;
        }

        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.loop = true;
            audioSource.Play();
        }
        
        
    }
    void ApplyVolume()
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;

        }
    }
    // เรียกจาก UI Slider ได้
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume); // ป้องกันค่าเกินช่วง
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}

