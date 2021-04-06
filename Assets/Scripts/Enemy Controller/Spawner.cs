using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    
    //[SerializeField] int border = 10; //% �� ���� ������
    [SerializeField] GameObject asteroid;
    [SerializeField] float asteroid_delay = 1f;
    [SerializeField] float asteroid_spd_max = 5f;
    [SerializeField] float asteroid_spd_min = 1f;

    private float a_time;
    private int camera_h;
    private int camera_w;
    void Awake()
    {
        a_time = asteroid_delay - 0.5f;
        camera_h = Camera.main.pixelHeight;
        camera_w = Camera.main.pixelWidth;
    }

    void Update()
    {
        TimeUpdate();
        if (a_time == 0f)
        {
            int side = Random.Range(0, 3);
            
            SpawnEnemy(side, 0);//����� ���������
        }
    }
    private void SpawnEnemy(int side, int type)
    {
        GameObject item = null;
        float angle = 0f;
        float spd = 0f;
        float spd_min = 0f;
        float spd_max = 0f;
        float out_camera = 40f;
        Vector3 start = new Vector3(0, 0, 0);

        if (type == 0) //����������� ������� � ��� ��������
        {
            item = asteroid;
            spd_max = asteroid_spd_max;
            spd_min = asteroid_spd_min;
            spd = Random.Range(spd_min, spd_max);
        }
        else if (type == 1)
        {
            Debug.Log("common enemy ship");
        }
        else
        {
            Debug.LogError("����� � ����� ������� �� ����������: " + type);
        }

        if (side == 0) // ������ ����� ������
        {
            int place = Random.Range(0, camera_w);
            start.x = place;
            start.y = -out_camera;
            start.z = Camera.main.nearClipPlane;
            start = Camera.main.ScreenToWorldPoint(start);
            start.z = 0f;
            angle = GetAngleRand(place, camera_w);
            
        }
        else if (side == 1) // ������� ����� ������
        {
            int place = Random.Range(0, camera_w);
            start.x = place;
            start.y = camera_h + out_camera;
            start.z = Camera.main.nearClipPlane;
            start = Camera.main.ScreenToWorldPoint(start);
            start.z = 0f;
            angle = (GetAngleRand(place, camera_w) - 180f) * -1f;
        }
        else if (side == 2) // ����� ����� ������
        {
            int place = Random.Range(0, camera_h);
            start.x = -out_camera;
            start.y = place;
            start.z = Camera.main.nearClipPlane;
            start = Camera.main.ScreenToWorldPoint(start);
            start.z = 0f;
            angle = -90 - GetAngleRand(place, camera_h);
        }
        else if (side == 3) // ������ ����� ������
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
        item = Instantiate(item, start, Quaternion.identity);
        item.GetComponent<ObjectFly>().ActivateObj(spd, angle, 15f);

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
        if (a_time >= asteroid_delay)
            a_time = 0f;
    }
}