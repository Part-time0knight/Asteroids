using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletState : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;
    private ObjState state;
    private void Awake()
    {
        state = GetComponent<ObjState>();
    }
    private void OnTriggerEnter2D(Collider2D wall)
    {
        if (wall.tag == "Wall") state.DestroyObj(delay);
    }
}
