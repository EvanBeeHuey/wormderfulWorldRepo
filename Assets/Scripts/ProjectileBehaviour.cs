using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float projSpeed = 4.0f;
    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * projSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
