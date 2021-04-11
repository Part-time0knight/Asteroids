using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatText : MonoBehaviour
{
    [SerializeField] string[] frase;
    [SerializeField] float spd = 0.1f;
    [SerializeField] GameObject cat_face;
    private Text cursed_text;
    private Animator face_anim = null;
    private float delta_time = 0f;
    private string take_frase;
    private bool stop = false;
    private int total;
    private int ind = 0;
    private int length;
    

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