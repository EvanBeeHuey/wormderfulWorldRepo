using UnityEngine;

public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;
    AudioSource audioSource;

    [SerializeField] private Vector2 initShotVelocity = Vector2.zero;

    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private Transform spawnPointLeft;

    [SerializeField] private ProjectileBehaviour projectilePrefab;
    [SerializeField] private AudioClip playerProj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (initShotVelocity == Vector2.zero)
        {
            Debug.Log("Init Shot Velocity has been changed to a default value. ShootProjectile script line 21");
            initShotVelocity.x = 7.0f;
        }

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
        {
            Debug.Log($"Please set default values on {gameObject.name}. ShootProjectile script line 27");
        }
    }
    public void Fire()
    {
        ProjectileBehaviour curProjectile;
        if (!sr.flipX)
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.SetVelocity(initShotVelocity);
        }
        else
        {
            curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.SetVelocity(new Vector2(-initShotVelocity.x, initShotVelocity.y));
            SpriteRenderer projFlip = curProjectile.GetComponent<SpriteRenderer>();
            projFlip.flipX = true;
        }

        audioSource.PlayOneShot(playerProj);
    }
}
