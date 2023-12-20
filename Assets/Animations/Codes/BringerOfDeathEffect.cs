using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeathEffect : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    BoxCollider2D boxCollider;
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ShootSpellEffect()
    {
        StartCoroutine(MakeSpellEffect());
    }

    private IEnumerator MakeSpellEffect()
    {
        yield return new WaitForSeconds(1.7f);
        Destroy(gameObject);
    }
}
