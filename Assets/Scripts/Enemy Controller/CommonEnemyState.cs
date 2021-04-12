using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjState))]
[RequireComponent(typeof(Animator))]
/*
 * ���������� �������� �������� ����������
 * 
 */
public class CommonEnemyState : MonoBehaviour
{
    [SerializeField] private GameObject bullet;//������ ����
    [SerializeField] private GameObject bullet_start;//������ ����� ������ ����
    [SerializeField] private GameObject bullet_ang;//������, ������������ ���� ��������
    [SerializeField] private int score = 20;//���������
    [SerializeField] private int hp = 1;//���-������
    [SerializeField] private int damage = 1;//����
    [SerializeField] private float fire_spd = 1f;//�������� ��������
    [SerializeField] private float bullet_spd = 10f;//�������� �����
    [SerializeField] private float bullet_life = 4f;//����� ����� ����
    private float reload = 0f;//������
    private float anim_spd;
    private ObjState state;
    private Factory shoota;//������� ����
    private GameState game_state;
    private Animator enemy_anim;//�������� �������

    private void Awake()
    {
        shoota = ScriptableObject.CreateInstance<Factory>();
        state = GetComponent<ObjState>();
        state.InitObj(hp, damage, score, null);
        game_state = state.GetGameState();
        enemy_anim = GetComponent<Animator>();
        anim_spd = enemy_anim.speed;
    }
    private void Update()
    {
        if (!game_state.GetPause())
        {
            if (fire_spd < reload) reload = 0f;//����� �����������
            if (reload == 0f)//������� ��������
            {
                bullet_ang.transform.eulerAngles = new Vector3(0, 0, Random.Range(-180f, 180f));
                shoota.CreateObject(bullet, bullet_start.transform.position, bullet_life, bullet_spd, bullet_ang.transform.eulerAngles.z);//�������
            }
            reload += Time.deltaTime;//������� �����������
            if (enemy_anim.speed == 0f)
            {
                enemy_anim.speed = anim_spd;//������������� �������� ��������
            }
        }
        else if (enemy_anim.speed != 0f)
        {
            enemy_anim.speed = 0f;//��������� �������� �� ����� �����
        }
        
    }
}
