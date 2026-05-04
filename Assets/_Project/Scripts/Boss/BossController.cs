using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
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

        if (direction > 0 && _isFlipped)
        {
            Flip();
        }
        else if (direction < 0 && !_isFlipped)
        {
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
        AudioManager.Instance.PlayMusic("Whispering Woods");
        gameObject.SetActive(true);
    }


}
