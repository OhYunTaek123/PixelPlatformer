using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostParticles : MonoBehaviour
{
    public float fadeSpeed = 1f, speed = 1;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;
    private Color startColor;
    private CircleCollider2D circleCollider;
    private float lifeTime = 15f;
    public bool CanHit = true;
    private MovingMaskDude maskDude;
    public GameObject Effect;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        maskDude = Effect.GetComponent<MovingMaskDude>();
        startColor = spriteRenderer.color;
    }
    public void FixedUpdate()
    {
        if(rigid.position.x < 4.64f || rigid.position.x > 9.44f)
        {
            rigid.velocity = new Vector2(-rigid.velocity.x, rigid.velocity.y);
        }
        if(rigid.position.y < -0.96f || rigid.position.y > 1.28)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -rigid.velocity.y);
        }
        if(circleCollider.gameObject.CompareTag("Map"))
        {
            Debug.Log("Å©¾Ç");
        }
        OnTriggerEnter2D(circleCollider);
    }
    public void StartShootParticles(Vector3 arrowRotation)
    {
        StartCoroutine(ShootParticles(arrowRotation));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            maskDude.MaskDudeHitPoint++;
        }
    }

    private IEnumerator ShootParticles(Vector3 arrowRotation)
    {
        Vector2 velocity = arrowRotation * speed;
        rigid.velocity = velocity;
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
