using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectCode : MonoBehaviour
{
    public GameObject ghostEffectPrefab, Effect, ghostAttackEffectPrefab;
    private MovingMaskDude maskDude;
    private float lookingNumeric, lookingNumeric2;
    private Vector3 minusPosition, afterPosition;
    private bool CanDash = true;

    private void Start()
    {
        maskDude = Effect.GetComponent<MovingMaskDude>();
    }

    private void Update()
    {
        if (!maskDude.GetFlipX())
        {
            lookingNumeric = 0;
            lookingNumeric2 = 1;
        }
        else
        {
            lookingNumeric = 180;
            lookingNumeric2 = -1;
        }

        if (Input.GetButtonDown("Fire2") && CanDash)
        {
            StartCoroutine(MakeDashEffect());
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 AttackEffectPosition = maskDude.GetPosition() + new Vector3(0.4f * lookingNumeric2, 0f);
            GameObject ghostAttackEffect = Instantiate(ghostAttackEffectPrefab, AttackEffectPosition, Quaternion.Euler(0, lookingNumeric, 0));

            GhostAttackEffect ghostAttackEffectScript = ghostAttackEffect.GetComponent<GhostAttackEffect>();
            if (ghostAttackEffectScript != null)
            {
                ghostAttackEffectScript.StartFadeOut();
            }
        }
    }
    private IEnumerator MakeDashEffect()
    {
        CanDash = false;
        minusPosition = new Vector3(-0.3f * lookingNumeric2, 0, 0);
        yield return new WaitForSeconds(float.Epsilon);
        afterPosition = maskDude.GetPosition();
        GameObject ghostEffect = Instantiate(ghostEffectPrefab, afterPosition + minusPosition, Quaternion.Euler(0, lookingNumeric, 0));

        GhostEffect ghostEffectScript = ghostEffect.GetComponent<GhostEffect>();
        if (ghostEffectScript != null)
        {
            ghostEffectScript.StartFadeOut();
        }
        yield return new WaitForSeconds(1f);
        CanDash = true;
    }
}
