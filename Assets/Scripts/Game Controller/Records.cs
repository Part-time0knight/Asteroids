using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Records : MonoBehaviour
{
    private string[] records_arr;
    private string free_string = ".....................\n";
    private int length = 0;
    private Text record_text;
    private void Start()
    {
        length = PlayerPrefs.GetInt("index");
        records_arr = new string[10];
        record_text = GetComponent<Text>();
        for (int i = 0; i < 9; i++)
        {
            if (i < length)
                records_arr[i] = i + ".Score:" + PlayerPrefs.GetInt(i.ToString() + "i") + "...." + PlayerPrefs.GetString(i.ToString() + "s") + "\n";
            else
                records_arr[i] = i + free_string;
            record_text.text += records_arr[i];
        }
    }
}
