using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ������, ������������ ������ 
 * ��� ��������� ������������ �����
 * 
 */
public class SoundEffect : MonoBehaviour
{
    private AudioSource audio_source;
    private void Awake()
    {
        audio_source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (!audio_source.isPlaying)
            Destroy(gameObject);
    }
}
