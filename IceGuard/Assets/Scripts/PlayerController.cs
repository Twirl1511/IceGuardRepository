using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    public AnimationCurve Easing;
    [SerializeField] float TimeToReachNextTile = 3;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _time;
    [SerializeField] private int RotationSpeed = 1;
    void Start()
    {
        _startPosition = transform.position;
        //_endPosition = _startPosition + new Vector3(-1.1f, 0f, 0f);
    }


    void Update()
    {
        MoveForwardMouse();
        RotateMouse();
        //MoveForward();
        //Rotate();

    }
    private void MoveForward()
    {
        if (Input.GetTouch(0).phase.ToString() == "Stationary")
        {
            if(_time == 0)
            {
                _endPosition = _startPosition + transform.forward * 1.1f;
            }
            
            _time += Time.deltaTime;
            
            transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
        }
        if (_time >= TimeToReachNextTile)
        {
            _startPosition = _endPosition;
            _time = 0;
        }
    }
    private void MoveForwardMouse()
    {
        if (Input.GetMouseButton(0))
        {
            if (_time == 0)
            {
                _endPosition = _startPosition + transform.forward * 1.1f;
            }

            _time += Time.deltaTime;

            transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
        }
        if (_time >= TimeToReachNextTile)
        {
            _startPosition = _endPosition;
            _time = 0;
        }
    }
    private int y = 0;
    private bool flag = true;
    private void Rotate()
    {
        if (joystick.Horizontal < -0.8f && flag)
        {
            y -= 90;
            flag = false;
        }
        if (joystick.Horizontal > 0.8f && flag)
        {
            y += 90;
            flag = false;
        }
        if (_time == 0)
        {
            flag = true;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, y, 0), 1f);
    }
    private void RotateMouse()
    {
        Debug.Log(Input.GetAxis("Mouse X"));
        if (Input.GetAxis("Mouse X") < -0.8f && flag)
        {
            Debug.Log("left");
            y -= 90;
            flag = false;
        }
        if (Input.GetAxis("Mouse X") > 0.8f && flag)
        {
            y += 90;
            flag = false;
        }
        if (_time == 0)
        {
            flag = true;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, y, 0), 1f);
    }
    /// <summary>
    /// Плавно двигаем объект
    /// </summary>
    /// <param name="start">начальная позиция объекта</param>
    /// <param name="end">конечная позиция объекта</param>
    /// <param name="time">время за которое он переместится</param>
    /// <returns></returns>
    private Vector3 LerpMoveTo(Vector3 start, Vector3 end, float time)
    {
        return Vector3.Lerp(start, end, Easing.Evaluate(_time / time));
    }
}
