using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsFly : MonoBehaviour
{
    [SerializeField] float b_spd = 10;
    [SerializeField] float life_time = 4f;

    private Rigidbody2D body;
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        Destroy(gameObject, life_time);
    }
    void FixedUpdate()
    {
        body.transform.Translate(0, b_spd * Time.deltaTime, 0);
    }

}
