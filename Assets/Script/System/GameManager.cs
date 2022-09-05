using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public Dictionary<Item,int> itemDic = new Dictionary<Item,int>();

    public static GameManager Instance()
    {
        if (instance == null)
            Debug.Log("Null Gamemanager");
        return instance;
    }

    private GameManager() { }
    void Awake()
    {
        instance = this;
        
        DontDestroyOnLoad(this);
    }
    
    void Start()
    {
        Debug.Log("HELLO");
    }
    private void DisplayItems()
    {
        foreach(var item in itemDic.Keys)
        {
            Debug.Log("Now Got " + itemDic[item] +" "+ item.itemName);
        }
    }

    public void AddItem(Item item)
    {
        // if item is a NEW item,then add it to the dic
        if (!itemDic.ContainsKey(item))
        {
            itemDic.Add(item, 1);
        }
        // else add the count of item
        else
        {
            itemDic[item]++;
        }
        // display for debug
        DisplayItems();
    }
}
