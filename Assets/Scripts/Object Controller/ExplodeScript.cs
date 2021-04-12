using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{
    [SerializeField] GameObject sound_effect = null;
    private void Awake()
    {
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        if (sound_effect)
            Instantiate(sound_effect, transform.position, Quaternion.identity);
    }
}
