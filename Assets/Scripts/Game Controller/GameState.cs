using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Игровой контроллер. Содержит методы управления сецной, сохранения данных,
 * контроля игрока и управления игровой информации.
 * 
 */
public class GameState : MonoBehaviour
{
    [SerializeField] GameObject player; //Объект игрового персонажа
    //---------------------------------
    /*Жизни игрового персонажа. Длина массива - колличество жизней,
     * объекты массива - изображения жизней в HUD*/
    [SerializeField] GameObject[] life; 
    //---------------------------------
    [SerializeField] GameObject pause_text;//Подменю игровой паузы
    private AudioSource bg_music;//саундтрэк
    private string nickname = "";//имя игрока
    private int score = 0;//набранные очки
    private int life_ind;//кол-во текущих жизней
    private bool play = false;//активен ли игровой процесс? Отрицание паузы
    private bool press_pause = false;//Вызвана ли пауза нажатием?
    private int rating_pos = 0;//позиция элемента сохранения
    private void Awake()
    {
        PlayerSpawn();
        life_ind = life.Length - 1;
        //--определение позиции в списке сохранений--
        if (!PlayerPrefs.HasKey("index") || PlayerPrefs.GetInt("index") == 9)
            PlayerPrefs.SetInt("index", rating_pos);
        else
            rating_pos = PlayerPrefs.GetInt("index");
        //---загрузка значения очков---
        if(SceneManager.GetActiveScene().name == "EndGame")
            score = PlayerPrefs.GetInt("temp");
        else
            Invoke("TikScore", 1f);
        //---запуск фоновой музыки при наличии-----
        bg_music = GetComponent<AudioSource>();
        if (bg_music)
            SoundPlay();
    }
    private void Update()
    {
        //-----проверка инпута паузы------
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
    //----загруска с момента остановки фоновой музыки
    public void SoundPlay()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
            bg_music.time = PlayerPrefs.GetFloat("music");
        bg_music.Play();
    }
    //---Метод записи имени игрока---
    public void SetNickname(string new_nickname)
    {
        nickname = new_nickname;
    }
    //---Метод сохранения результата игры---
    public void SaveScore()
    {
        string ind = rating_pos.ToString();
        PlayerPrefs.SetString(ind + "s", nickname);
        PlayerPrefs.SetInt(ind + "i", score);
        PlayerPrefs.SetInt("index", rating_pos + 1);
    }
    //---Методы управления значением игровых очков---
    public void SetScore( int new_score )
    {
        score = new_score;
    }
    public int GetScore()
    {
        return score;
    }
    private void TikScore()//метод начисляющий игровые очки каждую секунду
    {
        if (play)
            score++;
        Invoke("TikScore", 1f);
    }
    //----методы управлением персонажем игрока----
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
    //------методы вызова паузы------
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
    //------методы менеджмента сцен-------
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

    //----------------------
    private void OnDestroy()
    {
        CancelInvoke();
    }
}
