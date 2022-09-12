using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.transform.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
