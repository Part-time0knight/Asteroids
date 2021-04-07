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
    private float range = 30f;

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
            NewAsteroid(0, 1);
            if (Random.Range(0, 2) > 0)
            {
                NewAsteroid(0, -1);
            }
        }
    }
    private void NewAsteroid(int new_size, int side)
    {

        Vector3 pos = new Vector3(transform.position.x + c_collider.radius / 2f * side, transform.position.y + c_collider.radius / 2f * side, 0);
        GameObject new_asteroid = Instantiate(aster_pref, pos, Quaternion.identity);
        new_asteroid.GetComponent<AsteroidState>().AsteroidSize(new_size);
        new_asteroid.GetComponent<ObjectFly>().ActivateObj(state.GetSpeed() + Random.Range(-2f, 2f), transform.eulerAngles.z + Random.Range(0, range * side) * 2, 15f);
    }
}
