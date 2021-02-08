using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public Image PurchaseItemDisplay;
    public ShopSlot[] ItemSlots;
    public InventoryItem[] ShopItems;
    private static ShopSlot SelectedShopSlot;

    private int nextSlotIndex = 0;
    public Text shopKeeperText;

    void Start()
    {
        if (ItemSlots.Length > 0 && ShopItems.Length > 0)
        {
            for (int i = 0; i < ShopItems.Length; i++)
            {
                if (nextSlotIndex > ItemSlots.Length) break;
                ItemSlots[nextSlotIndex].AddShopItem(ShopItems[i]);
                ItemSlots[nextSlotIndex].Manager = this;
                nextSlotIndex++;
            }
        }
    }

    public void SetShopSelectedItem(ShopSlot slot)
    {
        SelectedShopSlot = slot;
        PurchaseItemDisplay.sprite = slot.Item.itemImage;
        shopKeeperText.text = " ";
    }
    public void ConfirmPurchase()
    {
        PurchaseSelectedItem();
        PurchaseItemDisplay.sprite = null;
        shopKeeperText.text = "Thanks fer your money!";
    }

    public static void PurchaseSelectedItem()
    {
        SelectedShopSlot.PurchaseItem();
    }

    public void LeaveTheShop()
    {
        NavigationManager.NavigateTo("Village");
    }


}
