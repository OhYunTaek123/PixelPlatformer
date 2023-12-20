using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeathCharacter : MonoBehaviour
{
    public GameObject ethanCharacter, BringerOfDeathEffect;
    EthanGenerator ethanGenerator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    private bool isPattern = false;
    private float speed = 1f;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        ethanGenerator = ethanCharacter.GetComponent<EthanGenerator>();
    }

    void FixedUpdate()
    {
        if (!isPattern)
        {
            BringerWalk();
        }
        RaycastHit2D boxHit = Physics2D.BoxCast(rigid.position, capsuleCollider.size, 0f, Vector3.right * BringerGetFlipX(), 1f);
        if(boxHit.collider != null)
        {
            if (boxHit.collider.tag == "Player")
            {
                if (!isPattern)
                {
                    animator.SetBool("isWalk", false);
                    rigid.velocity = Vector3.zero;
                    isPattern = true;
                    BossSetBoolPattern(Random.Range(1, 4));
                }
            }
        }
    }
    private void BossSetBoolPattern(float RandNum)
    {
        switch (RandNum)
        {
            case 1: BringerSpell(); break;
            case 2: BringerAttack(); break;
            case 3: EthanAttack(); break;
        }
    }
    public bool GetFlipX()
    {
        return spriteRenderer.flipX;
    }
    private void BringerWalk()
    {
        StartCoroutine(MakeBringerWalkEffect());
    }
    public void attackToBossMonster()
    {
        animator.SetTrigger("isHurt");
        isPattern = true;

        spriteRenderer.color = new(1, 1, 1, 0.4f);
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        rigid.AddForce(Vector2.right * 3 * BringerGetFlipX(), ForceMode2D.Impulse);
        Invoke("RecoverColor", 1f);
    }
    void RecoverColor()
    {
        spriteRenderer.color = new(1, 1, 1, 1);
        isPattern = false;
    }
    private void BringerSpell()
    {
        StartCoroutine(MakeBringerSpellEffect());
    }
    private void BringerAttack()
    {
        StartCoroutine(BringerAttackEffect());
    }
    private void EthanAttack()
    {
         StartCoroutine(MakeEthanEffect());
    }
    private IEnumerator BringerAttackEffect()
    {
        animator.SetTrigger("isAttack");
        yield return new WaitForSeconds(2f);
        isPattern = false;
    }
    private IEnumerator MakeBringerWalkEffect()
    {
        animator.SetBool("isWalk", true);
        Vector2 velocity = new Vector3(BringerGetFlipX(), 0f) * speed;
        rigid.velocity = velocity;

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.right * BringerGetFlipX(), 0f, LayerMask.GetMask("Platform"));
        if (rayHit.collider != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            capsuleCollider.offset = new Vector2(-capsuleCollider.offset.x, capsuleCollider.offset.y);
            rigid.transform.position = new Vector2(rigid.position.x + BringerGetFlipX() * 0.5f, rigid.position.y);
        }
        yield return null;
    }
    private IEnumerator MakeBringerSpellEffect()
    {
        animator.SetTrigger("isCast");
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 3; i++)
        {
            Vector3 MakeEffectPosition = new Vector3(0.5f * BringerGetFlipX() + rigid.position.x + i * 0.65f * BringerGetFlipX(), rigid.position.y);

            GameObject BringerOfDeathAttackEffect = Instantiate(BringerOfDeathEffect, MakeEffectPosition, Quaternion.identity);
            BringerOfDeathEffect BringerAttackEffectScript = BringerOfDeathAttackEffect.GetComponent<BringerOfDeathEffect>();
            if(BringerAttackEffectScript != null)
            {
                BringerAttackEffectScript.ShootSpellEffect();
            }
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1.5f);
        isPattern = false;
    }
    private IEnumerator MakeEthanEffect()
    {
        float FlipXValue = BringerGetFlipX();
        Vector3 EthanEffectPosition = new Vector3(0.5f * BringerGetFlipX() + rigid.position.x, rigid.position.y - 0.15f);

        Quaternion ethanRotation = Quaternion.Euler(0, FlipXValue * -90f + 90f, 0);
        GameObject EthanCharacter = Instantiate(ethanCharacter, EthanEffectPosition, ethanRotation);
        EthanGenerator EthanGeneratorScript = EthanCharacter.GetComponent<EthanGenerator>();

        if (EthanGeneratorScript != null)
        {
            EthanGeneratorScript.bringerMakeWaveformEffect(BringerGetFlipX());
        }
        yield return new WaitForSeconds(3f);
        Destroy(EthanCharacter);
        yield return new WaitForSeconds(1f);
        isPattern = false;
    }

    private float BringerGetFlipX()
    {
        if (spriteRenderer.flipX) return 1;
        else return -1;
    }
}
