using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parallax : MonoBehaviour
{
    [SerializeField] private float _speed = 0.1f;
    private Transform _camera;
    private Vector3 _lastPos;

    void Start()
    {
        _camera = Camera.main.transform;
        _lastPos = _camera.position;
    }

    void LateUpdate()
    {
        Vector3 delta = _camera.position - _lastPos;
        transform.position += delta * _speed;
        _lastPos = _camera.position;
    }
}