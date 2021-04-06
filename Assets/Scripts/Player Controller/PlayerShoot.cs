using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet_start;
    [SerializeField] float fire_spd = 1f;
    [SerializeField] float bullet_spd = 10f;
    [SerializeField] float bullet_life = 4f;

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
        temp_bullet.GetComponent<ObjectFly>().ActivateObj(bullet_spd, transform.eulerAngles.z -180, bullet_life);
        temp_bullet.transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, -180);

    }

}
