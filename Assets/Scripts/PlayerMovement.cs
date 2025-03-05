using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))] //can only put 3 typeof in one line, start another line of RequireComponent if there is 4+
[RequireComponent(typeof(GroundCheck))]
public class PlayerController : MonoBehaviour
{
    //movement variables
    [Range(1, 10)]
    public float speed = 2.0f;
    public float jumpForce = 7.0f;

    public bool isGrounded = false;

    public Vector2 boxColliderOffset;
    public Vector2 boxColliderFlippedOffset;

    //component references
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    //private BoxCollider2D bc;
    private GroundCheck gndChk;

    //worm projectile
    public ProjectileBehaviour ProjectilePrefab;
    public Transform LaunchOffset;

    //score
    private int _score = 0;
    public int Score
    {
        get => _score;
        set => _score = value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //bc = GetComponent<BoxCollider2D>();
        gndChk = GetComponent<GroundCheck>();

        //boxColliderOffset = bc.offset;
        //boxColliderFlippedOffset = new Vector2(-boxColliderOffset.x, boxColliderOffset.y);

        if (jumpForce < 0) jumpForce = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGrounded();

        float hInput = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector3(hInput * speed, rb.linearVelocity.y, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        //sprite flipping
        if (hInput != 0) sr.flipX = (hInput < 0);
        //if (hInput > 0 && sr.flipX || hInput < 0 && !sr.flipX) sr.flipX = !sr.flipX; //most efficient way to write this, but more verbose

        //bc.offset = (sr.flipX) ? boxColliderFlippedOffset : boxColliderOffset;

        anim.SetBool("isGroundedAnim", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(hInput));
        anim.SetBool("isFalling", rb.linearVelocity.y < 0.1f);

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            anim.SetTrigger("crouchActive");

        //worm projectile
        //if (Input.GetKeyDown(KeyCode.LeftAlt))
        //{
        //    Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        //}

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            ProjectileBehaviour newProjectile = Instantiate(ProjectilePrefab, LaunchOffset.position, LaunchOffset.rotation);
            //newProjectile.direction = sr.flipX ? -1 : 1; // -1 if flipped (left), 1 if not flipped (right) }
        }

        void CheckIsGrounded()
        {
            if (!isGrounded)
            {
                if (rb.linearVelocity.y <= 0.01) isGrounded = gndChk.isGrounded();
            }
            else isGrounded = gndChk.isGrounded();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Pickup pickup = collision.gameObject.GetComponent<Pickup>();
        if (pickup != null) pickup.Pickup(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pickup pickup = collision.GetComponent<Pickup>();
        if (pickup != null) pickup.Pickup(this);
    }
}
