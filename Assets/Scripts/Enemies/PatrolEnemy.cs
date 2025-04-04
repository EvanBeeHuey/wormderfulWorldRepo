using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PatrolEnemy : Enemy
{
    Rigidbody2D rb;

    [SerializeField] private float xVel;
    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (xVel <= 0) xVel = 3;
    }

    public override void TakeDamage(int DamageValue, DamageType damageType = DamageType.Default)
    {
        if (damageType == DamageType.JumpedOn)
        {
            anim.SetTrigger("Death");
            Destroy(transform.parent.gameObject, 0.5f);
            return;
        }

        base.TakeDamage(DamageValue, damageType);
    }
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);
        if (curPlayingClips[0].clip.name.Contains("RatWalk") || curPlayingClips[0].clip.name.Contains("BirdFly"))
            rb.linearVelocity = (sr.flipX) ? new Vector2(-xVel, rb.linearVelocityY) : new Vector2(xVel, rb.linearVelocityY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Barrier"))
        {
            sr.flipX = !sr.flipX;
        }
    }
}
