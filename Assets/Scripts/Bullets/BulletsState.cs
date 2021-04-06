using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsState : MonoBehaviour
{
    private int damage = 1;
    void OnTriggerEnter2D(Collider2D targed)
    {
        if (targed.tag == "Enemy" || targed.tag == "Player" || targed.tag == "Bullet")
        {
            targed.GetComponent<ObjState>().DealDamage(damage);
            Destroy(gameObject);
        }
    }
}
