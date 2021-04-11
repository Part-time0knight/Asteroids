using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemyState : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet_ang;
    [SerializeField] GameObject bullet_start;
    [SerializeField] int score = 20;
    [SerializeField] int hp = 1;
    [SerializeField] int damage = 1;
    [SerializeField] float fire_spd = 1f;
    [SerializeField] float bullet_spd = 10f;
    [SerializeField] float bullet_life = 4f;

    private ObjState state;
    private float reload = 0f;
    private float anim_spd;
    private Factory shoota;
    private GameState game_state;
    private Animator enemy_anim;

    private void Awake()
    {
        shoota = ScriptableObject.CreateInstance<Factory>();
        state = GetComponent<ObjState>();
        state.InitObj(hp, damage, score, null);
        game_state = state.GetGameState();
        enemy_anim = GetComponent<Animator>();
        anim_spd = enemy_anim.speed;
    }
    private void Update()
    {
        if (!game_state.GetPause())
        {
            if (fire_spd < reload) reload = 0f;//сброс перезарядки
            if (reload == 0f)
            {
                bullet_ang.transform.eulerAngles = new Vector3(0, 0, Random.Range(-180f, 180f));
                shoota.CreateObject(bullet, bullet_start.transform.position, bullet_life, bullet_spd, bullet_ang.transform.eulerAngles.z);//выстрел
            }
            reload += Time.deltaTime;//процесс перезарядки
            if (enemy_anim.speed == 0f)
            {
                enemy_anim.speed = anim_spd;
            }
        }
        else if (enemy_anim.speed != 0f)
        {
            enemy_anim.speed = 0f;
        }
        
    }
}
