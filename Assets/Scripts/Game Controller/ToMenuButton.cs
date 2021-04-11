using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToMenuButton : MonoBehaviour
{
    [SerializeField] string to_scene;
    [SerializeField] Color on_mause;
    [SerializeField] Color click_mause;
    private Color butten_color;
    private Text butten;
    private void Awake()
    {
        butten = GetComponent<Text>();
        butten_color = butten.color;
    }
    private void OnMouseDown()
    {
        butten.color = click_mause;
    }
    private void OnMouseOver()
    {
        butten.color = butten_color;
    }
    private void OnMouseEnter()
    {
        butten.color = on_mause;
    }
}
