using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AnimationCurve Easing;
    [SerializeField] float TimeToReachNextTile = 3;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _time;
    void Start()
    {
        _startPosition = transform.position;
        _endPosition = _startPosition + new Vector3(-1.1f, 0f, 0f);
    }
    private void FixedUpdate()
    {
        Debug.Log(Input.GetAxis("Mouse X")); 
        if(Input.GetAxis("Mouse X") > 0.5f)
        {
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
        //if (Input.GetAxis("Mouse X") < -0.5f)
        //{
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -90f, 0), 0.1f);
        //}
    }

    void Update()
    {
        transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
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
        _time += Time.deltaTime;
        return Vector3.Lerp(start, end, Easing.Evaluate(_time / time));
    }
}
