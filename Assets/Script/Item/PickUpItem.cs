using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private void Start()
    {
        this.transform.tag = "Item";
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.itemSprite;
    }

    public Item getItem()
    {
        return item;
    }
}
