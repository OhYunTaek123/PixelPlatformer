using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoltBulletEffect : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    public float speed = 2f;
    private float BoltFlip;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        BoltFlip = BoltGetFlipX();
    }
    private void Update()
    {
        OnTriggerEnter2D(boxCollider);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Map"))
        {
            Destroy(gameObject);
        }
    }
    public void ShootWaveformEffect(float RorL)
    {
        StartCoroutine(MakeBulletEffect(RorL));
    }
    private IEnumerator MakeBulletEffect(float RorL)
    {
        yield return new WaitForSeconds(0.2f);
        Vector2 velocity =  new Vector2(RorL, 0) * speed;
        if (RorL == 1) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
        rigid.velocity = velocity;
        yield return new WaitForFixedUpdate();
    }
    private float BoltGetFlipX()
    {
        if (spriteRenderer.flipX) return 1;
        else return -1;
    }
}
