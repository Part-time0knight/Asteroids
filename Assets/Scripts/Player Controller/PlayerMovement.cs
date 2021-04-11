using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float max_spd = 3.5f;
    [SerializeField] float accelerate_spd = 0.4f;

    private float current_spd_h = 0f;
    private float current_spd_v = 0f;
    private Rigidbody2D body;
    private ObjState state;
    private GameState game_state;
    private Vector2 freez_spd;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        state = GetComponentInChildren<ObjState>();
        freez_spd = new Vector2(0f, 0f);
    }
    private void Start()
    {
        game_state = state.GetGameState();
    }
    void FixedUpdate()
    {
        if (!game_state.GetPause())
        {
            float move_h = Input.GetAxis("Horizontal");
            float move_v = Input.GetAxis("Vertical");
            if (state != null)
                if (move_h != 0f || move_v != 0f)
                {
                    state.SetFly();
                }
                else
                    state.StopFly();
            if (freez_spd.x != 0f || freez_spd.y != 0f)
            {
                body.velocity = freez_spd;
                freez_spd *= 0;
            }
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
        else if (body.velocity.x != 0f || body.velocity.y != 0f)
        {
            freez_spd = body.velocity;
            body.velocity *= 0f;
        }

    }
}
