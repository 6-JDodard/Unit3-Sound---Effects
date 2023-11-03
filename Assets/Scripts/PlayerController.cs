using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isOnGround;
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool gameOver;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound; 
    public AudioClip crashSound;
    private AudioSource playerAudio;
    
    // Start is called before the first frame update
    void Start()
    { 
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround  && !gameOver)
        {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        
        void OnCollisionEnter(Collision collision)
        {
          if (collision.gameObject.CompareTag("Ground"))
          {
            isOnGround = true;
            dirtParticle.Play(); 
          }
          else if (collision.gameObject.CompareTag("Obstacle"))
          {
            Debug.Log("Game Over!");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
          }
        }
    }
}
