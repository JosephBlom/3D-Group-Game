using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopLogic : MonoBehaviour
{
    [Header("UI")]
    public List<Slot> shopSlots = new List<Slot>();

    private PlayerManager playerManager;

    private void Start()
    {
        Cursor.visible = true;

        //foreach (Slot uiSlots in shopSlots)
        //{
        //    uiSlots.initialiseSlot();
        //}

        playerManager = GetComponentInChildren<PlayerManager>();
    }
}