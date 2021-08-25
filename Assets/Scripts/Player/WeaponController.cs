using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponController : MonoBehaviour {
    [SerializeField] private GameObject mBullet;
    [SerializeField] private GameObject mGunBarrel;
    [SerializeField] private AudioClip gunShotSound;

    private void Update ()
    {
        if (!Input.GetButtonDown("Fire1")) return;
        Instantiate(mBullet, mGunBarrel.transform.position, mGunBarrel.transform.rotation);
        AudioController.Instance.PlayOneShot(gunShotSound);
    }
}
