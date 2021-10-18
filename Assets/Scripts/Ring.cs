using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private Transform _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > _player.position.y)
        {
            FindObjectOfType<AudioManager>().Play("whoosh");
            GameManager.numberOfPassedRings++;
            GameManager.score++;
            Destroy(gameObject);
        }
    }
}