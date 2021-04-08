using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjShoot))]
[RequireComponent(typeof(ObjState))]
[RequireComponent(typeof(ObjHyperJump))]
[RequireComponent(typeof(Animator))]

public class PlayerState : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet_start;
    [SerializeField] GameObject jump_effect;
    [SerializeField] float fire_spd = 1f;
    [SerializeField] float bullet_spd = 10f;
    [SerializeField] float bullet_life = 4f;
    [SerializeField] float ship_radius = 0.4f;

    private ObjShoot shoota;
    private ObjState state;
    private ObjHyperJump jump;
    private Animator anim;
    private bool fly;
    private float reload = 0;
    private void Awake()
    {
        shoota = GetComponent<ObjShoot>();
        state = GetComponent<ObjState>();
        anim = GetComponent<Animator>();
        jump = GetComponent<ObjHyperJump>();
    }
    private void Update()
    {
        //------------Shooting------------
        if (reload != 0) reload += Time.deltaTime;//процесс перезарядки
        if (fire_spd < reload) reload = 0f;//сброс перезарядки
        if (Input.GetMouseButton(0) && reload == 0f)
        {
            shoota.Shoot(bullet, bullet_start.transform.position, bullet_spd, transform.eulerAngles.z - 180, bullet_life);//выстрел
            reload += Time.deltaTime;//начало перезарядки
        }
        //------------animation-----------
        fly = state.GetFly();
        anim.SetBool("Fly", fly);
        //------------Gyper Jump----------
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 new_coord = jump.GetPoint(ship_radius);
            if (jump_effect != null) Instantiate(jump_effect, transform.position, Quaternion.identity);
            transform.parent.transform.position = new_coord;
            if (jump_effect != null) Instantiate(jump_effect, transform.position, Quaternion.identity);
        }
    }
}