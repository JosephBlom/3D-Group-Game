using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    [Header("UI")]
    public GameObject inventory;
    public List<Slot> inventorySlots = new List<Slot>();
    public Image crosshair;
    public TMP_Text itemHoverText;

    [Header("Raycast")]
    public float raycastDistance = 5f;
    public LayerMask itemLayer;
    public Transform dropLocation;

    [Header("Drag and Drop")]
    public Image dragIconImage;
    private Item currentDraggedItem;
    private int currentDragSlotIndex = -1;

    [Header("Save")]
    [SerializeField] ItemList list;
    private Player player;
    private List<Item> allItems;

    private PlayerManager playerManager;

    private void Start()
    {
        toggleInventory(false);
        Cursor.visible = true;

        foreach(Slot uiSlots in inventorySlots)
        {
            uiSlots.initialiseSlot();
        }

        player = GetComponent<Player>();
        playerManager = GetComponentInChildren<PlayerManager>();
        allItems = list.allItems;
        loadInventory(player);
    }

    private void Update()
    {
        itemRaycast(Input.GetMouseButtonDown(0));

        if (Input.GetKeyDown(KeyCode.Z))
        {
            toggleInventory(!inventory.activeInHierarchy);
        }
        if(inventory.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            dragInventoryIcon();
        }
        else if(currentDragSlotIndex != -1 && Input.GetMouseButtonUp(0) || currentDragSlotIndex != -1 && !inventory.activeInHierarchy)
        {
            dropInventoryIcon();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            dropItem();
        }
        dragIconImage.transform.position = Input.mousePosition;
    }

    public void loadInventory(Player player)
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            for(int y = 0; y < allItems.Count; y++)
            {
                if (allItems[y].name.Equals(player.itemNames[i]))
                {
                    //Work on using the set item command to set the item.
                    inventorySlots[i].slotQuantity = player.itemCurQuantity[i];
                    inventorySlots[i].setItem(allItems[y]);
                    
                }
            }
        }
    }

    private void itemRaycast(bool hasClicked = false)
    {
        itemHoverText.text = "";
        Ray ray = Camera.main.ScreenPointToRay(crosshair.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, itemLayer))
        {
            if(hit.collider != null)
            {
                if (hasClicked) //pick up
                {
                    Item newItem = hit.collider.GetComponent<Item>();
                    if (newItem)
                    {
                        playerManager.quest.goal.ItemCollected(hit.collider.tag);
                        playerManager.quest.checkComplete();
                        addItemToInventory(newItem);
                    }
                }
                else //show name
                {
                    Item newItem = hit.collider.GetComponent<Item>();

                    if (newItem)
                    {
                        itemHoverText.text = newItem.name;
                    }
                }
            }
        }
    }

    private void addItemToInventory(Item itemToAdd)
    {
        int leftoverQuantity = itemToAdd.currentQuantity;
        Slot openSlot = null;
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            Item heldItem = inventorySlots[i].getItem();

            if (heldItem != null && itemToAdd.name == heldItem.name)
            {
                int freeSpaceInSlot = heldItem.maxQuantity -heldItem.currentQuantity;

                if (freeSpaceInSlot >= leftoverQuantity)
                {
                    inventorySlots[i].slotQuantity += leftoverQuantity;
                    Destroy(itemToAdd.gameObject);
                    inventorySlots[i].updateData();
                    return;
                }
                else
                {
                    inventorySlots[i].slotQuantity = heldItem.maxQuantity;
                    leftoverQuantity -= freeSpaceInSlot;
                }
            }
            else if (heldItem == null)
            {
                if (!openSlot)
                    openSlot = inventorySlots[i];
            }

            inventorySlots[i].updateData();
        }

        if(leftoverQuantity > 0 && openSlot)
        {
            openSlot.setItem(itemToAdd);
            itemToAdd.currentQuantity = leftoverQuantity;
            itemToAdd.gameObject.SetActive(false);
        }
        else
        {
            itemToAdd.currentQuantity = leftoverQuantity;
        }
    }

    private void toggleInventory(bool enable)
    {
        inventory.SetActive(enable);

        Cursor.lockState = enable ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = enable;

        GetComponentInChildren<StarterAssets.StarterAssetsInputs>().cursorInputForLook = !enable;
    }

    private void dragInventoryIcon()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            Slot curSlot = inventorySlots[i];
            if(curSlot.hovered && curSlot.hasItem())
            {
                currentDragSlotIndex = i;

                currentDraggedItem = curSlot.getItem();
                dragIconImage.sprite = currentDraggedItem.icon;
                dragIconImage.color = new Color(1, 1, 1, 1);

                curSlot.setItem(null);
            }
        }
    }

    private void dropInventoryIcon()
    {
        dragIconImage.sprite = null;
        dragIconImage.color = new Color(1, 1, 1, 0);

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            Slot curSlot = inventorySlots[i];
            if (curSlot.hovered)
            {
                if (curSlot.hasItem())
                {
                    Item itemToSwap = curSlot.getItem();

                    curSlot.setItem(currentDraggedItem);

                    inventorySlots[currentDragSlotIndex].setItem(itemToSwap);

                    resetDragVariables();
                    return;
                }
                else
                {
                    curSlot.setItem(currentDraggedItem);
                    resetDragVariables();
                    return;
                }
            }
        }

        //If we get to this point we have either dropped the item in a non "inventory" spot or closed the inventory.
        inventorySlots[currentDragSlotIndex].setItem(currentDraggedItem);
        resetDragVariables();
    }

    private void dropItem()
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            Slot curSlot = inventorySlots[i];
            if(curSlot.hovered && curSlot.hasItem())
            {
                curSlot.getItem().gameObject.SetActive(true);
                curSlot.getItem().transform.position = dropLocation.position;
                curSlot.setItem(null);
                break;
            }
        }
    }

    private void resetDragVariables()
    {
        currentDraggedItem = null;
        currentDragSlotIndex = -1;
    }
}
