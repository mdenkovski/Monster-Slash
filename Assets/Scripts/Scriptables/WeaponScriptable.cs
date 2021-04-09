using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 1)]
public class WeaponScriptable : ItemScriptable
{
    public float AttackPower = 0;

    public override void EquipItem(PlayerController controller)
    {
        Debug.Log("Add equip functionality");

        if (Equipped)
        {
            controller.Stats.UnequipWeapon();
        }
        else
        {
            controller.Stats.EquipWeapon(this);
            controller.Inventory.DeactivateAllWeaponsExceptEquipped(this);
        }
        Equipped = !Equipped;
    }

    
}
