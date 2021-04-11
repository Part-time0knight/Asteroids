using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotation : MonoBehaviour
{
    [SerializeField] float turn_per_second = 1f;
    private GameState game_state;
    private void Awake()
    {
        game_state = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
    }

    void FixedUpdate()
    {
        if (!game_state.GetPause())
            transform.eulerAngles += new Vector3(0, 0, (360 * Time.deltaTime) * turn_per_second);
    }
}
