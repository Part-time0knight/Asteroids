using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] life;
    [SerializeField] GameObject pause_text;
    private AudioSource bg_music;
    private string nickname = "";
    private int score = 0;
    private int life_ind;
    private bool play = false;
    private bool press_pause = false;
    private int rating_pos = 0;
    private void Awake()
    {
        PlayerSpawn();
        life_ind = life.Length - 1;
        if (!PlayerPrefs.HasKey("index") || PlayerPrefs.GetInt("index") == 9)
            PlayerPrefs.SetInt("index", rating_pos);
        else
            rating_pos = PlayerPrefs.GetInt("index");
        if(SceneManager.GetActiveScene().name == "EndGame")
            score = PlayerPrefs.GetInt("temp");
        else
            Invoke("TikScore", 1);
        bg_music = GetComponent<AudioSource>();
        if (bg_music)
            SoundPlay();
    }
    private void Update()
    {
        if (pause_text && play && Input.GetButtonDown("Pause"))
        {
            Pause(true);
            pause_text.SetActive(true);
            press_pause = true;
        }
        else if (pause_text && press_pause && !play && Input.GetButtonDown("Pause"))
        {
            Pause(false);
            press_pause = false;
            pause_text.SetActive(false);
        }
    }
    public void SoundPlay()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
            bg_music.time = PlayerPrefs.GetFloat("music");
        bg_music.Play();
    }
    public void SetNickname(string new_nickname)
    {
        nickname = new_nickname;
    }
    public void SaveScore()
    {
        string ind = rating_pos.ToString();
        Debug.Log(score);
        PlayerPrefs.SetString(ind + "s", nickname);
        PlayerPrefs.SetInt(ind + "i", score);
        PlayerPrefs.SetInt("index", rating_pos + 1);
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
            Invoke("EndGame", 1f);
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
    public void EndGame()
    {
        SceneManager.LoadScene("EndGame");
        PlayerPrefs.SetInt("temp", score);
        PlayerPrefs.SetFloat("music", bg_music.time);
    }
    public void InMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void InGame()
    {
        SceneManager.LoadScene("Game");
        PlayerPrefs.SetFloat("music", bg_music.time);
    }
    private void OnDestroy()
    {
        CancelInvoke();
    }
}
