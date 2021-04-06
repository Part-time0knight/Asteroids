using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjState : MonoBehaviour
{
    [SerializeField] GameObject explode_obj;
    private int hp = 1;

    public void InitObj(int new_hp)
    {
        hp = new_hp;
    }
    public void DealDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Explode();
        }
    }
    private void Explode()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        Instantiate(explode_obj, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
