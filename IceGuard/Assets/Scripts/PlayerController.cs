using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button Up;
    public Button Down;
    public Button Left;
    public Button Right;
    private bool _upPressed;
    private bool _downPressed;
    private bool _leftPressed;
    private bool _rightPressed;

    public AnimationCurve Easing;
    public static float TimeToReachNextTile = 0.7f;
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
        MoveUp();
        MoveDown();
        MoveLeft();
        MoveRight();

    }

    private void MoveUp()
    {
        if (Up.GetComponent<ButtonState>().IsPressed)
        {
            if(_time == 0)
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
            Up.GetComponent<ButtonState>().IsPressed = false;
        }
    }
    private void MoveDown()
    {
        if (Down.GetComponent<ButtonState>().IsPressed)
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
            Down.GetComponent<ButtonState>().IsPressed = false;
        }
    }
    private void MoveLeft()
    {
        if (Left.GetComponent<ButtonState>().IsPressed)
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
            Left.GetComponent<ButtonState>().IsPressed = false;
        }
    }
    private void MoveRight()
    {
        if (Right.GetComponent<ButtonState>().IsPressed)
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
            Right.GetComponent<ButtonState>().IsPressed = false;
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
