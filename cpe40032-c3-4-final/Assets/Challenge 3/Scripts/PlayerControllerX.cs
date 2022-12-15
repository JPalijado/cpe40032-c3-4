using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;

    public float floatForce;
    public float decentForce;
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
    public float bounceForce;

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 30, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // While arrow up is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.UpArrow) && isLowEnough && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

        // While arrow down is pressed, speed up descend
        if (Input.GetKey(KeyCode.DownArrow) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * -decentForce);
        }

        // Checks if the y postion is less than the y boundary
        if (transform.position.y > yBound)
        {
            isLowEnough = false;
        }
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
            // Destroys the bomb game object
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
            // Adds 1 to the score
            AddScore(1);
        }

        // if player collides with ground, explode and set gameOver to true
        else if (other.gameObject.CompareTag("Ground"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
        }
    }

    public void AddScore(int value)
    {
        // Counts and displays the score
        score += value;
        Debug.Log("Score = " + score);
    }
}