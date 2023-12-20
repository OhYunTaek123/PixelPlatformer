using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    Animator animatorItem;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        animatorItem = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void attackToItem(GameObject ItemFruit)
    {
        StartCoroutine(ItemCoroutine(ItemFruit));
    }
    private IEnumerator ItemCoroutine(GameObject ItemFruit)
    {
        animatorItem.SetTrigger("ItemDestroy");
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
