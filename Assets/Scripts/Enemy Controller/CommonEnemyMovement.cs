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
    private void Awake()
    {
        Invoke("Change—ourse", activate_time);
    }
    private void Change—ourse()
    {
        float side_y = Mathf.Sign(transform.position.y) * -1f;
        float side_x = Mathf.Sign(180f - transform.eulerAngles.z);
        if (side_y < 0f)
            transform.eulerAngles = new Vector3(0f, 0f, ((180f - angle) * side_x));
        else
            transform.eulerAngles = new Vector3(0f, 0f, (angle * side_x));
        Invoke("Line—ourse", change_time);
    }
    private void Line—ourse()
    {
        float side_x = Mathf.Sign(180f - transform.eulerAngles.z);
        transform.eulerAngles = new Vector3(0f, 0f, 90f * side_x);
    }
}
