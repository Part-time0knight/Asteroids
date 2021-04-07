using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjShoot))]

public class PlayerState : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet_start;
    [SerializeField] float fire_spd = 1f;
    [SerializeField] float bullet_spd = 10f;
    [SerializeField] float bullet_life = 4f;

    private float reload = 0;
    private ObjShoot shoota;
    private void Awake()
    {
        shoota = GetComponent<ObjShoot>();
    }
    private void Update()
    {
        if (reload != 0) reload += Time.deltaTime;//процесс перезарядки
        if (fire_spd < reload) reload = 0f;//сброс перезарядки
        if (Input.GetMouseButton(0) && reload == 0f)
        {
            shoota.Shoot(bullet, bullet_start.transform.position, bullet_spd, transform.eulerAngles.z - 180, bullet_life);//выстрел
            reload += Time.deltaTime;//начало перезарядки
        }
    }
}