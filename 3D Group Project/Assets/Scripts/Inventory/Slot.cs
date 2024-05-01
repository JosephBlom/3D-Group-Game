using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool equipmentSlot;
    public bool hovered;
    public Item heldItem;
    public int slotQuantity;

    private Color opaque = new Color(1, 1, 1, 1);
    private Color transparent = new Color(1, 1, 1, 0);

    private Image thisSlotImage;

    public TMP_Text thisSlotQuantityText;

    public void initialiseSlot()
    {
        thisSlotImage = gameObject.GetComponent<Image>();
        thisSlotQuantityText = transform.GetChild(0).GetComponent<TMP_Text>();
        thisSlotImage.sprite = null;
        thisSlotImage.color = transparent;
        setItem(null, null);
    }

    public void setItem(Item item, Slot slot)
    {
        heldItem = item;

        if(item != null)
        {
            if(slot != null)
            {
                slotQuantity = slot.slotQuantity;
            }
            else
            {
                slotQuantity = 1;
            }
            thisSlotImage.sprite = heldItem.icon;
            thisSlotImage.color = opaque;
            updateData();
        }
        else
        { 
            thisSlotImage.sprite = null;
            thisSlotImage.color = transparent;
            updateData();
        }
    }

    public Item getItem()
    {
        return heldItem;
    }

    public void updateData()
    {
        if(heldItem != null)
        {
            thisSlotQuantityText.text = slotQuantity.ToString();
        }
        else
        {
            thisSlotQuantityText.text = "";
        }
    }

    public void swapItem(Item item, int count)
    {
        heldItem = item;

        if (item != null)
        {
            slotQuantity = count;
            thisSlotImage.sprite = heldItem.icon;
            thisSlotImage.color = opaque;
            updateData();
        }
        else
        {
            thisSlotImage.sprite = null;
            thisSlotImage.color = transparent;
            updateData();
        }
    }


    public bool hasItem()
    {
        return heldItem ? true : false;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        hovered = false;
    }
}
