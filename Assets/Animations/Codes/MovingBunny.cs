using System;
using UnityEngine;

public class MovingBunny : MonoBehaviour
{
    Rigidbody2D rigidMob;
    private Animator animatorMob;
    public int nextMove = 1;
    private SpriteRenderer spriteRendererMob;
    private CapsuleCollider2D capsuleCollider2D;
    void Awake()
    {
        rigidMob = GetComponent<Rigidbody2D>();
        animatorMob = GetComponent<Animator>();
        spriteRendererMob = GetComponent<SpriteRenderer>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        Vector2 mobFrontVec = new Vector2(rigidMob.position.x + nextMove * 0.2f, rigidMob.position.y);
        RaycastHit2D rayMobFrontHit = Physics2D.Raycast(mobFrontVec, Vector3.down, 1.2f, LayerMask.GetMask("Platform"));

        if (rayMobFrontHit.collider == null || rayMobFrontHit.distance < 0.17f)
        {
            nextMove = nextMove * (-1);
        }
    }

    private void moveMob()
    {
        rigidMob.velocity = new Vector2(nextMove * 0.5f, rigidMob.velocity.y);

        if (rigidMob.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (rigidMob.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Math.Abs(rigidMob.velocity.x) < 0.3)
        {
            animatorMob.SetBool("isWalk", false);
        }
        else
        {
            animatorMob.SetBool("isWalk", true);
        }
    }
    public void onDamaged()
    {
        spriteRendererMob.color = new(1, 1, 1, 0.4f);
        rigidMob.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        Invoke("RecoverColor", 1f);
    }

    void RecoverColor()
    {
        spriteRendererMob.color = new(1, 1, 1, 1);
    }

    void Update()
    {
        moveMob();
    }
}
