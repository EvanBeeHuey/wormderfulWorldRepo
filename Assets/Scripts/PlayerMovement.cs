using TMPro;
using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections;
using UnityEditor;

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
    private BoxCollider2D bc;
    private GroundCheck gndChk;

    //worm projectile
    public ProjectileBehaviour ProjectilePrefab;
    public Transform LaunchOffset;

    //Audio clips
    private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gndChk = GetComponent<GroundCheck>();
        audioSource = GetComponent<AudioSource>();

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

        anim.SetBool("isGroundedAnim", isGrounded);
        anim.SetFloat("speed", Mathf.Abs(hInput));
        anim.SetBool("isFalling", rb.linearVelocity.y < 0.1f);

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            anim.SetTrigger("crouchActive");

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            anim.SetTrigger("Fire");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(jumpSound);
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

    public void ResetRigidbody() => rb.bodyType = RigidbodyType2D.Dynamic;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //detect a pickup
        Pickup pickup = collision.gameObject.GetComponent<Pickup>();
        if (pickup != null) pickup.Pickup(this);
    }
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        //detect a pickup
        Pickup pickup = collision.gameObject.GetComponent<Pickup>();
        if (pickup != null) pickup.Pickup(this);

        //jump damage to enemies
        if ((rb.linearVelocityY < 0) && collision.transform.CompareTag("Squish"))
        {
            collision.enabled = false;
            collision.gameObject.GetComponentInParent<Enemy>().TakeDamage(9999, DamageType.JumpedOn);
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}
