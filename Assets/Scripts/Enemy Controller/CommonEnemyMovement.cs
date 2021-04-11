using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjState))]
public class CommonEnemyMovement : MonoBehaviour
{
    [SerializeField] private float activate_time = 2f;
    [SerializeField] private float change_time = 2f;
    [SerializeField] private float angle = 30f;
    private ObjState state;
    private float delta_time1 = 0f;
    private float delta_time2 = 0f;
    private GameState game_state;
    private void Awake()
    {
        game_state = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
    }
    private void Update()
    {
        if (!game_state.GetPause())
        {
            if (delta_time1 < activate_time && delta_time1 >= 0f)
                delta_time1 += Time.deltaTime;
            else if (delta_time2 < activate_time && delta_time2 >= 0f)
                delta_time2 += Time.deltaTime;

            if (delta_time1 >= activate_time)
            {
                Change—ourse();
                delta_time1 = -1f;
            }

            if (delta_time2 >= change_time)
            {
                Line—ourse();
                delta_time2 = -1f;
            }
        }
    }
    private void Change—ourse()
    {
        float side_y = Mathf.Sign(transform.position.y) * -1f;
        float side_x = Mathf.Sign(180f - transform.eulerAngles.z);
        if (side_y < 0f)
            transform.eulerAngles = new Vector3(0f, 0f, ((180f - angle) * side_x));
        else
            transform.eulerAngles = new Vector3(0f, 0f, (angle * side_x));
    }
    private void Line—ourse()
    {
        float side_x = Mathf.Sign(180f - transform.eulerAngles.z);
        transform.eulerAngles = new Vector3(0f, 0f, 90f * side_x);
    }
}
