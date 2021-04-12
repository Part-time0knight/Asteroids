using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ������ �� �������� ����������� ����. ������� �������� �������� �����������
 */

public class Spawner : MonoBehaviour
{
    
    [SerializeField] GameObject asteroid;//������ ���������
    [SerializeField] float asteroid_delay = 2f;//����� ��������� ������ asteroid_delay �������
    [SerializeField] float asteroid_spd_max = 3f;
    [SerializeField] float asteroid_spd_min = 1f;
    [SerializeField] GameObject common_enemy;//������ �������� �����
    [SerializeField] float common_enemy_delay = 5f;//����� ����� ������ common_enemy_delay �������
    [SerializeField] float common_enemy_spd_max = 3f;
    [SerializeField] float common_enemy_spd_min = 1f;
    //---------�������---------
    private float a_time;
    private float ce_time;
    private float max_asteroid_delay;
    private float max_Common_enemy_delay;
    //-------------------------
    //----------������---------
    private int camera_h;
    private int camera_w;
    //-------------------------
    private float score;//������� ����
    private Factory creator;//������� ������
    private GameState game_state;


    void Awake()
    {
        //----��������� ��������---
        a_time = asteroid_delay - 0.5f;
        ce_time = 0f;
        max_Common_enemy_delay = common_enemy_delay;
        max_asteroid_delay = asteroid_delay;
        //-------------------------
        //----��������� ������-----
        camera_h = Camera.main.pixelHeight;
        camera_w = Camera.main.pixelWidth;
        //-------------------------
        //-----������-----
        creator = ScriptableObject.CreateInstance<Factory>();
        game_state = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();

    }

    void Update()
    {
        if (!game_state.GetPause())
        {
            TimeUpdate();//�������
            //---���������� ��������� ���� �� ���� ����� ������� �����---
            score = 1f + game_state.GetScore() / 1000f;
            asteroid_delay = max_asteroid_delay / 2 + asteroid_delay / (2 * score);
            common_enemy_delay = max_Common_enemy_delay / 2 + common_enemy_delay / (2 * score);
            //-----------------------------------------------------------
            //------����� ������ ����������� �����-------
            if (a_time == 0f)
            {
                SpawnEnemy(Random.Range(0, 4), asteroid);//����� ���������
            }
            if (ce_time == 0f)
            {
                SpawnEnemy(Random.Range(0, 4), common_enemy);//����� �������� ����������
            }
        }
    }
    //---����� ������ �������. ��� �������� ������ ��������� ������ ��� �������---
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

        if (type == asteroid) //����������� ������� � ��� ��������
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
            Debug.LogError("����� � ����� �������� �� ����������: " + type);
        }
        life_time = Camera.main.ScreenToWorldPoint(new Vector3(hypotenuse, 0, 0)).x / spd * 2f; //����������� ������� ����� �������
        if (side == 0) // ����� ��� ������ ����� ������
        {
            int place = Random.Range(0, camera_w);
            start.x = place;
            start.y = -out_camera;
            start.z = Camera.main.nearClipPlane;
            start = Camera.main.ScreenToWorldPoint(start);
            start.z = 0f;
            angle = GetAngleRand(place, camera_w);
            
        }
        else if (side == 1) // ����� ��� ������� ����� ������
        {
            int place = Random.Range(0, camera_w);
            start.x = place;
            start.y = camera_h + out_camera;
            start.z = Camera.main.nearClipPlane;
            start = Camera.main.ScreenToWorldPoint(start);
            start.z = 0f;
            angle = (GetAngleRand(place, camera_w) - 180f) * -1f;
        }
        else if (side == 2) // ����� ��� ����� ����� ������
        {
            int place = Random.Range(0, camera_h);
            start.x = -out_camera;
            start.y = place;
            start.z = Camera.main.nearClipPlane;
            start = Camera.main.ScreenToWorldPoint(start);
            start.z = 0f;
            angle = -90 - GetAngleRand(place, camera_h);
        }
        else if (side == 3) // ����� ��� ������ ����� ������
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
            Debug.LogError("������� � ����� ������� �� ����������: " + side);
        }
        item = creator.CreateObject(type, start, life_time, spd, angle);//����� �������
        return item;
    }
    //---����� ����������� ���� ��� ������---
    private float GetAngleRand( int point, int length)
    {
        float res = -45f;
        float range = 30;
        float discrete_unit = 90f / length;
        res += point * discrete_unit;
        res = Random.Range(res - range, res + range);
        return res;
    }
    //---�������, ���� �� ��� ���---
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
