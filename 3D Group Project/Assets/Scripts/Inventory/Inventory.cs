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
    [SerializeField] PlayerHealthSystem playerHealthSystem;
    public Item currentDraggedItem;
    public Slot currentDraggedSlot;
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
                    inventorySlots[i].setItem(allItems[y], null);
                    inventorySlots[i].slotQuantity = player.itemCurQuantity[i];
                    inventorySlots[i].updateData();
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
                        addItemToInventory(newItem, false);
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

    public void addItemToInventory(Item itemToAdd, bool shopItem)
    {
        int leftoverQuantity = itemToAdd.currentQuantity;
        Slot openSlot = null;
        Item itemPrefab = null;

        for(int i = 0; i < allItems.Count; i++)
        {
            if (allItems[i].name.Equals(itemToAdd.name))
            {
                itemPrefab = allItems[i];
            }
        }
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            Item heldItem = inventorySlots[i].getItem();

            if (heldItem != null && itemToAdd.name == heldItem.name && !inventorySlots[i].equipmentSlot)
            {
                int freeSpaceInSlot = heldItem.maxQuantity -inventorySlots[i].slotQuantity;

                if (freeSpaceInSlot >= leftoverQuantity)
                {
                    inventorySlots[i].slotQuantity += leftoverQuantity;
                    inventorySlots[i].updateData();
                    if(!shopItem)
                        Destroy(itemToAdd.gameObject);
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
            openSlot.setItem(itemPrefab, null);
            openSlot.slotQuantity = leftoverQuantity;
            Destroy(itemToAdd.gameObject);
            openSlot.updateData();
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
        if (GetComponentInChildren<GunSystem>() != null)
        {
            GetComponentInChildren<GunSystem>().active = enable ? false : true;
        }

        GetComponentInChildren<StarterAssets.StarterAssetsInputs>().cursorInputForLook = !enable;
    }

    private void dragInventoryIcon()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            Slot curSlot = inventorySlots[i];
            if(curSlot.hovered && curSlot.hasItem() && curSlot.equipmentSlot && curSlot.getItem().CompareTag("Equipment"))
            {
                currentDragSlotIndex = i;
                currentDraggedSlot = curSlot;

                currentDraggedItem = curSlot.getItem();
                dragIconImage.sprite = currentDraggedItem.icon;
                dragIconImage.color = new Color(1, 1, 1, 1);

                playerHealthSystem.removeItemBuffs(curSlot.getItem());

                curSlot.setItem(null, null);
            }
            else if(curSlot.hovered && curSlot.hasItem())
            {
                currentDragSlotIndex = i;
                currentDraggedSlot = curSlot;
                
                currentDraggedItem = curSlot.getItem();
                dragIconImage.sprite = currentDraggedItem.icon;
                dragIconImage.color = new Color(1, 1, 1, 1);

                curSlot.setItem(null, null);
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
                    if(!curSlot.equipmentSlot && !inventorySlots[currentDragSlotIndex].equipmentSlot)
                    {
                        Item itemToSwap = curSlot.getItem();

                        int count = curSlot.slotQuantity;

                        curSlot.setItem(currentDraggedItem, currentDraggedSlot);

                        inventorySlots[currentDragSlotIndex].swapItem(itemToSwap, count);

                        resetDragVariables();
                        return;
                    }
                    else
                    {
                        if(curSlot.equipmentSlot && !inventorySlots[currentDragSlotIndex].equipmentSlot)
                        {
                            Item itemToSwap = curSlot.getItem();
                            Item itemBeingSwapped = inventorySlots[currentDragSlotIndex].getItem();
                            if (itemBeingSwapped.CompareTag("Equipment"))
                            {
                                int count = curSlot.slotQuantity;
                                curSlot.setItem(currentDraggedItem, currentDraggedSlot);
                                inventorySlots[currentDragSlotIndex].swapItem(itemToSwap, count);
                                playerHealthSystem.addItemBuffs(curSlot.getItem());
                                resetDragVariables();
                                return;
                            }
                        }
                        else if(!curSlot.equipmentSlot && inventorySlots[currentDragSlotIndex].equipmentSlot)
                        {
                            Item itemToSwap = curSlot.getItem();
                            if (itemToSwap.CompareTag("Equipment"))
                            {
                                int count = curSlot.slotQuantity;
                                curSlot.setItem(currentDraggedItem, currentDraggedSlot);
                                inventorySlots[currentDragSlotIndex].swapItem(itemToSwap, count);
                                playerHealthSystem.addItemBuffs(inventorySlots[currentDragSlotIndex].getItem());
                                resetDragVariables();
                                return;
                            }
                        }
                    }
                    
                }
                else
                {
                    if (curSlot.equipmentSlot)
                    {
                        if (currentDraggedItem.CompareTag("Equipment"))
                        {
                            curSlot.setItem(currentDraggedItem, currentDraggedSlot);
                            playerHealthSystem.addItemBuffs(curSlot.getItem());
                            resetDragVariables();
                            return;
                        }
                    }
                    else
                    {

                        curSlot.setItem(currentDraggedItem, currentDraggedSlot);
                        resetDragVariables();
                        return;
                    }
                }
            }
        }

        //If we get to this point we have either dropped the item in a non "inventory" spot or closed the inventory.
        inventorySlots[currentDragSlotIndex].setItem(currentDraggedItem, inventorySlots[currentDragSlotIndex]);
        resetDragVariables();
    }

    private void dropItem()
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            Slot curSlot = inventorySlots[i];
            if (curSlot.hovered && curSlot.hasItem() && curSlot.equipmentSlot && curSlot.getItem().CompareTag("Equipment"))
            {
                GameObject droppedItem = Instantiate(curSlot.getItem().gameObject, dropLocation.position, Quaternion.identity);
                droppedItem.GetComponent<Item>().currentQuantity = curSlot.slotQuantity;
                playerHealthSystem.removeItemBuffs(curSlot.getItem());
                curSlot.setItem(null, null);
                break;
            }
            else if(curSlot.hovered && curSlot.hasItem())
            {
                GameObject droppedItem = Instantiate(curSlot.getItem().gameObject, dropLocation.position, Quaternion.identity);
                droppedItem.GetComponent<Item>().currentQuantity = curSlot.slotQuantity;
                curSlot.setItem(null, null);
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
