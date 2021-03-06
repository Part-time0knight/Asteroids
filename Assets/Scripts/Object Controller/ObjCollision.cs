using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjState))]
[RequireComponent(typeof(Collider2D))]
/*
 * ?????? ??????????? ?? ???????????? ? ?????? ????????, 
 * ??????? collider ? ???? ??????
 * 
 */
public class ObjCollision : MonoBehaviour
{
    [SerializeField] GameObject explode_obj = null;
    private ObjState state;
    private GameObject damag_maker;
    GameState game_controller;
    private int hp; //??? ?????? ???????
    private int dmg; //???? ???????

    void Awake()
    {
        state = GetComponent<ObjState>();
        game_controller = state.GetGameState();
    }
    //---????? ????????? ?????---
    public void DealDamage(int damage, GameObject damager)
    {
        damag_maker = damager;
        hp = state.GetHP();
        hp -= damage;
        state.SetHP(hp);
        if (hp <= 0)
            Explode();
    }
    //---????? ?????? ???????---
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
    //---???????? ????????????---
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
