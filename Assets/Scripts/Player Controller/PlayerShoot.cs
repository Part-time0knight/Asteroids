using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] float fire_spd = 1f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet_start;

    private float reload = 0;
    private void Update()
    {
        if (reload != 0) reload += Time.deltaTime;//процесс перезарядки
        if (fire_spd < reload) reload = 0f;//сброс перезарядки
        if ( Input.GetMouseButton(0) && reload == 0f )
        {
            Shoot();//выстрел
            reload += Time.deltaTime;//начало перезарядки
        }
    }

    private void Shoot()
    {
        GameObject temp_bullet = Instantiate(bullet, bullet_start.transform.position, Quaternion.identity);
        temp_bullet.transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, -180);

    }

}
