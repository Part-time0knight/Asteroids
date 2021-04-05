using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float max_spd = 3.5f;
    [SerializeField] float accelerate_spd = 0.4f;

    private float current_spd_h = 0f;
    private float current_spd_v = 0f;
    private Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        float move_h = Input.GetAxis("Horizontal");
        float move_v = Input.GetAxis("Vertical");

        if (move_h != 0f)
        {
            current_spd_h += (Mathf.Sign(move_h) * accelerate_spd);
            body.velocity += new Vector2(current_spd_h * Time.deltaTime, 0);
        }
        else current_spd_h = body.velocity.x;

        if (move_v != 0f)
        {
            current_spd_v += Mathf.Sign(move_v) * accelerate_spd;
            body.velocity += new Vector2(0, current_spd_v * Time.deltaTime);
        }
        else current_spd_v = body.velocity.y;
        current_spd_h = Mathf.Clamp(current_spd_h, -max_spd, max_spd);
        current_spd_v = Mathf.Clamp(current_spd_v, -max_spd, max_spd);

    }
}
