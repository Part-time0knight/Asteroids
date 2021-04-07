using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    
    //[SerializeField] int border = 10; //% от кра€ сторон
    [SerializeField] GameObject asteroid;
    [SerializeField] float asteroid_delay = 1f;
    [SerializeField] float asteroid_spd_max = 3f;
    [SerializeField] float asteroid_spd_min = 1f;
    [SerializeField] GameObject common_enemy;
    [SerializeField] float common_enemy_delay = 5f;
    [SerializeField] float common_enemy_spd_max = 3f;
    [SerializeField] float common_enemy_spd_min = 1f;

    private float a_time;
    private float ce_time;
    private int camera_h;
    private int camera_w;
    void Awake()
    {
        a_time = asteroid_delay - 0.5f;
        ce_time = 0f;
        camera_h = Camera.main.pixelHeight;
        camera_w = Camera.main.pixelWidth;
    }

    void Update()
    {
        TimeUpdate();
        if (a_time == 0f)
        {
            GameObject asteroid = SpawnEnemy(Random.Range(0, 4), 0);//спавн астероида
            asteroid.GetComponent<AsteroidState>().AsteroidSize(Random.Range(0, 3));
        }
        if (ce_time == 0f)
        {
            GameObject asteroid = SpawnEnemy(Random.Range(0, 4), 1);//спавн обычного противника
            //asteroid.GetComponent<AsteroidState>().AsteroidSize(Random.Range(0, 3));
        }
    }
    private GameObject SpawnEnemy(int side, int type)
    {
        GameObject item = null;
        float angle = 0f;
        float spd = 0f;
        float spd_min = 0f;
        float spd_max = 0f;
        float out_camera = 40f;
        Vector3 start = new Vector3(0, 0, 0);

        if (type == 0) //определение объекта и его скорость
        {
            item = asteroid;
            spd_max = asteroid_spd_max;
            spd_min = asteroid_spd_min;
            spd = Random.Range(spd_min, spd_max);
        }
        else if (type == 1)
        {
            item = common_enemy;
            spd_max = common_enemy_spd_max;
            spd_min = common_enemy_spd_min;
            spd = Random.Range(spd_min, spd_max);
        }
        else
        {
            Debug.LogError("¬рага с таким номером не существует: " + type);
        }

        if (side == 0) // нижн€€ грань экрана
        {
            int place = Random.Range(0, camera_w);
            start.x = place;
            start.y = -out_camera;
            start.z = Camera.main.nearClipPlane;
            start = Camera.main.ScreenToWorldPoint(start);
            start.z = 0f;
            angle = GetAngleRand(place, camera_w);
            
        }
        else if (side == 1) // верхн€€ грань экрана
        {
            int place = Random.Range(0, camera_w);
            start.x = place;
            start.y = camera_h + out_camera;
            start.z = Camera.main.nearClipPlane;
            start = Camera.main.ScreenToWorldPoint(start);
            start.z = 0f;
            angle = (GetAngleRand(place, camera_w) - 180f) * -1f;
        }
        else if (side == 2) // лева€ грань экрана
        {
            int place = Random.Range(0, camera_h);
            start.x = -out_camera;
            start.y = place;
            start.z = Camera.main.nearClipPlane;
            start = Camera.main.ScreenToWorldPoint(start);
            start.z = 0f;
            angle = -90 - GetAngleRand(place, camera_h);
        }
        else if (side == 3) // права€ грань экрана
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
            Debug.LogError("—тороны с таким номером не существует: " + side);
        }
        item = Instantiate(item, start, Quaternion.identity);
        item.GetComponent<ObjFly>().ActivateObj(spd, angle, Camera.main.ScreenToWorldPoint(new Vector3(camera_w, 0, 0)).x / spd * 2.5f);
        return item;
    }

    private float GetAngleRand( int point, int length)
    {
        float res = -45f;
        float range = 30;
        float discrete_unit = 90f / length;
        res += point * discrete_unit;
        res = Random.Range(res - range, res + range);
        return res;
    }
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
