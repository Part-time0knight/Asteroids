using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ObjState))]
[RequireComponent(typeof(ObjHyperJump))]
[RequireComponent(typeof(Animator))]

/*----------------------------------------------
 *���� ������ �������� �������������� ������� ������ � ��� ���������� ��������
 *
 ----------------------------------------------*/

public class PlayerState : MonoBehaviour
{
    [SerializeField] private GameObject bullet;//������ ����
    [SerializeField] private GameObject bullet_start;//������ ����� ������ ����
    [SerializeField] private GameObject jump_effect;//������ ������� ������������
    [SerializeField] private int hp = 1;
    [SerializeField] private int dmg = 1;
    [SerializeField] private float fire_spd = 1f;
    [SerializeField] private float bullet_spd = 10f;
    [SerializeField] private float bullet_life = 4f;
    [SerializeField] private float ship_radius = 0.4f;

    private Factory shoota;//������� ����
    private ObjState state;
    private ObjHyperJump jump;
    private Animator anim;
    private GameState game_state;
    private bool fly;
    private float reload = 0;
    private float anm_spd;
    private void Awake()
    {
        //------����������� ����������------
        shoota = ScriptableObject.CreateInstance<Factory>();
        state = GetComponent<ObjState>();
        state.InitObj(hp, dmg, 0, PreDestroy);
        anim = GetComponent<Animator>();
        jump = GetComponent<ObjHyperJump>();
        anm_spd = anim.speed;
    }
    private void Start()
    {
        //------����������� ����������2------
        game_state = state.GetGameState();
    }
    private void Update()
    {
        
        if (!game_state.GetPause())
        {
            //------------��������------------
            if (reload != 0f) reload += Time.deltaTime;
            if (fire_spd < reload) reload = 0f;
            if (Input.GetMouseButton(0) && reload == 0f)
            {
                shoota.CreateObject(bullet, bullet_start.transform.position, bullet_life, bullet_spd, transform.eulerAngles.z - 180);//�������
                reload += Time.deltaTime;
            }
            //------------�������� ��������-----------
            fly = state.GetFly();
            anim.SetBool("Fly", fly);
            if (anim.speed == 0f)
                anim.speed = anm_spd;
            //--------------����� ������--------------
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
            //---��������� �������� � ������ �����---
            if (anim.GetBool("Fly"))
            {
                anim.speed = 0f;
            }
        }
    }

    //---������� ���������� ����� ������������ �������---
    private void PreDestroy()
    {
        if (game_state)
            game_state.PlayerDead();
        Destroy(transform.parent.gameObject);
    }
}