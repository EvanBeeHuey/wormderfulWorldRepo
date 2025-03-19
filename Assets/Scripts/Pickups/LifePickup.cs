using UnityEditor;
using UnityEngine;

public class Life : MonoBehaviour, Pickup
{
    public int addLife;

    public float bobSpeed = 2.0f;
    public float bobAmount = 0.2f;

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        transform.position = new Vector2(startPos.x, newY);
    }
    public void Pickup(PlayerController player)
    {
        if (GameManager.Instance.Life <= 4)
        {
            GameManager.Instance.Life++;
            Destroy(gameObject);
            return;
        }
        Destroy(gameObject);
    }
}