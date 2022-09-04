using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform Firepoint;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private GameObject ChargeBullet;

    public void PullTrigger()
    {
        Debug.Log("Pulled Trigger");
    }
    
    public void ReleaseTrigger()
    {
        var bumper = (GameObject)Instantiate(Bullet, Firepoint.position, Firepoint.rotation);
        Destroy(bumper, 5f);
    }
}
