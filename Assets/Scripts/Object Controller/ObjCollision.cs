using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjState))]
[RequireComponent(typeof(Collider2D))]
/*
 * —крипт провер€ющий на столкновение с другим объектом, 
 * имеющим collider и этот скрипт
 * 
 */
public class ObjCollision : MonoBehaviour
{
    [SerializeField] GameObject explode_obj = null;
    private ObjState state;
    private GameObject damag_maker;
    GameState game_controller;
    private int hp; //хит поинты объекта
    private int dmg; //урон объекта

    void Awake()
    {
        state = GetComponent<ObjState>();
        game_controller = state.GetGameState();
    }
    //---метод получени€ урона---
    public void DealDamage(int damage, GameObject damager)
    {
        damag_maker = damager;
        hp = state.GetHP();
        hp -= damage;
        state.SetHP(hp);
        if (hp <= 0)
            Explode();
    }
    //---метод гибели объекта---
    private void Explode()
    {
        state.DestroyObj(0f);
        if (explode_obj != null)
            Instantiate(explode_obj, transform.position, Quaternion.identity);
        if (damag_maker.tag == "Player Bullet")
        {
            game_controller.SetScore(game_controller.GetScore() + state.GetScore());
        }
    }
    //---проверка столкновений---
    void OnTriggerEnter2D(Collider2D targed)
    {
        ObjCollision targed_inf = targed.GetComponent<ObjCollision>();
        if (targed_inf != null)
        {
            dmg = state.GetDamage();
            targed_inf.DealDamage(dmg, gameObject);
        }
    }
}
