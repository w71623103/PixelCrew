using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastWeapon : MonoBehaviour
{
    public Transform Firepoint;
    [SerializeField] private int damage = 10;
    public GameObject impactEffect;
    public LineRenderer lineRenderer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        RaycastHit2D target = Physics2D.Raycast(Firepoint.position, Firepoint.right);

        if (target)
        {
            Enemy enemy = target.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                lineRenderer.SetPosition(0, Firepoint.position);
                lineRenderer.SetPosition(0, target.point);
            }
        }
        else
        {
            lineRenderer.SetPosition(0, Firepoint.position);
            lineRenderer.SetPosition(1, Firepoint.position + Firepoint.right * 100);
        }
        //Some Problem Happened in LineRenderer
        lineRenderer.enabled = true;

        yield return 0;

        lineRenderer.enabled = false;
    }
}
