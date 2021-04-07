using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidState : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private int hp = 1;
    [SerializeField] private Sprite[] big_spr;
    [SerializeField] private float big_coll_radius = 1f;
    [SerializeField] private Sprite[] medium_spr;
    [SerializeField] private float medium_coll_radius = 0.65f;
    [SerializeField] private Sprite[] small_spr;
    [SerializeField] private float small_coll_radius = 0.2f;
    [SerializeField] private GameObject aster_pref;
    private ObjState state;
    private SpriteRenderer spr;
    private CircleCollider2D c_collider;
    private int size;
    private float range = 45f;

    private void Awake()
    {
        state = GetComponent<ObjState>();
        state.InitObj(hp, damage);
        spr = GetComponentInChildren<SpriteRenderer>();
        c_collider = GetComponent<CircleCollider2D>();
    }
    public void AsteroidSize(int new_size)
    {
        size = new_size;
        if (size == 2)
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
        Debug.Log(spr.sprite + " " + c_collider.radius);
    }
    public void OnDestroy()
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
    private void NewAsteroid(int new_size, int side)
    {
        float rad = transform.eulerAngles.z * Mathf.PI / 180;
        float pos_x = transform.position.x + Mathf.Cos(rad) * c_collider.radius / 2f * side;
        float pos_y = transform.position.y + Mathf.Sin(rad) * c_collider.radius / 2f * side;
        float angle = transform.eulerAngles.z - Random.Range(5, range) * side;

        Vector3 pos = new Vector3(pos_x, pos_y, 0);
        GameObject new_asteroid = Instantiate(aster_pref, pos, Quaternion.identity);
        new_asteroid.GetComponent<AsteroidState>().AsteroidSize(new_size);
        new_asteroid.GetComponent<ObjectFly>().ActivateObj(state.GetSpeed() + Random.Range(-0.5f, 1.5f), angle, 15f);
    }
}
