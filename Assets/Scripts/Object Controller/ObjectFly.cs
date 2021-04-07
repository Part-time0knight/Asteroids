using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjState))]
[RequireComponent(typeof(Rigidbody2D))]
public class ObjectFly : MonoBehaviour
{
    private Rigidbody2D body;
    private ObjState state;
    private bool active = false;
    private float obj_spd = 0;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        state = GetComponent<ObjState>();
    }

    void FixedUpdate()
    {
        if (active) body.transform.Translate(0, obj_spd * Time.deltaTime, 0);
    }
    public void ActivateObj(float spd, float angl, float time)
    {
        transform.eulerAngles = new Vector3(0, 0, angl);
        obj_spd = spd;
        active = true;
        if (time > 0) Destroy(gameObject, time);
        state.SetSpeed(spd);
    }
}
