using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField, Range(1, 20)] private float lifespan = 3.0f;
    [SerializeField, Range(1, 20)] private int damage = 1;

    private Rigidbody2D m_Rigidbody;

    private void Start()
    {
        Destroy(gameObject, lifespan);
    }

    public void SetVelocity(Vector2 velocity)
    {
        GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("pProjectile"))
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        if (gameObject.CompareTag("eProjectile") && collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.lives-=1;
            Debug.Log(GameManager.Instance.lives);
            Destroy(gameObject);
        }
    
    }
}
