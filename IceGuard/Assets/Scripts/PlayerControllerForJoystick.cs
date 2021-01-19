using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControllerForJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Joystick joystick;
    private bool _upPressed;
    private bool _downPressed;
    private bool _leftPressed;
    private bool _rightPressed;

    public AnimationCurve Easing;
    public float TimeToReachNextTile = 0.7f;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _time;
    [SerializeField] private int RotationSpeed = 1;
    void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        MoveDirection();
        MoveUp();
        MoveDown();
        MoveLeft();
        MoveRight();
    }

    private void MoveDirection()
    {
        if (joystick.Direction.x > 0 && joystick.Direction.y > 0)
        {
            _upPressed = true;
            _rightPressed = false;
            _downPressed = false;
            _leftPressed = false;
        }
        if (joystick.Direction.x > 0 && joystick.Direction.y < 0)
        {
            _upPressed = false;
            _rightPressed = true;
            _downPressed = false;
            _leftPressed = false;
        }
        if (joystick.Direction.x < 0 && joystick.Direction.y < 0)
        {
            _upPressed = false;
            _rightPressed = false;
            _downPressed = true;
            _leftPressed = false;
        }
        if (joystick.Direction.x < 0 && joystick.Direction.y > 0)
        {
            _upPressed = false;
            _rightPressed = false;
            _downPressed = false;
            _leftPressed = true;
        }
    }

    private void MoveUp()
    {
        if (_upPressed)
        {
            if (_time == 0)
            {
                _endPosition = _startPosition + Vector3.left * 1.1f;
            }
            transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
            _time += Time.deltaTime;
        }
        if (_time >= TimeToReachNextTile)
        {
            _startPosition = _endPosition;
            _time = 0;
            _upPressed = false;
        }
    }
    private void MoveDown()
    {
        if (_downPressed)
        {
            if (_time == 0)
            {
                _endPosition = _startPosition + Vector3.left * -1.1f;
            }
            transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
            _time += Time.deltaTime;
        }
        if (_time >= TimeToReachNextTile)
        {
            _startPosition = _endPosition;
            _time = 0;
            _downPressed = false;
        }
    }
    private void MoveLeft()
    {
        if (_leftPressed)
        {
            if (_time == 0)
            {
                _endPosition = _startPosition + Vector3.forward * -1.1f;
            }
            transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
            _time += Time.deltaTime;
        }
        if (_time >= TimeToReachNextTile)
        {
            _startPosition = _endPosition;
            _time = 0;
            _leftPressed = false;
        }
    }
    private void MoveRight()
    {
        if (_rightPressed)
        {
            if (_time == 0)
            {
                _endPosition = _startPosition + Vector3.forward * 1.1f;
            }
            transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
            _time += Time.deltaTime;
        }
        if (_time >= TimeToReachNextTile)
        {
            _startPosition = _endPosition;
            _time = 0;
            _rightPressed = false;
        }
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

    // колбэки для проверки нажимаем ли мы мышкой на объект
    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
