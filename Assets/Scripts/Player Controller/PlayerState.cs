using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    private Factory shoota;
    private ObjState state;
    private ObjHyperJump jump;
    private Animator anim;
    private GameState game_state;
    private bool fly;
    private float reload = 0;
    private float anm_spd;
    private void Awake()
    {
        shoota = ScriptableObject.CreateInstance<Factory>();
        state = GetComponent<ObjState>();
        anim = GetComponent<Animator>();
        jump = GetComponent<ObjHyperJump>();
        game_state = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
        anm_spd = anim.speed;
    }
    private void Update()
    {
        //------------Shooting------------
        if (!game_state.GetPause())
        {
            if (reload != 0f) reload += Time.deltaTime;//процесс перезарядки
            if (fire_spd < reload) reload = 0f;//сброс перезарядки
            if (Input.GetMouseButton(0) && reload == 0f)
            {
                shoota.CreateObject(bullet, bullet_start.transform.position, bullet_life, bullet_spd, transform.eulerAngles.z - 180);//выстрел
                reload += Time.deltaTime;
            }

            //------------animation-----------
            fly = state.GetFly();
            anim.SetBool("Fly", fly);
            if (anim.speed == 0f)
                anim.speed = anm_spd;
            //------------Gyper Jump----------
            if (Input.GetButtonDown("Jump"))
            {
                Vector2 new_coord = jump.GetPoint(ship_radius);
                if (jump_effect != null) Instantiate(jump_effect, transform.position, Quaternion.identity);
                transform.parent.transform.position = new_coord;
                if (jump_effect != null) Instantiate(jump_effect, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (anim.GetBool("Fly"))
            {
                anim.speed = 0f;
            }
        }
    }

    private void OnDestroy()
    {
        if (game_state)
            game_state.PlayerDead();
        Destroy(transform.parent.gameObject);
    }
}