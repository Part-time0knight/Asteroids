using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ObjState))]
/*
 * Скрипт поворота объекта за движением мыши
 * 
 * 
 */
public class MouseRotate : MonoBehaviour
{
    [SerializeField] float angl_fix = 90f;
    private ObjState state;
    private GameState game_state;
    private void Awake()
    {
        //------определение переменных------
        state = GetComponent<ObjState>();
    }
    private void Start()
    {
        //------определение переменных2------
        game_state = state.GetGameState();
    }
    void Update()
    {
        if (!game_state.GetPause())
        {
            Vector3 m_position = new Vector3();
            float angle;
            m_position = Input.mousePosition;
            m_position = Camera.main.ScreenToWorldPoint(m_position);
            angle = Vector2.Angle(Vector2.right, m_position - transform.position);
            if (transform.position.y >= m_position.y) angle *= -1f;
            transform.eulerAngles = new Vector3(0f, 0f, angle + angl_fix);
        }
    }
}
