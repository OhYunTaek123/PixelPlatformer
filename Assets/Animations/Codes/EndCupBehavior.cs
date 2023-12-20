using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCupBehavior : MonoBehaviour
{
    Animator animatorEndCup;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        animatorEndCup = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void attackToEndCup(GameObject EndCup)
    {
        StartCoroutine(EndCupCoroutine(EndCup));
    }
    private IEnumerator EndCupCoroutine(GameObject EndCup)
    {
        if(EndCup.tag == "EndCup" )
        {
            animatorEndCup.SetTrigger("EndCupMove");
            yield return null;
        }
        if (EndCup.tag == "EndCup2")
        {
            animatorEndCup.SetTrigger("EndCupMove");
            yield return null;
        }    
    }
}
