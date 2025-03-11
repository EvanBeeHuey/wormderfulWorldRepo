using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class ProjectileBehaviour : MonoBehaviour
{
    public float lifespan = 3.0f;
    

    //public int direction = 1;
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
        Destroy(gameObject);
    }
}
