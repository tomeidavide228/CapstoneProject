using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    [SerializeField] private Transform _player;

    private bool _isFlipped = true;

	private void Update()
	{
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        float direction = _player.position.x - transform.position.x;
        Debug.Log($"Player position: {_player.position.x}, Boss position: {transform.position.x}, Direction: {direction}");

        if (direction > 0 && _isFlipped)
        {
            Debug.Log("Flipping boss to the right");
            Flip();
        }
        else if (direction < 0 && !_isFlipped)
        {
            Debug.Log("Flipping boss to the left");
            Flip();
        }
    }

    private void Flip()
    {
        _isFlipped = !_isFlipped;

        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
    }

    public void StartBossFight()
    {
        gameObject.SetActive(true);
    }


}
