using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private string itemname;

    private void Start()
    {
        this.transform.tag = "Item";
    }

    public void getItem()
    {
        Debug.Log("Fetched " + itemname);
        gameObject.SetActive(false);
    }

    public string getItemName()
    {
        return itemname;
    }
}
