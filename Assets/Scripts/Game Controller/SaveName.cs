using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveName : MonoBehaviour
{
    [SerializeField] GameObject text_obj;
    private GameState state;
    private void Awake()
    {
        state = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
    }
    public void SaveNickName()
    {
        state.SetNickname(text_obj.GetComponent<Text>().text);
    }
}
