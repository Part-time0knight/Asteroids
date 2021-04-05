using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float max_spd = 5f;
    [SerializeField] float accelerate_spd = 0.1f;
    [SerializeField] float friction = 1.01f;

    private float current_spd_h = 0f;
    private float current_spd_v = 0f;

    void Update()
    {
        float move_h = Input.GetAxis("Horizontal");
        float move_v = Input.GetAxis("Vertical");

        if (move_h != 0f) current_spd_h += (Mathf.Sign(move_h) * accelerate_spd);
        else if (current_spd_h != 0f) current_spd_h /= friction;

        if (move_v != 0f) current_spd_v += Mathf.Sign(move_v) * accelerate_spd;
        else if (current_spd_v != 0f) current_spd_v /= friction;
        
        current_spd_h = Mathf.Clamp(current_spd_h, -max_spd, max_spd);
        current_spd_v = Mathf.Clamp(current_spd_v, -max_spd, max_spd);

        transform.position += new Vector3 (current_spd_h * Time.deltaTime, current_spd_v * Time.deltaTime, 0);
    }
}
