using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static public InventoryManager instance;

    [SerializeField] private GameObject slotGrid;
    [SerializeField] private  Slot slotPref;
    public static InventoryManager Instance()
    {
        if (instance == null)
            Debug.Log("Null Gamemanager");
        return instance;
    }

    private InventoryManager() { }
    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this);
    }

    public static void addNewItem(Item item)
    {
        Slot newSlot = Instantiate(instance.slotPref, instance.slotGrid.transform);
        newSlot.gameObject.transform.SetParent(instance.slotGrid.transform);
        newSlot.slotItem = item;
        newSlot.slotImage.sprite = item.itemSprite;
        newSlot.Amount.text = GameManager.instance.getItemAmount(item);
        if (item.isEpic)
        {
            newSlot.Amount.gameObject.SetActive(false);
        }
    }   

    public static void refreshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        
        foreach(Item item in GameManager.instance.itemDic.Keys)
        {
            addNewItem(item);
        }
    }
}
 