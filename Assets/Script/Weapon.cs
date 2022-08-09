using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform Firepoint;
    public GameObject ProjectilePrefab;
    public Transform target;
    public Vector2 startPos, endPos, midPos;
    public float normalizedSpeed;
    
    // Update is called once per frame
    void Update()
    {
        /*startPos = transform.position;
        if(enemies.Length > 0)
        {
            int id = Random.Range(0, enemies.Length);
            target = enemies[id].transform;
            endPos = transform.position;
        }
        else
        {
            endPos = startPos + new Vector2(Firepoint.right.x * 100 , 0);
        }*/
          
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
    }

    void Shoot()
    {
        //how to shoot
        Instantiate(ProjectilePrefab, Firepoint.position, Firepoint.rotation);
    }

    Vector2 GetMiddlePosition(Vector2 a, Vector2 b)
    {
        Vector2 m = Vector2.Lerp(a, b, .1f); // the third parameter decides the position of inflection point
        Vector2 normal = Vector2.Perpendicular(a - b).normalized;
        float rangeRatio = Random.Range(2f, -2f);
        float curveRatio = 0.3f;
        return m + (a - b).magnitude * curveRatio * rangeRatio * normal;
    }
}
