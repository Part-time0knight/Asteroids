using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] life;
    private int score = 0;
    private int life_ind;
    private bool play = false;
    private bool press_pause = false;
    private void Awake()
    {
        PlayerSpawn();
        Invoke("TikScore", 1);
        life_ind = life.Length - 1;
    }
    private void Update()
    {
        if (play && Input.GetButtonDown("Pause"))
        {
            Pause(true);
            press_pause = true;
        }
        if (press_pause && !play && Input.GetButtonDown("Pause"))
        {
            Pause(false);
            press_pause = false;
        }
    }
    public void SetScore( int new_score )
    {
        score = new_score;
    }
    public int GetScore()
    {
        return score;
    }
    private void TikScore()
    {
        if (play)
            score++;
        Invoke("TikScore", 1f);
    }
    private void PlayerSpawn()
    {
        GameObject new_player = Instantiate(player, new Vector3(0f, 0f, 0f), Quaternion.identity);
        Pause(false, new_player.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
    public void PlayerDead()
    {
        Pause(true);
        Destroy(life[life_ind--]);
        if (life_ind >= 0)
            Invoke("PlayerSpawn", 1f);
        else
            EndGame();
    }
    public void Pause(bool active, float time = 0f)
    {
        if (active)
            if (time > 0f)
                Invoke("ActivePause", time);
            else
                ActivePause();
        else
            if (time > 0f)
                Invoke("DeactivePause", time);
            else
                DeactivePause();


    }
    private void ActivePause()
    {
        play = false;
    }
    private void DeactivePause()
    {
        play = true;
    }
    public bool GetPause()
    {
        return !play;
    }
    private void EndGame()
    {

    }
    private void OnDestroy()
    {
        CancelInvoke();
    }
}
