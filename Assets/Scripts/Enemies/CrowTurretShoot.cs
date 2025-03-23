using System;
using UnityEngine;

public class CrowTurretShoot : Enemy
{
    [SerializeField] private float radar = 5;
    [SerializeField] private float fireRate = 2.0f;

    private Transform playerTransform;
    private float timeSinceLastShot = 0;

    protected override void Start()
    {
        base.Start();

        if (fireRate < -0)
            fireRate = 2;

        if (radar <= 0)
            radar = 5;
    }

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerSpawned += OnPlayerSpawnedCallback;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerSpawned -= OnPlayerSpawnedCallback;
    }

    private void OnPlayerSpawnedCallback(PlayerController controller) => playerTransform = controller.transform;


    void Update()
    {
        if (!playerTransform) return;

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        sr.flipX = (transform.position.x > playerTransform.position.x);

        CheckDistance(Mathf.Abs(playerTransform.position.x - transform.position.x), curPlayingClips[0]);
    }

    void CheckDistance(float distance, AnimatorClipInfo curClip)
    {
        if (distance <= radar)
        {
            sr.color = Color.red;
            if (curClip.clip.name.Contains("Idle")) CheckFire();
        }
        else
            sr.color = Color.white;
    }

    void CheckFire()
    {
        if (Time.time >= timeSinceLastShot + fireRate)
        {
            anim.SetTrigger("Shoot");
            timeSinceLastShot = Time.time; 
        }
    }
}
