using UnityEngine;

public class LifePickup : MonoBehaviour, IPickup
{
    public AudioClip pickupS;
    public int lifeToAdd;
    AudioSource audioSource;

    public void Pickup()
    {
        GameManager.Instance.lives += lifeToAdd;
        audioSource.PlayOneShot(pickupS);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 0.2f);
    }
}
