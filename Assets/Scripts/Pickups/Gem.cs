//this is the score pickup script, it was named gem because the gem sprite was the placeholder
// for the pickup item

using UnityEditor;
using UnityEngine;

public class Gem : MonoBehaviour, Pickup
{
    public int addScore;

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
        GameManager.Instance.Score++;
        Destroy(gameObject);
    }
}
