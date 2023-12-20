using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossMonster : MonoBehaviour
{
    Animator animatorBoss;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    CapsuleCollider2D capsuleCollider;
    private float[] BossPatterns = new float[5] { 1, 2, 3, 4, 5 };
    private float BossPoints;
    private MovingMaskDude maskDude;
    private GameObject ghostGameObject;
    private bool GhostActive = false, FirstEngageBool  = true;
    public bool isBossPattern = false, isPatternOnce = false;
    private void Awake()
    {
        animatorBoss = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Start()
    {
        BossPoints = 100;
    }
    public void attackToBossMonster(Collider2D BossMob)
    {
        animatorBoss.SetTrigger("BossHit");
    }

    private void FixedUpdate()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.left, 5f, LayerMask.GetMask("Player"));
        if (rayHit.collider != null && FirstEngageBool)
        {
            StartCoroutine(FirstEngage());
        }
        if (GhostActive)
        {
            GhostActive = false;
            if (!isBossPattern && BossPoints > 0)
            {
                isBossPattern = true;
                rigid.velocity = Vector3.zero;
                StartCoroutine(DoBossPattern(BossPatterns[Random.Range(1, 5)]));
            }
        }
    }

    private void Update()
    {
        if(BossPoints <= 0)
        {
            animatorBoss.enabled = false;
        }
    }
    private IEnumerator FirstEngage()
    {
        FirstEngageBool = false;
        yield return new WaitForSeconds(3f);
        animatorBoss.SetTrigger("GhostDisappear");
        yield return new WaitForSeconds(0.5f);
        ghostObjectActive(false);
        yield return new WaitForSeconds(float.Epsilon);
        GhostActive = true;
    }

    public void attackToBossMonster()
    {
        StartCoroutine(onAttackedBoss());
    }
    private void ghostObjectActive(bool TorF)
    {
       spriteRenderer.enabled = TorF;
       
    }
    private IEnumerator onAttackedBoss()
    {
        animatorBoss.SetTrigger("GhostHit");

        spriteRenderer.color = new(1, 1, 1, 0.4f);
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        Invoke("RecoverColor", 1f);

        yield return null;
    }

    void RecoverColor()
    {
        spriteRenderer.color = new(1, 1, 1, 1);
    }
    private IEnumerator DoBossPattern(float randomPatternNum)
    {
        yield return new WaitForSeconds(2f);
        ghostObjectActive(true);
        animatorBoss.SetTrigger("GhostAppear");
        yield return new WaitForSeconds(float.Epsilon);
        BossSetBoolPattern(randomPatternNum, true);
        yield return new WaitForSeconds(1f);
        isPatternOnce = true;
        yield return new WaitForSeconds(1f);
        BossSetBoolPattern(randomPatternNum, false);
        animatorBoss.SetTrigger("GhostDisappear");
        yield return new WaitForSeconds(0.5f);
        ghostObjectActive(false);
        yield return new WaitForSeconds(0.5f);
        GhostActive = true;
        isBossPattern = false;
    }
    private void BossSetBoolPattern(float BossPatternsNum, bool TorF)
    {
        switch (BossPatternsNum)
        {
            case 1: animatorBoss.SetBool("Pattern1", TorF); animatorBoss.transform.position = new Vector3(9.1f, -0.73f, 0f); spriteRenderer.flipX = false; break;
            case 2: animatorBoss.SetBool("Pattern2", TorF); animatorBoss.transform.position = new Vector3(9.1f, 0f, 0f); spriteRenderer.flipX = false; break;
            case 3: animatorBoss.SetBool("Pattern3", TorF); animatorBoss.transform.position = new Vector3(5f, -0.73f, 0f); spriteRenderer.flipX = true; break;
            case 4: animatorBoss.SetBool("Pattern4", TorF); animatorBoss.transform.position = new Vector3(5f, 0f, 0f); spriteRenderer.flipX = true; break;
            case 5: animatorBoss.SetBool("Pattern5", TorF); animatorBoss.transform.position = new Vector3(7.05f, 0.9f, 0f); spriteRenderer.flipX = false; break;
        }
    }

    public float GetPattern()
    {
        if (animatorBoss.GetBool("Pattern1") == true)
        {
            return 1;
        }
        if (animatorBoss.GetBool("Pattern2") == true)
        {
            return 2;
        }
        if (animatorBoss.GetBool("Pattern3") == true)
        {
            return 3;
        }
        if (animatorBoss.GetBool("Pattern4") == true)
        {
            return 4;
        }
        if (animatorBoss.GetBool("Pattern5") == true)
        {
            return 5;
        }
        else return 0;
    }
}
