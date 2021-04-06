using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{
    [SerializeField] float turn_per_second = 1f;

    void Awake()
    {
        
    }
    void FixedUpdate()
    {
        transform.eulerAngles += new Vector3(0, 0, (360 * Time.deltaTime) * turn_per_second);
    }
}
