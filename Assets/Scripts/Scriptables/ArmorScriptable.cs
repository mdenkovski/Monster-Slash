using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor", order = 2)]
public class ArmorScriptable : ItemScriptable
{
    public float DefenceBonus = 0;

    public override void EquipItem(PlayerController controller)
    {
        Debug.Log("Add equip functionality");

        if (Equipped)
        {
            controller.Stats.UnequipArmor();
        }
        else
        {
            controller.Stats.EquipArmor(this);
            controller.Inventory.DeactivateAllArmorExceptEquipped(this);
        }
        Equipped = !Equipped;
    }
}
