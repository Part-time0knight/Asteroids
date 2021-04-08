using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletState : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;
    private void OnTriggerEnter2D(Collider2D wall)
    {
        if (wall.tag == "Wall") Destroy(gameObject, delay);
    }
}
