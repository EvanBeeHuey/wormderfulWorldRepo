using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class ProjectileBehaviour : MonoBehaviour
{
    public float projSpeed = 4.0f;
    public float lifespan = 3.0f;

    //public int direction = 1;
    private Rigidbody2D m_Rigidbody;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Rigidbody.AddForce(m_Rigidbody.transform.forward * projSpeed);
        Destroy(gameObject, lifespan);
    }

    //private void Update()
    //{
    //    transform.position += -transform.right * Time.deltaTime * projSpeed;
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
