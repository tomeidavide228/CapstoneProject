using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 _currentCheckpoint;

    private void Start()
    {
        SaveData data = SaveSystem.Load();

        if (data != null)
        {
            _currentCheckpoint = new Vector3(data.CheckpointX, data.CheckpointY, 0);
            transform.position = _currentCheckpoint;
        }
        else
        {
            _currentCheckpoint = transform.position;
        }
    }

    public void UpdateCheckpoint(Vector3 newCheckpoint)
    {
        _currentCheckpoint = newCheckpoint;

        SaveData data = new SaveData();
        data.CheckpointX = newCheckpoint.x;
        data.CheckpointY = newCheckpoint.y;

        SaveSystem.Save(data);
    }

    public void Respawn()
    {
        transform.position = _currentCheckpoint;
    }
}
