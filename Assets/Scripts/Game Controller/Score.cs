using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    private GameState state;
    private Text score;
    private void Awake()
    {
        state = GameObject.FindGameObjectsWithTag("Game Controller")[0].GetComponent<GameState>();
        score = GetComponent<Text>();
    }
    void Update()
    {
        score.text = "Score: " + state.GetScore();
    }
}
