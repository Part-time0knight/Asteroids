using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Скрипт, содержащий основу игрового объекта
 * 
 */
public delegate void StateDestroy();//делегат функции, вызываемой перед уничтожением объекта
public class ObjState : MonoBehaviour
{
    private int hp = 1; //хит поинты объекта
    private int dmg = 1; //урон объекта
    private int score = 0; //ценность объекта в игровых очках
    private bool in_fly = false; //для контроля анимации
    //------таймер------
    private float delta_time = -1f;
    private float destroy_time = 0f;
    //------------------
    private float spd = 0; //скорость объекта
    StateDestroy SpecialDestroy = null;
    GameState game_state;
    private void Awake()
    {
        //--поиск игрового контроллера--
        game_state = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
    }
    /*
     * Функция определения характеристик объекта. Вызывается из специальных скриптов, 
     * определяющих уникальные св-ва(например PlayerState или CommonEnemyState)
     */
    public void InitObj(int new_hp, int new_damage, int new_score, StateDestroy func)
    {
        hp = new_hp;
        dmg = new_damage;
        score = new_score;
        SpecialDestroy = func;
    }

    //------Счетчик перед уничтожением------
    private void Update()
    {
        if (!game_state.GetPause() && delta_time >= 0f)
            delta_time += Time.deltaTime;
        if (destroy_time <= delta_time)
            DestroyObj(0);

    }
    //--передача ссылки на игровой контроллер--
    public GameState GetGameState()
    {
        return game_state;
    }
    //--методы обмена значениями игровых очков--
    public void SetScore(int new_score)
    {
        score = new_score;
    }
    public int GetScore()
    {
        return score;
    }
    //--методы управлением булевым значением полета--
    public void SetFly()
    {
        in_fly = true;
    }
    public void StopFly()
    {
        in_fly = false;
    }
    public bool GetFly()
    {
        return in_fly;
    }
    //--метод получения значения урона--
    public int GetDamage()
    {
        return dmg;
    }
    //--методы управления значением хит-поинтов--
    public int GetHP()
    {
        return hp;
    }
    public void SetHP(int new_hp)
    {
        hp = new_hp;
    }
    //--методы управления значением скорости--
    public float GetSpeed()
    {
        return spd;
    }
    public void SetSpeed(float new_spd)
    {
        spd = new_spd;
    }
    //-----метод уничтожения объекта-----
    public void DestroyObj(float time)
    {
        destroy_time = time;
        delta_time = 0f;
        if (time == 0)
        {
            if (SpecialDestroy != null)
            {
                SpecialDestroy();
            }
            Destroy(gameObject);
        }
    }
}
