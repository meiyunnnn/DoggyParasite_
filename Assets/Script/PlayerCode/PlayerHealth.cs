using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;

public class PlayerHealth : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public int maxHealth = 50;
    public int currentHealth;
    public Transform respawnPoint;
    public GameOverUI gameOverUI;
    public HealthBarBehavior HealthBar;
    private bool isDead = false;
    public AudioSource audioSource;
    public AudioClip respawnClip;
    public GameObject AdsBeforeRespawn;
    public GameObject RespawnButton;
    public GameObject deadUIPanel;

    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOSAdUnitId = "Interstitial_iOS";
    string _adUnitId;

    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
        ? _iOSAdUnitId
        : _androidAdUnitId;
    }

    void Start()
    {
       
        LoadAd();
 
        currentHealth = maxHealth;
        if (HealthBar != null)
        {
            HealthBar.SetHealth(currentHealth, maxHealth);
        }
    }


    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        if (HealthBar != null)
        {
            HealthBar.SetHealth(currentHealth, maxHealth);
        }

        Debug.Log($"🔥 Player โดนโจมตี! HP: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        if (HealthBar != null)
        {
            HealthBar.SetHealth(currentHealth, maxHealth);
        }

        Debug.Log("ได้รับการฮีล: " + amount + " | HP ตอนนี้: " + currentHealth);
    }

    void Die()
    {
        isDead = true;
        Debug.Log("💀 Player ตายแล้ว!");

        if (gameOverUI != null)
        {
            gameOverUI.ShowGameOverUI();
            AdsBeforeRespawn.SetActive(true);
            RespawnButton.SetActive(false);
        }

        Time.timeScale = 0;
    }


    public void WatchAdsAfterSpawn()
    {
        ShowAd();
        if (AdsBeforeRespawn != null)
        {
            AdsBeforeRespawn.SetActive(false);
            RespawnButton.SetActive(true);
        }

    }
    public void Respawn()
    {

         
       
        if (deadUIPanel != null)
        {
            deadUIPanel.SetActive(false);
        }

        if (audioSource != null && respawnClip != null)
        {
            audioSource.PlayOneShot(respawnClip);
        }

        transform.position = respawnPoint.position;
        currentHealth = maxHealth;
        isDead = false;

        if (HealthBar != null)
        {
            HealthBar.SetHealth(currentHealth, maxHealth);
        }

        Time.timeScale = 1;
        Debug.Log("🔁 Player Respawn แล้ว!");
    }

    //Unity Ads

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        Time.timeScale = 0;
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string _adUnitId) { }
    public void OnUnityAdsShowClick(string _adUnitId) { }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) { }
}