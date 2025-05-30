using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Shop/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public bool canBuyMultipleTimes = false; // ซื้อซ้ำได้ไหม
    public string weaponName;
    public int price;
    public int weaponIndex; // สำหรับใช้กับ WeaponChanger

    public int damage;
    public float attackRange = 2f;     // ระยะโจมตี
    public float attackCooldown = 1f;  // เวลาระหว่างโจมตีแต่ละครั้ง
    public float attackDelay = 0.2f;   // เวลาดีเลย์ก่อนดาเมจออก (ใช้กับ Animation)
}
