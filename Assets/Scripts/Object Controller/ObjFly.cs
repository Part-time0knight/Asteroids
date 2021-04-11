using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjState))]
[RequireComponent(typeof(Rigidbody2D))]
public class ObjFly : MonoBehaviour
{
    private Rigidbody2D body;
    private ObjState state;
    private bool active = false;
    private float obj_spd = 0;
    private GameState game_state;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        state = GetComponent<ObjState>();
    }
    private void Start()
    {
        game_state = state.GetGameState();
    }

    void FixedUpdate()
    {
        if (active && !game_state.GetPause()) body.transform.Translate(0, obj_spd * Time.deltaTime, 0);
    }
    public void ActivateObj(float spd, float angl, float time)
    {
        transform.eulerAngles = new Vector3(0, 0, angl);
        obj_spd = spd;
        active = true;
        if (time > 0) state.DestroyObj(time);
        state.SetSpeed(spd);
    }
}
