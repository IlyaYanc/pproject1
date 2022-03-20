using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(TimerManager))]
public class InventoryRenderer : MonoBehaviour
{
    [SerializeField] private InventoryComponent inventory;
    [SerializeField] private GameObject wholeContainer;

    //Inventory
    [SerializeField] private GameObject container;
    private List<GameObject> cells = new List<GameObject>();

    //Applied Items Bar
    //[SerializeField] private GameObject appliedItemsContainer;
    [SerializeField] private AttackController[] attackControllers;
    [SerializeField] private Sprite appliedCellSprite;
    [SerializeField] private Sprite defaultCellSprite;
    //private List<GameObject> applieditemsCells = new List<GameObject>();
    

    //Active Item
    [SerializeField] private Image ActiveItemImage;
    [SerializeField] private TextMeshProUGUI ActiveItemName;
    [SerializeField] private TextMeshProUGUI ActiveItemType;
    [SerializeField] private TextMeshProUGUI ActiveItemDescription;
    [SerializeField] private UnityEngine.UI.Button ActiveItemButton;
    [SerializeField] private UnityEngine.UI.Button DeleteButton;
    [SerializeField] private TextMeshProUGUI textToHide;
    private InventoryItem activeItem;
    
    [SerializeField] private HP[] HpControllers;
    [SerializeField] private EquipmentController[] equipmentControllers;
    
    //выбор персонажа, на которого применяется броня
    [SerializeField] private GameObject heroPanel;
    [SerializeField] private UnityEngine.UI.Button[] heroBtns;

    //timers
    private Timer boostCD_timer;
    private TimerManager _manager;
    private float damageBoostValue;
    private float defenceBoostValue;
    
    //damage texts
    [SerializeField] private TextMeshProUGUI[] dmgTexts;
    
    private void Start()
    {
        _manager = GetComponent<TimerManager>();
        
        ActiveItemButton.gameObject.SetActive(false);
        DeleteButton.gameObject.SetActive(false);
        wholeContainer.SetActive(false);

        Image[] contents = container.GetComponentsInChildren<Image>(true);
        for (int i = 0; i < contents.Length; i++)
        {
            if (contents[i].CompareTag("invCell"))
            {
                cells.Add(contents[i].gameObject);
            }
        }
        UpdateInventory(inventory.ReturnItems());
       
    }

    public void UpdateInventory(List<InventoryItem> items)
    {
        
        if(cells.Count < items.Count)
        {
            return;
        }

        for (int i = 0; i < cells.Count; i++)
        {
            if(i < items.Count)
            {
                int x = i;
                cells[i].GetComponentInChildren<Text>(true).text = items[i].amount.ToString();
                if(items[i].isApplied)
                {
                    cells[i].GetComponent<Image>().sprite = appliedCellSprite;
                }
                else
                {
                    cells[i].GetComponent<Image>().sprite = defaultCellSprite;
                }
                Image[] imgs = cells[i].GetComponentsInChildren<Image>(true);
                foreach (Image img in imgs)
                {
                    if (img.CompareTag("invSprite"))
                    {
                        img.sprite = items[i].item.inventorySprite;
                        img.gameObject.SetActive(true);
                        break;
                    }
                }
                UnityEngine.UI.Button btn = cells[i].GetComponent<UnityEngine.UI.Button>();
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => ActiveItemUpdate(items[x], x));

                if (items[i].isApplied && items[i].item.type == ItemType.Weapon)
                {
                    foreach (var attackController in attackControllers)
                    {
                        if (attackController.WeaponType() != ((WeaponItem) items[i].item).weaponType) continue;
                        attackController.SetActiveWeapon((WeaponItem) items[i].item, items[i].damage, false);
                        break;
                    }
                }
                
            }
            else
            {
                cells[i].GetComponentInChildren<Text>().text = "";
                Image[] imgs = cells[i].GetComponentsInChildren<Image>();
                foreach (Image img in imgs)
                {
                    if (img.CompareTag("invSprite"))
                    {
                        img.gameObject.SetActive(false);
                        break;
                    }
                }
                UnityEngine.UI.Button btn = cells[i].GetComponent<UnityEngine.UI.Button>();
                btn.onClick.RemoveAllListeners();
                cells[i].GetComponent<Image>().sprite = defaultCellSprite;
            }
        }



        if (items.Count == 0)
        {
            ActiveItemUpdate(null, 0);
        }
    }
    
    public void ActiveItemUpdate(InventoryItem _item, int _x)
    {
        TextMeshProUGUI btnText = ActiveItemButton.GetComponentInChildren<TextMeshProUGUI>(true);
        if (_item == null)
        {
            ActiveItemName.text = "";
            ActiveItemDescription.text = "";
            ActiveItemType.text = "";
            ActiveItemImage.color = new Color(0, 0, 0, 0);
            ActiveItemButton.onClick.RemoveAllListeners();
            DeleteButton.onClick.RemoveAllListeners();
            btnText.text = "";
            textToHide.text = "";
            return;
        }
        textToHide.text = "тип:";
        ActiveItemImage.color = new Color(100,100,100, 100);
        ActiveItemName.text = _item.item.itemName;
        ActiveItemDescription.text = _item.item.description;
        if(_item.item.type == ItemType.Weapon)
        {
            ActiveItemDescription.text += "\nурон: ";// + _item.damage * attackControllers[_x].GetDamageMultiplier();
            
        }
        ActiveItemType.text = _item.item.type.ToString();
        switch (_item.item.type)
        {
            case ItemType.Equipment: ActiveItemType.text = "экипировка"; break;
            case ItemType.Weapon: ActiveItemType.text = "оружие"; break;
            case ItemType.Food: ActiveItemType.text = "еда"; break;
                
        }
        
        ActiveItemImage.sprite = _item.item.inventorySprite;

        ActiveItemButton.onClick.RemoveAllListeners();
        DeleteButton.onClick.RemoveAllListeners();
        DeleteButton.onClick.AddListener(() => DeleteItem(_x));
        
        ActiveItemButton.gameObject.SetActive(true);
        DeleteButton.gameObject.SetActive(true);
        //TextMeshProUGUI btnText = ActiveItemButton.GetComponentInChildren<TextMeshProUGUI>(true);
        if (_item.item.canBeApplied)
        {
            
            btnText.text = !_item.isApplied ? "надеть" : "снять";

            ActiveItemButton.onClick.AddListener(() => ApplyItem(_item, _x));
        }
        else
        {
            btnText.text = "использовать";
            ActiveItemButton.onClick.AddListener(() => UseItem(_item, _x));
        }
        
        
    }

    private void FoodBoostTimerCompleted(Timer timer)
    {
        foreach (var hp in HpControllers)
        {
            hp.EncreaseResist(CalculateResist(defenceBoostValue));
        }

        for (var index = 0; index < attackControllers.Length; index++)
        {
            var attackController = attackControllers[index];
            attackController.DivDamageMultiplier(damageBoostValue);
            dmgTexts[index].text = attackControllers[index].GetDamage().ToString();
            Debug.Log(attackControllers[index].GetDamage().ToString());
            
        }

        defenceBoostValue = 0;
        damageBoostValue = 1;
    }
    
    void UseItem(InventoryItem _item, int _x)
    {
        ItemType itemType = _item.item.type;

        if (itemType == ItemType.Food)
        {
            FoodItem foodItem = ((FoodItem) _item.item);
            defenceBoostValue = foodItem.defenceBoostValue;
            damageBoostValue = foodItem.damageBoostValue;
            foreach (var hp in HpControllers)
            {
                hp.GetHill(foodItem.restoreHealthValue);
            }
            foreach (var hp in HpControllers)
            {
                
                hp.GetHill(foodItem.restoreHealthValue); 
                hp.DecreaseResist(CalculateResist(defenceBoostValue));
                boostCD_timer = new Timer(foodItem.boostLength, false, FoodBoostTimerCompleted);
                _manager.RegisterTimer(boostCD_timer);
                boostCD_timer.Restart();
            }

            for (var index = 0; index < attackControllers.Length; index++)
            {
                var attackController = attackControllers[index];
                attackController.MultiplyDamageMultiplier(damageBoostValue);
                dmgTexts[index].text = attackControllers[index].GetDamage().ToString();
                Debug.Log(attackControllers[index].GetDamage().ToString());
            }

            if (_item.amount > 1)
            {
                _item.amount--;
                inventory.ChangeItem(_item, _x);
                UpdateInventory(inventory.ReturnItems());
            }
            else
            {
                DeleteItem(_x);
            }
        }
    }
    private float CalculateResist(float _defence)
    {
        return (_defence/ 100f) * 0.3f;
    }
    
    public void UnapplyItem(InventoryItem _item)
    {
        Debug.Log("unequiping");
        List<InventoryItem> items = inventory.ReturnItems();
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i] == _item)
            {
                Debug.Log("unequiping " + _item);
                items[i].isApplied = false;
                int j = i;
                inventory.ChangeItem(_item, j);
                UpdateInventory(inventory.ReturnItems());
                break;
            }
        }
    }
    void ApplyItem(InventoryItem _item, int _x)
    {
        
        ItemType itemType = _item.item.type;

        if (itemType == ItemType.Weapon)
        {
            for (var index = 0; index < attackControllers.Length; index++)
            {
                var attackController = attackControllers[index];
                if (attackController.WeaponType() != ((WeaponItem) _item.item).weaponType)
                    continue;
                attackController.SetActiveWeapon((WeaponItem) _item.item, _item.damage, _item.isApplied);
                dmgTexts[index].text = attackControllers[index].GetDamage().ToString();
                break;
            }
        }
        if (itemType == ItemType.Equipment)
        {
            foreach (var equipmentController in equipmentControllers)
            {
                if (equipmentController.EqupmentClassType() != ((EquipmentItem) _item.item).classType) 
                    continue;
                if (((EquipmentItem) _item.item).equipmentType == equipmentType.ChestPlate)
                {
                    equipmentController.ApplyChestPlate((EquipmentItem)_item.item, _item.isApplied);
                }
                else
                {
                    equipmentController.ApplyHat((EquipmentItem)_item.item, _item.isApplied);
                }
                break;
            }
        }
        List<InventoryItem> items = inventory.ReturnItems();
        for(int i = 0; i < items.Count; i++)
        {
            if(_x != i && items[i].isApplied && items[i].item.type == itemType)
            {
                if (itemType != ItemType.Weapon && itemType != ItemType.Equipment)
                {
                    items[i].isApplied = false;
                    inventory.ChangeItem(items[i], i);
                }
                else if(itemType == ItemType.Weapon)
                {
                    if (((WeaponItem) (items[i].item)).weaponType == ((WeaponItem) _item.item).weaponType)
                    {
                        items[i].isApplied = false;
                        inventory.ChangeItem(items[i], i);
                    }
                }
                else if(itemType == ItemType.Equipment)
                {
                    if (((EquipmentItem) (items[i].item)).classType == ((EquipmentItem) _item.item).classType)
                    {
                        items[i].isApplied = false;
                        inventory.ChangeItem(items[i], i);
                    }
                }
                
            }
        }
        TextMeshProUGUI btnText = ActiveItemButton.GetComponentInChildren<TextMeshProUGUI>(true);
        
        _item.isApplied = !_item.isApplied;
        
        btnText.text = !_item.isApplied ? "надеть" : "снять";
        inventory.ChangeItem(_item, _x);
        UpdateInventory(inventory.ReturnItems());
    }

    void DeleteItem(int _x)
    {
        List<InventoryItem> items = inventory.ReturnItems();
        if (items[_x].item.type == ItemType.Weapon)
        {
            InventoryItem item = items[_x];
            if (item.isApplied)
            {
                foreach (var attackController in attackControllers)
                {
                    if (attackController.WeaponType() != ((WeaponItem) item.item).weaponType) continue;
                    attackController.SetActiveWeapon((WeaponItem)item.item, item.damage, item.isApplied);
                    break;
                }
            }
                //attackController.SetActiveWeapon((WeaponItem)items[_x].item, items[_x].damage, items[_x].isApplied);
        }
        inventory.DeleteItem(_x);
        UpdateInventory(inventory.ReturnItems());
        ActiveItemUpdate(null, 0);
        ActiveItemButton.gameObject.SetActive(false);
        DeleteButton.gameObject.SetActive(false);

    }

    public void InventoryOpenClose()
    {
        wholeContainer.SetActive(!wholeContainer.activeSelf);
    }
}
