using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
    [SerializeField] private GameObject _mBullet;
    [SerializeField] private GameObject _mGunBarrel;

    void Start () {
		
	}
	
	void Update () {
	    if(Input.GetButtonDown("Fire1"))
        {
            Instantiate(_mBullet, _mGunBarrel.transform.position, _mGunBarrel.transform.rotation);
        }
	}
}
