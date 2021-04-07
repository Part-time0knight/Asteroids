using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjState))]
public class ObjCollision : MonoBehaviour
{
    [SerializeField] GameObject explode_obj = null;
    private ObjState state;
    private int hp;
    private int dmg;

    void Awake()
    {
        state = GetComponent<ObjState>();
    }
    public void DealDamage(int damage)
    {
        hp = state.GetHP();
        hp -= damage;
        state.SetHP(hp);
        if (hp <= 0)
        {
            Explode();
        }
    }
    private void Explode()
    {
        Destroy(gameObject);
        if (explode_obj != null)
            Instantiate(explode_obj, transform.position, Quaternion.identity);
    }
    void OnTriggerEnter2D(Collider2D targed)
    {
        ObjCollision targed_inf = targed.GetComponent<ObjCollision>();
        if (targed_inf != null)
        {
            dmg = state.GetDamage();
            targed_inf.DealDamage(dmg);
        }
    }
}
