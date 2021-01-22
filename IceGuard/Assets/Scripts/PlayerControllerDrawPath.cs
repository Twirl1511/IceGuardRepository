using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControllerDrawPath : MonoBehaviour
{
    public static Queue<Vector3> queuePath;
    public AnimationCurve Easing;
    public static float TimeToReachNextTile = 0.7f;
    public static Vector3 _startPosition;
    private Vector3 _endPosition;
    public static Vector3[] adjacentPositions = new Vector3[4];

    private float _time;
    [SerializeField] private int RotationSpeed = 1;
    void Start()
    {
        _startPosition = transform.position;
        queuePath = new Queue<Vector3>();
    }

    private void Update()
    {
        AdjacentPositions();
        Move();
    }
    private void AdjacentPositions()
    {
        adjacentPositions[0] = _startPosition + Vector3.left;
        adjacentPositions[1] = _startPosition + Vector3.left * -1;
        adjacentPositions[2] = _startPosition + Vector3.forward;
        adjacentPositions[3] = _startPosition + Vector3.forward * -1;
    }
    private bool flag = false;
    private void Move()
    {
        
        if (!Input.GetMouseButton(0) && queuePath.Count > 0 && _time == 0)
        {
            
            _endPosition = queuePath.Dequeue();
            flag = true;

            CreateEnergyField();
            
        }
        if (flag)
        {
            transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
            _time += Time.deltaTime;
        }
        
        if (_time >= TimeToReachNextTile)
        {
            _startPosition = _endPosition;
            flag = false;
            _time = 0;
            
                
        }
    }

    private void CreateEnergyField()
    {
        Instantiate(Resources.Load("force_field"), transform.position, Quaternion.identity);
    }
    IEnumerator DelayForEnergyField()
    {
        yield return new WaitForSeconds(TimeToReachNextTile);
        
    }

    //private void MoveDown()
    //{
    //    if (Down.GetComponent<ButtonState>().IsPressed)
    //    {
    //        if (_time == 0)
    //        {
    //            _endPosition = _startPosition + Vector3.left * -1.1f;
    //        }
    //        transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
    //        _time += Time.deltaTime;
    //    }
    //    if (_time >= TimeToReachNextTile)
    //    {
    //        _startPosition = _endPosition;
    //        _time = 0;
    //        Down.GetComponent<ButtonState>().IsPressed = false;
    //    }
    //}
    //private void MoveLeft()
    //{
    //    if (Left.GetComponent<ButtonState>().IsPressed)
    //    {
    //        if (_time == 0)
    //        {
    //            _endPosition = _startPosition + Vector3.forward * -1.1f;
    //        }
    //        transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
    //        _time += Time.deltaTime;
    //    }
    //    if (_time >= TimeToReachNextTile)
    //    {
    //        _startPosition = _endPosition;
    //        _time = 0;
    //        Left.GetComponent<ButtonState>().IsPressed = false;
    //    }
    //}
    //private void MoveRight()
    //{
    //    if (Right.GetComponent<ButtonState>().IsPressed)
    //    {
    //        if (_time == 0)
    //        {
    //            _endPosition = _startPosition + Vector3.forward * 1.1f;
    //        }
    //        transform.position = LerpMoveTo(_startPosition, _endPosition, TimeToReachNextTile);
    //        _time += Time.deltaTime;
    //    }
    //    if (_time >= TimeToReachNextTile)
    //    {
    //        _startPosition = _endPosition;
    //        _time = 0;
    //        Right.GetComponent<ButtonState>().IsPressed = false;
    //    }
    //}

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
