using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    public WeaponChanger weaponChanger; // อ้างอิงถึง Player
    public WeaponData[] weapons;
    private bool[] isWeaponBought;

    [Header("Audio")]
    public AudioClip successClip;
    public AudioClip failClip;
    private AudioSource audioSource;

    //UIbutton
    public GameObject[] buyButtons;
    private void Start()
    {
        // สร้าง array สำหรับเก็บสถานะการซื้อ
        isWeaponBought = new bool[weapons.Length];
        audioSource = GetComponent<AudioSource>();

        // ถ้าซื้อไปแล้วก็ปิดปุ่ม
        for (int i = 0; i < weapons.Length; i++)
        {
            if (isWeaponBought[i] && buyButtons[i] != null)
            {
                buyButtons[i].SetActive(false);
            }
        }
    }
    public void BuyWeapon(int index)
    {
        // ตรวจสอบว่า index ถูกต้อง
        if (index < 0 || index >= weapons.Length)
        {
            Debug.LogWarning("❌ Index weapon ไม่ถูกต้อง");
            return;
        }
        WeaponData weapon = weapons[index];
        int currentMoney = CoinManager.Instance.Coin;
        int price = weapon.price;


        if (!weapon.canBuyMultipleTimes && isWeaponBought[index])
        {
            Debug.Log($"⚠️ อาวุธ {weapons[index].weaponName} ซื้อไปแล้ว");
            return;
        }
        

        Debug.Log($"💰 กำลังซื้ออาวุธ '{weapon.weaponName}' ราคา {price} | เงินปัจจุบัน: {currentMoney}");

        if (currentMoney >= price)
        {
            // จ่ายเงินก่อน
            bool spent = CoinManager.Instance.SpendMoney(price);
            if (!spent)
            {
                Debug.Log($"❌ เงินไม่พอจริง ๆ");
                PlaySound(failClip);
                return;
            }
            if (weaponChanger != null)
            {
                weaponChanger.EquipWeapon(weapon.weaponIndex);

                // บันทึกสถานะเฉพาะถ้าไม่ให้ซื้อซ้ำ
                if (!weapon.canBuyMultipleTimes)
                {
                    isWeaponBought[index] = true;
                    if (buyButtons != null && index < buyButtons.Length && buyButtons[index] != null)
                    {
                        buyButtons[index].SetActive(false);
                    }
                }

                Debug.Log($"✅ ซื้ออาวุธ {weapon.weaponName} สำเร็จ");
                PlaySound(successClip);

            }
            else
            {
                Debug.LogWarning("❌ weaponChanger ยังไม่ได้ตั้งค่าใน Inspector");
                PlaySound(failClip);

            }
        }
        else
        {
            Debug.Log($"❌ เงินไม่พอ! ต้องการ {price} แต่มี {currentMoney}");
            PlaySound(failClip);
        }
        
    }
    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }

}
