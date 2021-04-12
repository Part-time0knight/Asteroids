using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjState))]
/*
 * Свойство пули - умирать
 */
public class BulletState : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;
    [SerializeField] GameObject sound_effect = null;
    private ObjState state;
    private void Awake()
    {
        state = GetComponent<ObjState>();
        if (sound_effect)
            Instantiate(sound_effect, transform.position, Quaternion.identity);
    }
    //---уничтожение пули при столкновении со стеной---
    private void OnTriggerEnter2D(Collider2D wall)
    {
        if (wall.tag == "Wall") state.DestroyObj(delay);
    }
}
