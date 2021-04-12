using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjState))]
[RequireComponent(typeof(CircleCollider2D))]
/*
 * Уникальные свойства астероида
 * 
 */

public class AsteroidState : MonoBehaviour
{
    [SerializeField] private int damage = 1;//урон
    [SerializeField] private int hp = 1;//хит-поинты
    [SerializeField] private Sprite[] huge_spr;//спрайты ооочень больших астероидов
    [SerializeField] private float huge_coll_radius = 1.2f;
    [SerializeField] private Sprite[] big_spr;//спрайты больших астероидов
    [SerializeField] private float big_coll_radius = 0.8f;
    [SerializeField] private Sprite[] medium_spr;//спрайты средних астероидов
    [SerializeField] private float medium_coll_radius = 0.65f;
    [SerializeField] private Sprite[] small_spr;//спрайты маленьких астероидов
    [SerializeField] private float small_coll_radius = 0.2f;
    [SerializeField] private int score = 10;
    private ObjState state;
    private SpriteRenderer spr;
    private CircleCollider2D c_collider;
    private Factory creator;
    private int size;
    private float range = 45f;

    private void Awake()
    {
        state = GetComponent<ObjState>();
        state.InitObj(hp, damage, score, PreDestroy);
        spr = GetComponentInChildren<SpriteRenderer>();
        c_collider = GetComponent<CircleCollider2D>();
        creator = ScriptableObject.CreateInstance<Factory>();
    }
    //----инициация типа астероида----
    public void AsteroidSize(int new_size)
    {
        size = new_size;
        if (size == 3)
        {
            spr.sprite = huge_spr[Random.Range(0, huge_spr.Length)];
            c_collider.radius = huge_coll_radius;
        }
        else if (size == 2)
        {
            spr.sprite = big_spr[Random.Range(0, big_spr.Length)];
            c_collider.radius = big_coll_radius;
        }
        else if (size == 1)
        {
            spr.sprite = medium_spr[Random.Range(0, medium_spr.Length)];
            c_collider.radius = medium_coll_radius;
        }
        else if (size == 0)
        {
            spr.sprite = small_spr[Random.Range(0, small_spr.Length)];
            c_collider.radius = small_coll_radius;
        }
        else
            Debug.LogError("Астероида такого размера не существует!");
    }
    //---порождение астероидов поменьше перед смертью---
    private void PreDestroy()
    {
        if (size > 0)
        {
            NewAsteroid(size - 1, 1);
            if (Random.Range(0, 2) > 0)
            {
                NewAsteroid(0, -1);
            }
        }
    }
    //---метод создающий новый астероид---
    private void NewAsteroid(int new_size, int side)
    {
        int camera_w = Camera.main.pixelWidth;
        float rad = transform.eulerAngles.z * Mathf.PI / 180;
        float spd = state.GetSpeed() + Random.Range(-0.5f, 1.5f);
        float pos_x = transform.position.x + Mathf.Cos(rad) * c_collider.radius / 2f * side;
        float pos_y = transform.position.y + Mathf.Sin(rad) * c_collider.radius / 2f * side;
        float angle = transform.eulerAngles.z - Random.Range(5, range) * side;
        float life = Camera.main.ScreenToWorldPoint(new Vector3(camera_w, 0, 0)).x / spd * 2.1f;
        Vector3 pos = new Vector3(pos_x, pos_y, 0);
        creator.CreateObject(gameObject, pos, life, spd, angle, new_size);
    }
}
