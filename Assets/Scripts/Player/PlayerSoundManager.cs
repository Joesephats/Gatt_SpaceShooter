using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip laserSFX;

    private void Awake()
    {
    }

    public void PlayDeathSFX()
    {
        AudioSource.PlayClipAtPoint(deathSFX, transform.position);
    }

    public void PlayLaserSFX()
    {
        AudioSource.PlayClipAtPoint(laserSFX, transform.position);
    }
}
