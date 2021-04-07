using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemyState : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet_ang;
    [SerializeField] GameObject bullet_start;
    [SerializeField] float fire_spd = 1f;
    [SerializeField] float bullet_spd = 10f;
    [SerializeField] float bullet_life = 4f;

    private float reload = 0f;
    private ObjShoot shoota;
    private void Awake()
    {
        shoota = GetComponent<ObjShoot>();
    }
    private void Update()
    {
        if (fire_spd < reload) reload = 0f;//сброс перезарядки
        if (reload == 0f)
        {
            bullet_ang.transform.eulerAngles = new Vector3(0, 0, Random.Range(-180f, 180f));
            shoota.Shoot(bullet, bullet_start.transform.position, bullet_spd, bullet_ang.transform.eulerAngles.z, bullet_life);//выстрел
        }
        reload += Time.deltaTime;//процесс перезарядки
    }
}
