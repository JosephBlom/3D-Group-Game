using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAmmoManager : MonoBehaviour
{
    public int regularAmmoCount = 0;
    public int energyAmmoCount = 0;
    public int specialAmmoCount = 0;

    public int FindAmmoType(int ammoType)
    {
        switch (ammoType)
        {
            case 0:
                return regularAmmoCount;
            case 1:
                return energyAmmoCount;
            case 2:
                return specialAmmoCount;
            default:
                Debug.Log("Not a valid ammo type, defaulting to regular ammo");
                return regularAmmoCount;
        }
    }

    public void AddAmmo(int ammoAdded, int ammoType)
    {
        switch(ammoType)
        {
            case 0:
                regularAmmoCount += ammoAdded;
                break;
            case 1:
                energyAmmoCount += ammoAdded;
                break;
            case 2:
                specialAmmoCount += ammoAdded;
                break;
            default:
                Debug.Log("Not a valid ammo type!");
                break;
        }
    }
    public void RemoveAmmo(int ammotoRemove, int ammoType)
    {
        switch (ammoType)
        {
            case 0:
                regularAmmoCount -= ammotoRemove;
                break;
            case 1:
                energyAmmoCount -= ammotoRemove;
                break;
            case 2:
                specialAmmoCount -= ammotoRemove;
                break;
            default:
                Debug.Log("Not a valid ammo type!");
                break;
        }
    }

}
