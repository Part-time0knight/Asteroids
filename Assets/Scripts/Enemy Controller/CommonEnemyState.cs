using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemyState : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet_ang;
    [SerializeField] GameObject bullet_start;
    [SerializeField] int score = 20;
    [SerializeField] int hp = 1;
    [SerializeField] int damage = 1;
    [SerializeField] float fire_spd = 1f;
    [SerializeField] float bullet_spd = 10f;
    [SerializeField] float bullet_life = 4f;

    private ObjState state;
    private float reload = 0f;
    private Factory shoota;
    private void Awake()
    {
        shoota = ScriptableObject.CreateInstance<Factory>();
        state = GetComponent<ObjState>();
        state.InitObj(hp, damage, score);
    }
    private void Update()
    {
        if (fire_spd < reload) reload = 0f;//����� �����������
        if (reload == 0f)
        {
            bullet_ang.transform.eulerAngles = new Vector3(0, 0, Random.Range(-180f, 180f));
            shoota.CreateObject(bullet, bullet_start.transform.position, bullet_life, bullet_spd, bullet_ang.transform.eulerAngles.z);//�������
        }
        reload += Time.deltaTime;//������� �����������
    }
}
