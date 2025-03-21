using UnityEngine;

public class EyePickup : MonoBehaviour, IPickup
{
    public int scoreToAdd;

    public void Pickup()
    {
        GameManager.Instance.score += scoreToAdd;
        Destroy(gameObject);
    }
}
