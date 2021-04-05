using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    [SerializeField] float angl_fix = 90f;

    void Update()
    {
        Vector3 m_position = new Vector3();
        float angle;
        m_position = Input.mousePosition;
        m_position = Camera.main.ScreenToWorldPoint(m_position); //положение мыши из экранных в мировые координаты
        angle = Vector2.Angle(Vector2.right, m_position - transform.position);//угол между вектором от объекта к мыше и осью х
        if (transform.position.y >= m_position.y) angle *= -1f;
        transform.eulerAngles = new Vector3(0f, 0f, angle + angl_fix);
    }
}
