using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Effect;
    private MovingMaskDude maskDude;
    public Text text;

    private void Start()
    {
        maskDude = Effect.GetComponent<MovingMaskDude>();
    }

    private void LateUpdate()
    {
        BulletHitted();
    }

    public void BulletHitted()
    {
        text.text = "HitPoint : " + maskDude.MaskDudeHitPoint.ToString();
    }

    public void GetScore()
    {
        maskDude.MaskDudeHitPoint++;
        SetText();
    }

    public void SetText()
    {
        text.text = "HitPoint : " + maskDude.MaskDudeHitPoint++.ToString();
    }
}
