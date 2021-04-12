using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjState))]
[RequireComponent(typeof(Animator))]
/*
 * ”никальные свойства обычного противника
 * 
 */
public class CommonEnemyState : MonoBehaviour
{
    [SerializeField] private GameObject bullet;//префаб пули
    [SerializeField] private GameObject bullet_start;//объект места спавна пули
    [SerializeField] private GameObject bullet_ang;//объект, определ€ющий угол стрельбы
    [SerializeField] private int score = 20;//стоимость
    [SerializeField] private int hp = 1;//хит-поинты
    [SerializeField] private int damage = 1;//урон
    [SerializeField] private float fire_spd = 1f;//скорость стрельбы
    [SerializeField] private float bullet_spd = 10f;//скорость полеь
    [SerializeField] private float bullet_life = 4f;//врем€ жизни пули
    private float reload = 0f;//таймер
    private float anim_spd;
    private ObjState state;
    private Factory shoota;//фабрика пуль
    private GameState game_state;
    private Animator enemy_anim;//анимаци€ вражины

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
            if (fire_spd < reload) reload = 0f;//сброс перезар€дки
            if (reload == 0f)//процесс стрельбы
            {
                bullet_ang.transform.eulerAngles = new Vector3(0, 0, Random.Range(-180f, 180f));
                shoota.CreateObject(bullet, bullet_start.transform.position, bullet_life, bullet_spd, bullet_ang.transform.eulerAngles.z);//выстрел
            }
            reload += Time.deltaTime;//процесс перезар€дки
            if (enemy_anim.speed == 0f)
            {
                enemy_anim.speed = anim_spd;//востановление скорости анимации
            }
        }
        else if (enemy_anim.speed != 0f)
        {
            enemy_anim.speed = 0f;//остановка анимации во врем€ паузы
        }
        
    }
}
