using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private FloatingJoystick JoyStick;

    public float m_moveSpeed = 2;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;


    private Vector3 m_currentDirection = Vector3.zero;





    private void FixedUpdate()
    {
        DirectUpdate();

    }

    private void DirectUpdate()
    {

        float v = JoyStick.Vertical;
        float h = JoyStick.Horizontal;
      
        Transform camera = Camera.main.transform;

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * GameManager.Instance.playerSpeed * Time.deltaTime;

            gameObject.GetComponent<Animator>().SetFloat("MoveSpeed", direction.magnitude);
        }

    }


}
