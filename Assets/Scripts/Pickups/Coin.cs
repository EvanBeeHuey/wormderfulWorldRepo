using UnityEditor;
using UnityEngine;

public class Coin : MonoBehaviour, Pickup
{
    public int addScore;

    public void Pickup(PlayerController player)
    {
        player.Score += addScore;
        Destroy(gameObject);
    }
}
