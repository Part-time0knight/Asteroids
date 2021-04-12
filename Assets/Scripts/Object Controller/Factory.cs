using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Класс, помогающий создавать новые игровые объекты
 * Используется для создания пуль и противников
 */
public class Factory : ScriptableObject
{
    public GameObject CreateObject(GameObject item, Vector3 position, float life_time = 0f, float spd = 0f, float angle = 0f, int asteroid_size = -1)
    {
        GameObject create_item = Instantiate(item, position, Quaternion.identity);
        AsteroidState state1 = create_item.GetComponent<AsteroidState>();
        create_item.GetComponent<ObjFly>().ActivateObj(spd, angle, life_time);
        if (state1)
        {
            if (asteroid_size == -1)
                asteroid_size = Random.Range(0, 4);
            state1.GetComponent<AsteroidState>().AsteroidSize(asteroid_size);
        }
        return create_item;
    }
}
