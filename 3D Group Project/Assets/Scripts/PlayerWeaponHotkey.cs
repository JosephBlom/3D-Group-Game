using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHotkey : MonoBehaviour
{
    [SerializeField] private GameObject automatic;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject rocketLauncher;

    private GameObject equipped;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UnequipWeapon(shotgun);
            UnequipWeapon(rocketLauncher);

            EquipWeapon(automatic);
            equipped = automatic;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UnequipWeapon(automatic);
            UnequipWeapon(rocketLauncher);

            EquipWeapon(shotgun);
            equipped = shotgun;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UnequipWeapon(automatic);
            UnequipWeapon(shotgun);

            EquipWeapon(rocketLauncher);
            equipped = rocketLauncher;
        }
    }

    private void UnequipWeapon(GameObject equipped)
    {
        equipped.SetActive(false);
    }
    private void EquipWeapon(GameObject weapontoEquip)
    {
        weapontoEquip.SetActive(true);
    }


}
