using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
/*
 * —крипт, позвол€ющий коту поливать помо€ми игрока
 * 
 */

public class CatText : MonoBehaviour
{
    [SerializeField] string[] frase;//массив плохих фраз
    [SerializeField] float spd = 0.1f;//скорость вывода текста
    [SerializeField] GameObject cat_face;//мерзкое лицо мерзкого кота
    private Text cursed_text;//поле вывода текста
    private Animator face_anim = null;//мерзка€ анимаци€ мерзкого кота
    private float delta_time = 0f;//таймер
    private string take_frase;//выбранна€ плоха€ фраза
    private bool stop = false;//остановка анимации
    private int total;//колличество плохих фраз
    private int ind = 0;//текущий символ плохой фразы
    private int length;//длинна плохой фразы
    

    private void Awake()
    {
        cursed_text = GetComponent<Text>();
        if (cat_face)
            face_anim = cat_face.GetComponent<Animator>();
        total = frase.Length;
        take_frase = frase[Random.Range(0, total)];
        length = take_frase.Length;
    }
    private void Update()
    {
        if (delta_time == 0f && ind < length)
        {
            cursed_text.text += take_frase[ind++];
        }
        else if (ind >= length && !stop)
        {
            if (face_anim)
                face_anim.SetBool("stop", true);
            stop = true;
        }
        delta_time += Time.deltaTime;
        if (delta_time >= spd)
            delta_time = 0f;
    }
}