using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Audio;

public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Life,
        Powerup,
        Score
    }

    public PickupType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case PickupType.Life:
                    GameManager.Instance.lives++;
                    break;
                //powerup pickup won't be a speed change, if i can help it. it'll be shrinking
                //case PickupType.Powerup:
                //GameManager.Instance.PlayerInstance.SpeedChange();
                //break;
                case PickupType.Score:
                    GameManager.Instance.score++;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
