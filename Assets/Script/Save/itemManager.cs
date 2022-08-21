using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemManager : MonoBehaviour 
{ 
    public static itemManager instance;
    [SerializeField] private string[] currentItem;
    private int itemcount = 0;
    private void Awake()
    {
        if(instance == null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(this);
    }

    public void getItem(GameObject obj)
    {
        if (obj.transform.tag == "Item")
        {
            currentItem[itemcount] = obj.transform.GetComponent<Item>().getItemName();
            itemcount ++;
        }
    }

    public void printItemList()
    {
        foreach(string s in currentItem)
        {
            Debug.Log(s);
        }
    }
}
