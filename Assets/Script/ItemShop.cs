using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEditor.Progress;

public class ItemShop : MonoBehaviour
{
    public ItemData[] items;
    public GameObject[] buyButtons;

    [Header("Audio")]
    public AudioClip successClip;
    public AudioClip failClip;
    private AudioSource audioSource;

    private bool[] isItemBought; // ถ้าซื้อซ้ำไม่ได้ ก็เก็บสถานะ

    private void Start()
    {
        isItemBought = new bool[items.Length];
        audioSource = GetComponent<AudioSource>();
    }

    public void BuyItem(int index)
    {
        if (index < 0 || index >= items.Length) return;
        ItemData item = items[index];

        if (!items[index].canBuyMultipleTimes && isItemBought[index])
        {
            Debug.Log($"ซื้อ {items[index].itemName} ไปแล้ว ซื้อซ้ำไม่ได้");
            PlaySound(failClip);
            return;
        }

        if (!INVManagerFix.Instance.HasFreeSlot())
        {
            Debug.Log("❌ ช่องเก็บของเต็ม ซื้อไม่ได้");
            PlaySound(failClip);
            return;
        }

        int price = items[index].price;
        int currentMoney = CoinManager.Instance.Coin;

        if (currentMoney >= price)
        {
            CoinManager.Instance.SpendMoney(price);
            bool added = INVManagerFix.Instance.AddItem(items[index]);
            if (added)
            { if (!item.canBuyMultipleTimes)
                {
                    isItemBought[index] = true;
                }
            
                Debug.Log($"ซื้อ {items[index].itemName} สำเร็จ");
                PlaySound(successClip);
            }
            else
            {
                Debug.Log("ช่องเก็บของเต็ม");
                PlaySound(failClip);
            }
        }
        else
        {
            Debug.Log("เงินไม่พอ");
            PlaySound(failClip);
        }

    }
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }

}
