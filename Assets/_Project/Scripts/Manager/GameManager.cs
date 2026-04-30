using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _respawnPoint;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _respawnTime;

    private float _respawnTimeStart;
    private bool _respawn;
    private CinemachineVirtualCamera _camera;

    private void Start()
    {
        _camera = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        CheckRespawn();
    }
    public void Respawn()
    {
        _respawnTimeStart = Time.time;
        _respawn = true;
    }

    private void CheckRespawn()
    {
        if(Time.time >= _respawnTimeStart + _respawnTime && _respawn)
        {
            var playerTemp = Instantiate(_player, _respawnPoint);
            _camera.m_Follow = playerTemp.transform;
            _respawn = false;
        }
    }
}
