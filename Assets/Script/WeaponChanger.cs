using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    public Animator playerAnimator;
    public AnimatorOverrideController[] weaponOverrides;
    public WeaponData[] weaponDataList;
    public WeaponData currentWeapon;
    public int currentWeaponIndex = 0;
    private void Start()
    {
            currentWeaponIndex = 0;
        EquipWeapon(currentWeaponIndex);
    }
public void EquipWeapon(int index)
    {
        if (index >= 0 && index < weaponOverrides.Length)
        {
            playerAnimator.runtimeAnimatorController = weaponOverrides[index];
            Debug.Log("เปลี่ยนชุด Animation อาวุธเรียบร้อย: " + weaponOverrides[index].name);
            currentWeaponIndex = index;

            if (index < weaponDataList.Length)
            {
                currentWeapon = weaponDataList[index]; // ✅ ตั้งค่า currentWeapon
                Debug.Log($"🪓 ติดตั้งอาวุธ: {currentWeapon.weaponName}");
            }
            else
            {
                Debug.LogWarning("❌ weaponDataList ไม่มีข้อมูลตรงกับ index นี้");
            }
        }
    }
    public float GetAttackRange() => currentWeapon?.attackRange ?? 1f;
    public float GetAttackCooldown() => currentWeapon?.attackCooldown ?? 1f;
    public float GetAttackDelay() => currentWeapon?.attackDelay ?? 0f;
    public int GetCurrentWeaponDamage() => currentWeapon?.damage ?? 1;
}
