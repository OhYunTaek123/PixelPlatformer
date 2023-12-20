using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostParticlesShoot : MonoBehaviour
{
    public GameObject GhostParticlesPrefab, Effect;
    private FirstBossMonster FirstBoss;
    private Vector3 ShootinArrowRotation;

    private void Start()
    {
        FirstBoss = Effect.GetComponent<FirstBossMonster>();
    }
    private void Update()
    {
        MakeGhostParticles();
    }
    private void MakeGhostParticles()
    {
        if (FirstBoss.isBossPattern && FirstBoss.isPatternOnce)
        {
            FirstBoss.isPatternOnce = false;
            switch (FirstBoss.GetPattern())
            {
                case 1:
                    StartCoroutine(MakePatternNum1());
                    break;
                case 2:
                    StartCoroutine(MakePatternNum2());
                    break;
                case 3:
                    StartCoroutine(MakePatternNum3());
                    break;
                case 4:
                    StartCoroutine(MakePatternNum4());
                    break;
                case 5:
                    StartCoroutine(MakePatternNum5());
                    break;
            }
        }
    }

    private IEnumerator MakePatternNum1()
    {
        for(float i = -7; i <= 7; i += 1)
        {
            ShootinArrowRotation = new Vector3(-Random.Range(5,15), -i, 0);
            GameObject ghostAttackParticles = Instantiate(GhostParticlesPrefab, FirstBoss.transform.position, Quaternion.identity);
            GhostParticles shootingParticles = ghostAttackParticles.GetComponent<GhostParticles>();
            shootingParticles.StartShootParticles(ShootinArrowRotation);
            yield return null;
        }
    }
    private IEnumerator MakePatternNum2()
    {
        for (float i = -7; i <= 7; i += 1)
        {
            ShootinArrowRotation = new Vector3(-Random.Range(5, 15), i, 0);
            GameObject ghostAttackParticles = Instantiate(GhostParticlesPrefab, FirstBoss.transform.position, Quaternion.identity);
            GhostParticles shootingParticles = ghostAttackParticles.GetComponent<GhostParticles>();
            shootingParticles.StartShootParticles(ShootinArrowRotation);
            yield return null;
        }
    }
    private IEnumerator MakePatternNum3()
    {
        for (float i = -7; i <= 7; i += 1)
        {
            ShootinArrowRotation = new Vector3(Random.Range(5, 15), i, 0);
            GameObject ghostAttackParticles = Instantiate(GhostParticlesPrefab, FirstBoss.transform.position, Quaternion.identity);
            GhostParticles shootingParticles = ghostAttackParticles.GetComponent<GhostParticles>();
            shootingParticles.StartShootParticles(ShootinArrowRotation);
            yield return null;
        }
    }
    private IEnumerator MakePatternNum4()
    {
        for (float i = -7; i <= 7; i += 1)
        {
            ShootinArrowRotation = new Vector3(Random.Range(5, 15), i, 0);
            GameObject ghostAttackParticles = Instantiate(GhostParticlesPrefab, FirstBoss.transform.position, Quaternion.identity);
            GhostParticles shootingParticles = ghostAttackParticles.GetComponent<GhostParticles>();
            shootingParticles.StartShootParticles(ShootinArrowRotation);
            yield return null;
        }
    }
    private IEnumerator MakePatternNum5()
    {
        for (float i = -7; i <= 7; i += 1)
        {
            ShootinArrowRotation = new Vector3(i, -Random.Range(5, 15), 0);
            GameObject ghostAttackParticles = Instantiate(GhostParticlesPrefab, FirstBoss.transform.position, Quaternion.identity);
            GhostParticles shootingParticles = ghostAttackParticles.GetComponent<GhostParticles>();
            shootingParticles.StartShootParticles(ShootinArrowRotation);
            yield return null;
        }
    }
}
