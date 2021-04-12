using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Машина по созданию смертельных штук. Главный помошник игрового контроллера
 */

public class Spawner : MonoBehaviour
{
    
    [SerializeField] GameObject asteroid;//префаб астероида
    [SerializeField] float asteroid_delay = 2f;//спавн астероида каждые asteroid_delay секунды
    [SerializeField] float asteroid_spd_max = 3f;
    [SerializeField] float asteroid_spd_min = 1f;
    [SerializeField] GameObject common_enemy;//префаб обычного врага
    [SerializeField] float common_enemy_delay = 5f;//спавн врага каждые common_enemy_delay секунды
    [SerializeField] float common_enemy_spd_max = 3f;
    [SerializeField] float common_enemy_spd_min = 1f;
    //---------Таймеры---------
    private float a_time;
    private float ce_time;
    private float max_asteroid_delay;
    private float max_Common_enemy_delay;
    //-------------------------
    //----------камера---------
    private int camera_h;
    private int camera_w;
    //-------------------------
    private float score;//игровые очки
    private Factory creator;//фабрика смерти
    private GameState game_state;


    void Awake()
    {
        //----инициация таймеров---
        a_time = asteroid_delay - 0.5f;
        ce_time = 0f;
        max_Common_enemy_delay = common_enemy_delay;
        max_asteroid_delay = asteroid_delay;
        //-------------------------
        //----инициация камеры-----
        camera_h = Camera.main.pixelHeight;
        camera_w = Camera.main.pixelWidth;
        //-------------------------
        //-----Ссылки-----
        creator = ScriptableObject.CreateInstance<Factory>();
        game_state = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();

    }

    void Update()
    {
        if (!game_state.GetPause())
        {
            TimeUpdate();//таймеры
            //---увеличение сложности игры за счет роста игровых очков---
            score = 1f + game_state.GetScore() / 1000f;
            asteroid_delay = max_asteroid_delay / 2 + asteroid_delay / (2 * score);
            common_enemy_delay = max_Common_enemy_delay / 2 + common_enemy_delay / (2 * score);
            //-----------------------------------------------------------
            //------спавн всякой смертельной штуки-------
            if (a_time == 0f)
            {
                SpawnEnemy(Random.Range(0, 4), asteroid);//спавн астероида
            }
            if (ce_time == 0f)
            {
                SpawnEnemy(Random.Range(0, 4), common_enemy);//спавн обычного противника
            }
        }
    }
    //---Метод спавна объекта. Его основная задача вычеслить данные для фабрики---
    private GameObject SpawnEnemy(int side, GameObject type)
    {
        GameObject item = null;
        float angle = 0f;
        float spd = 0f;
        float spd_min = 0f;
        float spd_max = 0f;
        float out_camera = 40f;
        float hypotenuse = Mathf.Sqrt(camera_w * camera_w + camera_h * camera_h);
        float life_time = 1f;
        Vector3 start = new Vector3(0, 0, 0);

        if (type == asteroid) //определение объекта и его скорость
        {
            spd_max = asteroid_spd_max;
            spd_min = asteroid_spd_min;
            spd = Random.Range(spd_min, spd_max);
        }
        else if (type == common_enemy)
        {
            spd_max = common_enemy_spd_max;
            spd_min = common_enemy_spd_min;
            spd = Random.Range(spd_min, spd_max);
        }
        else
        {
            Debug.LogError("Врага с таким индексом не существует: " + type);
        }
        life_time = Camera.main.ScreenToWorldPoint(new Vector3(hypotenuse, 0, 0)).x / spd * 2f; //определение времени жизни объекта
        if (side == 0) // спавн для нижней грани экрана
        {
            int place = Random.Range(0, camera_w);
            start.x = place;
            start.y = -out_camera;
            start.z = Camera.main.nearClipPlane;
            start = Camera.main.ScreenToWorldPoint(start);
            start.z = 0f;
            angle = GetAngleRand(place, camera_w);
            
        }
        else if (side == 1) // спавн для верхней грани экрана
        {
            int place = Random.Range(0, camera_w);
            start.x = place;
            start.y = camera_h + out_camera;
            start.z = Camera.main.nearClipPlane;
            start = Camera.main.ScreenToWorldPoint(start);
            start.z = 0f;
            angle = (GetAngleRand(place, camera_w) - 180f) * -1f;
        }
        else if (side == 2) // спавн для левой грани экрана
        {
            int place = Random.Range(0, camera_h);
            start.x = -out_camera;
            start.y = place;
            start.z = Camera.main.nearClipPlane;
            start = Camera.main.ScreenToWorldPoint(start);
            start.z = 0f;
            angle = -90 - GetAngleRand(place, camera_h);
        }
        else if (side == 3) // спавн для правой грани экрана
        {
            int place = Random.Range(0, camera_h);
            start.x = camera_w + out_camera;
            start.y = place;
            start.z = Camera.main.nearClipPlane;
            start = Camera.main.ScreenToWorldPoint(start);
            start.z = 0f;
            angle = 90 + GetAngleRand(place, camera_h);
        }
        else
        {
            Debug.LogError("Стороны с таким номером не существует: " + side);
        }
        item = creator.CreateObject(type, start, life_time, spd, angle);//вызов фабрики
        return item;
    }
    //---Метод определения угла для спавна---
    private float GetAngleRand( int point, int length)
    {
        float res = -45f;
        float range = 30;
        float discrete_unit = 90f / length;
        res += point * discrete_unit;
        res = Random.Range(res - range, res + range);
        return res;
    }
    //---Таймеры, куда же без них---
    private void TimeUpdate()
    {
        a_time += Time.deltaTime;
        ce_time += Time.deltaTime;
        if (a_time >= asteroid_delay)
            a_time = 0f;
        if (ce_time >= common_enemy_delay)
            ce_time = 0f;
    }
}
