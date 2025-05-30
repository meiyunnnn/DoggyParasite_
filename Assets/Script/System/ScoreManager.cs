using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;


public class CoinManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOSAdUnitId = "Interstitial_iOS";
    string _adUnitId;

    public TextMeshProUGUI CoinText; // ตัวแสดงคะแนน UI
    public int Coin; // คะแนนเริ่มต้น
    public static CoinManager Instance;
    public GameObject Colloect;
    void Awake()
    {
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
        ? _iOSAdUnitId
        : _androidAdUnitId;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        LoadAd();
    }
    private void Update()
    {
        UpdateScoreUI();
    }
    public bool SpendMoney(int price)
    {
        if (Coin >= price)
        {
            Coin -= price;
            Debug.Log("💰 ใช้เงินไป " + price + " เหลือ: " + Coin);
            return true;
        }
        else
        {
            Debug.Log($"❌ ไม่พอ! มีแค่: {Coin}, ต้องการ: {price}");
            return false;
        }
    }
    // เพิ่มคะแนน
    public void AddScore(int value)
    {
        Coin += value;
        UpdateScoreUI();
    }
    public void AddMoneyCheat(int amount)
    {
        Coin += amount;
        Debug.Log($"💸 เสกเงิน {amount} | เหลือ: {Coin}");
    }
    public void PauseForads() 
    {
        Time.timeScale = 0;
        ShowAd();
        if (Colloect != null)
        {
            Colloect.SetActive(true);
        }
    }
    public void CollectAfterAds(int amount)
    {
        if (Colloect != null)
        {
            Colloect.SetActive(false);
        }
        Time.timeScale = 1;

        Coin += amount;

    }
    // อัปเดต UI
    void UpdateScoreUI()
    {
        CoinText.text = "     " + Coin;
    }
    //UnityAds

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
