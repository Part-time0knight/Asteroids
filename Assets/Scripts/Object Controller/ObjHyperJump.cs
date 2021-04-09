using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjHyperJump : MonoBehaviour
{
    private float cam_h;
    private float cam_w;
    private void Awake()
    {
        Vector3 cam = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, Camera.main.nearClipPlane));
        cam_w = cam.x;
        cam_h = cam.y;
    }

    public Vector2 GetPoint(float radius)
    {
        float point1 = Random.Range(cam_w * -1f, cam_w);
        float point2 = Random.Range(cam_h * -1f, cam_h);
        Vector2 start1 = new Vector2(point1 - radius, point2 - radius);
        Vector2 start2 = new Vector2(point1 - radius, point2 + radius);
        Vector2 fin1 = new Vector2(point1 + radius, point2 + radius);
        Vector2 fin2 = new Vector2(point1 + radius, point2 - radius);
        Vector2 res = new Vector2(start1.x + radius, start1.y + radius);
        RaycastHit2D res_ray1 = Physics2D.Raycast(start1, fin1, radius * 2f, -1, -9f, 9f);
        RaycastHit2D res_ray2 = Physics2D.Raycast(start2, fin2, radius * 2f, -1, -9f, 9f);
        if (res_ray1 || res_ray2)
            res = GetPoint(radius);
        return res;
    }
}
