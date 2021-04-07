using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthRotate : MonoBehaviour
{
    float y;
    [SerializeField] private float _speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        y += _speed;
        transform.rotation = Quaternion.Euler(transform.rotation.x, y * Time.deltaTime, transform.rotation.z);
    }
}
