using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthanGenerator : MonoBehaviour
{
    public GameObject bringerCharacter, BoltBulletEffect;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    Rigidbody2D rigid;
    Animator animator;
    private BringerOfDeathCharacter bringerOfDeathCharacter;
    private float lookingNumeric, lookingNumeric2;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bringerOfDeathCharacter = bringerCharacter.GetComponent<BringerOfDeathCharacter>();
    }

    public void bringerMakeWaveformEffect(float RorL)
    {
        StartCoroutine(MakeWaveformEffect(RorL));
    }

    private IEnumerator MakeWaveformEffect(float RorL)
    {
        float FlipXValue = EthanGetFlipX();
        Vector3 WaveformEffectPosition = new Vector3(0.5f * RorL + rigid.position.x, rigid.position.y);

        animator.SetTrigger("Death");
        yield return new WaitForSeconds(0.9f);
        animator.SetTrigger("DeathEnded");
        animator.SetTrigger("Attack01");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("Attack02");
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger("Attack03");

        Quaternion BoltRotation = Quaternion.Euler(0, FlipXValue * 90f + 90f, 0);
        GameObject waveform = Instantiate(BoltBulletEffect, WaveformEffectPosition, BoltRotation);
        BoltBulletEffect BoltBulletEffectScript = waveform.GetComponent<BoltBulletEffect>();
        
        if (BoltBulletEffectScript != null)
        {
            BoltBulletEffectScript.ShootWaveformEffect(RorL);
        }
        yield return null;
    }
    private float EthanGetFlipX()
    {
        if (spriteRenderer.flipX) return 1;
        else return -1;
    }
}
