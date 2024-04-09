using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip laserSFX;
    [SerializeField] AudioClip extraLifeSFX;
    [SerializeField] AudioClip doubleLasersSFX;

    private void Awake()
    {

    }

    public void PlayLaserSFX()
    {
        AudioSource.PlayClipAtPoint(laserSFX, transform.position);
    }
    public void PlayDoubleLasersSFX()
    {
        AudioSource.PlayClipAtPoint(doubleLasersSFX, transform.position);
    }
    public void PlayExtraLifeSFX()
    {
        AudioSource.PlayClipAtPoint(extraLifeSFX, transform.position);
    }
}
