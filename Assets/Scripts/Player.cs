using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerRB;
    public float bounceForce = 6;

    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        _audioManager.Play("bounce");
        playerRB.velocity = new Vector3(playerRB.velocity.x, bounceForce, playerRB.velocity.z);
        String materialName = other.transform.GetComponent<MeshRenderer>().material.name;
        if (materialName.Equals("Safe (Instance)"))
        {
            
        } else if (materialName.Equals("Unsafe (Instance)"))
        {
            GameManager.gameOver = true;
            _audioManager.Play("game over");
        }
        else if (materialName.Equals("LastRing (Instance)") && !GameManager.levelCompleted)
        {
            GameManager.levelCompleted = true;
            _audioManager.Play("win level");
        }
    }
}
