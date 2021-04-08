using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjState : MonoBehaviour
{
    private int hp = 1;
    private int dmg = 1;
    private float spd = 0;
    private bool in_fly = false;

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
    public void InitObj(int new_hp, int new_damage)
    {
        hp = new_hp;
        dmg = new_damage;
    }
    public int GetDamage()
    {
        return dmg;
    }
    public int GetHP()
    {
        return hp;
    }
    public void SetHP(int new_hp)
    {
        hp = new_hp;
    }
    public float GetSpeed()
    {
        return spd;
    }
    public void SetSpeed(float new_spd)
    {
        spd = new_spd;
    }
}
