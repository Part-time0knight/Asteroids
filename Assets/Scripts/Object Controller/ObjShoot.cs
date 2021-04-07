using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjShoot : MonoBehaviour
{
    public void Shoot(GameObject bullet, Vector3 bullet_start, float bullet_spd, float angle, float bullet_life)
    {
        GameObject temp_bullet = Instantiate(bullet, bullet_start, Quaternion.identity);
        temp_bullet.GetComponent<ObjFly>().ActivateObj(bullet_spd, angle, bullet_life);
    }
}
