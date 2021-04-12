using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void StateDestroy();
public class ObjState : MonoBehaviour
{
    private int hp = 1;
    private int dmg = 1;
    private float spd = 0;
    private bool in_fly = false; //для контроля анимации
    private int score = 0; //
    private float delta_time = -1f;
    private float destroy_time = 0f;
    StateDestroy SpecialDestroy = null;
    GameState game_state;
    private void Awake()
    {
        game_state = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
    }
    public void InitObj(int new_hp, int new_damage, int new_score, StateDestroy func)
    {
        hp = new_hp;
        dmg = new_damage;
        score = new_score;
        SpecialDestroy = func;
    }
    private void Update()
    {
        if (!game_state.GetPause() && delta_time >= 0f)
            delta_time += Time.deltaTime;
        if (destroy_time <= delta_time)
            DestroyObj(0);

    }
    public GameState GetGameState()
    {
        return game_state;
    }
    public void SetScore(int new_score)
    {
        score = new_score;
    }
    public int GetScore()
    {
        return score;
    }
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
    public void DestroyObj(float time)
    {
        destroy_time = time;
        delta_time = 0f;
        if (time == 0)
        {
            if (SpecialDestroy != null)
            {
                SpecialDestroy();
            }
            Destroy(gameObject);
        }
    }
}
