using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    public bool isLowEnough;
    private float yBound = 12;

    public AudioClip bounceSound;
    public float bounceForce = 10;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        // Problem 1 (The balloon should float up as the player presses spacebar)
        // Solution: add playerRb = GetComponent<Rigidbody>();
        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && isLowEnough && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        // Problem 6 Bonus (Prevent the player from floating their balloon too high)
        // Solution: add boolean isLowEnough and yBound.
        // If the y position of the player is less than the ybound, set isLowEnough = false
        if (transform.position.y > yBound)
        {
            isLowEnough = false;
        }
        // If the y position of the player is greater than the ybound, set isLowEnough = true
        else
        {
            isLowEnough = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

        // Problem 9 Bonus (Make the balloon appear to bounce off of the ground, preventing it from leaving the bottom of the screen. There should be a sound effect when this happens, too!)
        // Solution: add a tag "Ground" to the ground gameobject
        else if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            // Adds a small upward force to let the player bounce
            playerRb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            playerAudio.PlayOneShot(bounceSound, 1.5f);
        }
    }
}
