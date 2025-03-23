using UnityEngine;

public class EyePickup : MonoBehaviour, IPickup
{
    public AudioClip pickupSound;
    public int scoreToAdd;
    AudioSource audioSource;

    public void Pickup()
    {
        GameManager.Instance.score += scoreToAdd;
        audioSource.PlayOneShot(pickupSound);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 0.2f);
    }
}
