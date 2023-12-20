using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class MovingMaskDude : MonoBehaviour
{
    public GameObject ghostBullet;
    private GhostParticles ghostParticles;
    public float maxSpeed, jumpPower, dashSpeed, jumpTimeLimitRate, MaskDudeHitPoint = 0;
    private Rigidbody2D rigid;
    private Animator animator_;
    private SpriteRenderer spriteRenderer;
    private Collision2D collision;
    private CapsuleCollider2D capsuleCollider;
    private float lookingArrow, jumpTime, jumpCount;
    private bool MovingCameraBool = false, MoveCameraBool = false, MoveCameraBool2 = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator_ = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Start()
    {
        ghostParticles = ghostBullet.GetComponent<GhostParticles>();
    }
    public Vector3 GetPosition()
    {
        return rigid.position;
    }
    public bool GetFlipX()
    {
        return spriteRenderer.flipX;
    }
    public bool GetBoolMaskDudeDash()
    {
        return animator_.GetBool("CanDash");
    }
    private void FixedUpdate()
    {
        myMove();
        if (collision != null)
        {
            OnCollisionEnter2D(collision);
        }
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 0.2f, LayerMask.GetMask("Platform"));
        if (rayHit.collider != null)
        {
            if (rayHit.distance < 0.2f && rigid.velocity.y <= 0)
            {
                jumpCount = Time.deltaTime * jumpTimeLimitRate;
                animator_.SetBool("MaskJump", false);
                animator_.SetBool("MaskDoubleJump", false);
                animator_.SetBool("MaskFall", false);
                animator_.SetBool("MaskIsJump", false);
            }
        }
        myDash();
        isFall();
        GoToBringer();
        GoToStartPosition();
    }

    private void Update()
    {
        if (animator_.GetBool("MaskIsJump"))
        {
            myDoubleJump();
        }
        else
        {
            myJump();
        }
        myAttack();
        if (MovingCameraBool)
        {
            CameraFollow CameraPositionUpdate = Camera.main.GetComponent<CameraFollow>();
            CameraPositionUpdate.positionKeepUpdate();
        }
        if (MoveCameraBool)
        {
            CameraFollow CameraPositionUpdate = Camera.main.GetComponent<CameraFollow>();
            CameraPositionUpdate.positionUpdate(new Vector3(7f, 0f, -10f));
        }
        if (MoveCameraBool2)
        {
            CameraFollow CameraPositionUpdate = Camera.main.GetComponent<CameraFollow>();
            CameraPositionUpdate.positionUpdate(new Vector3(15.28f, 0f, -10f));
        }
        if (MovingCameraBool == false && MoveCameraBool == false && MoveCameraBool2 == false)
        {
            CameraFollow CameraPositionUpdate = Camera.main.GetComponent<CameraFollow>();
            CameraPositionUpdate.positionUpdate(new Vector3(0f, 0f, -10f));
        }
    }

    private void myAttack()
    {
        if (spriteRenderer.flipX) lookingArrow = -1; else lookingArrow = 1;
        if (Input.GetButtonDown("Fire1"))
        {
            capsuleCollider.enabled = false;
            RaycastHit2D boxHit = Physics2D.BoxCast(rigid.position, capsuleCollider.size, 0, Vector3.right, 0.65f * lookingArrow);
            capsuleCollider.enabled = true;
            if (boxHit.collider != null)
            {
                if(boxHit.collider.tag == "Item")
                {
                    ItemBehavior ItemFruit = boxHit.collider.GetComponent<ItemBehavior>();
                    ItemFruit.attackToItem(boxHit.collider.gameObject);
                }
                if(boxHit.collider.tag == "Enemy")
                {
                    MovingBunny movingBunny = boxHit.collider.gameObject.GetComponent<MovingBunny>();
                    movingBunny.onDamaged();
                }
                if(boxHit.collider.tag == "Boss")
                {
                    FirstBossMonster BossMonster = boxHit.collider.gameObject.GetComponent<FirstBossMonster>();
                    BossMonster.attackToBossMonster();
                }
                if (boxHit.collider.tag == "Boss2")
                {
                    BringerOfDeathCharacter BossMonster = boxHit.collider.gameObject.GetComponent<BringerOfDeathCharacter>();
                    BossMonster.attackToBossMonster();
                }
                if (boxHit.collider.tag == "EndCup")
                {
                    EndCupBehavior EndCup = boxHit.collider.GetComponent<EndCupBehavior>();
                    EndCup.attackToEndCup(boxHit.collider.gameObject);
                    Debug.Log(boxHit.collider.gameObject);
                    rigid.position = new Vector3(4.96f, -0.8f, 0f);
                    MoveCameraBool = true;
                    MovingCameraBool = false;
                    MoveCameraBool2 = false;
                }
                if (boxHit.collider.tag == "EndCup2")
                {
                    EndCupBehavior EndCup = boxHit.collider.GetComponent<EndCupBehavior>();
                    EndCup.attackToEndCup(boxHit.collider.gameObject);
                    Debug.Log(boxHit.collider.gameObject);
                    rigid.position = new Vector3(-1f, -5.7f, 0f);
                    MovingCameraBool = true;
                    MoveCameraBool = false;
                    MoveCameraBool2 = false;
                }
                
            }
        }
    }

    private void GoToBringer()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MoveCameraBool2 = true;
            rigid.position = new Vector3(12.7f, -0.8f, 0f);
            MovingCameraBool = false;
            MoveCameraBool = false;
        }
    }

    private void GoToStartPosition()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rigid.position = new Vector3(0,0,0);
            MovingCameraBool = false;
            MoveCameraBool = false;
            MoveCameraBool2 = false;
        }
    }


    private void myMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector3.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            spriteRenderer.flipX = false;
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
            spriteRenderer.flipX = true;
        }

        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            animator_.SetBool("MaskRun", false);
        }
        else
        {
            animator_.SetBool("MaskRun", true);
        }
    }
    private void myJump()
    {
        if (Input.GetButton("Jump") && !animator_.GetBool("MaskDoubleJump"))
        {
            animator_.SetBool("MaskJump", true);
        }
        if (Input.GetButton("Jump") && jumpCount > 0)
        {
            rigid.velocity = Vector3.zero;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            jumpCount -= Time.deltaTime;
        }
        if (Input.GetButtonUp("Jump"))
        {
            animator_.SetBool("MaskIsJump", true);
            jumpCount = 0;
        }
    }
    private void isFall()
    {
        if (rigid.velocity.y < -0.3f)
        {
            animator_.SetBool("MaskFall", true);
            animator_.SetBool("MaskIsJump", true);
        }
    }
    private void myDoubleJump()
    {
        if (Input.GetButtonDown("Jump") && !animator_.GetBool("MaskDoubleJump"))
        {
            rigid.velocity = Vector3.zero;
            rigid.AddForce(Vector3.up * jumpPower * 1.1f, ForceMode2D.Impulse);
            animator_.SetBool("MaskDoubleJump", true);
            animator_.SetBool("MaskFall", false);
        }
    }
    public void myDash()
    {
        if (spriteRenderer.flipX) lookingArrow = -1; else lookingArrow = 1;
        if (Input.GetButton("Fire2") && !animator_.GetBool("CanDash"))
        {
            animator_.SetBool("CanDash", true);
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.right, 1.1f * lookingArrow, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                float wallDistance = (rayHit.distance -0.1f) / 0.02f;
                rigid.velocity = Vector3.zero;
                if (spriteRenderer.flipX)
                {
                    rigid.velocity = transform.right * -wallDistance;
                }
                else
                {
                    rigid.velocity = transform.right * wallDistance;
                }
            }
            else
            {
                rigid.velocity = Vector3.zero;
                if (spriteRenderer.flipX)
                {
                    rigid.velocity = transform.right * -dashSpeed;
                }
                else
                {
                    rigid.velocity = transform.right * dashSpeed;
                }
            }
            Invoke("moveZero", float.Epsilon);
            Invoke("changeCanDash", 1f);
        }
    }
    private void moveZero()
    {
        rigid.velocity = Vector3.zero;
    }
    private void changeCanDash()
    {
        animator_.SetBool("CanDash", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            onDamaged(collision.transform.position);
        }
        if (collision.gameObject.CompareTag("Trampoline"))
        {
            if (rigid.velocity.y <= 0 && transform.position.y > collision.transform.position.y)
            {
                onJumpTrampoline();
            }
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            MaskDudeHitPoint++;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            MovingBunny movingBunny = collision.gameObject.GetComponent<MovingBunny>();
            movingBunny.onDamaged();
            MaskDudeHitPoint++;
        }
    }
    void onAttack(Transform enemy)
    {
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }
    void onJumpTrampoline()
    {
        rigid.velocity = Vector3.zero;
        rigid.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
        animator_.SetBool("MaskJump", true);
        animator_.SetBool("MaskFall", false);
        animator_.SetBool("MaskIsJump", true);
    }

    void onDamaged(Vector2 targetPos)
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        Invoke("offDamaged", 2);
    }
}
