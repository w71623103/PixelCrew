using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPickUpItem : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Start()
    {
        this.transform.tag = "Item";
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.itemSprite;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            GameManager.Instance().AddItem(item);
            this.gameObject.SetActive(false);
        }
    }
}
