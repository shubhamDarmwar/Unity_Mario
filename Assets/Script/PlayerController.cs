using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
	public float speed = 5f;
	public float jumpSpeed = 5f;
	private float movement = 0f;

	private Rigidbody2D rigidBody;
	//Following are connected in Scene 
	public Transform groundCheckPoint;
	public float groundCheckRadius;
	public LayerMask groundLayer;
	private bool isTouchingGround;
	private Animator playerAnimator;
	private float originalScale;

    public Vector3 respawnPoint;
    public LevelManager levelManager;

    //Score
    public Text scoreText;
    private int score = 0;

    // Audio 
    public AudioSource audioSource;
    public AudioClip coinAudioClip;
    public AudioClip blastAudioClip;
    public AudioClip jumpAudioClip;
    public AudioClip checkPointAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    	rigidBody = GetComponent<Rigidbody2D>(); 
        playerAnimator = GetComponent<Animator>();
        originalScale = transform.localScale.x;
        respawnPoint = transform.position;
        levelManager = FindObjectOfType<LevelManager>();
        scoreText.text = "Score: " + score.ToString();
        print("Player start");

    }

    // Update is called once per frame
    void Update()
    {   

    	GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    	isTouchingGround = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayer);
    	//Negative for left movement and +ve value for right movement
        movement = CrossPlatformInputManager.GetAxis("Horizontal");
        if (movement < 0f) {
        	rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
        	transform.localScale = new Vector2(-originalScale,originalScale);
        } else if (movement > 0f) {
        	rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
        	transform.localScale = new Vector2(originalScale,originalScale);
        } else {
        	rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

        //Jump
        if (CrossPlatformInputManager.GetButtonDown("Jump") && isTouchingGround) {
        	rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            audioSource.clip = jumpAudioClip;
            audioSource.Play();
        }

        //Animation
        playerAnimator.SetFloat("Speed",Mathf.Abs(rigidBody.velocity.x));
        playerAnimator.SetBool("OnGround", isTouchingGround);
        
    }

    void FixedUpdate() {
    	scoreText.text = "Score: " + score.ToString();
    }
    void OnTriggerEnter2D(Collider2D other) {
        
    	if (other.tag == "FallDetector") {
            levelManager.respawn();
    	} else if (other.tag == "Checkpoint") {
            audioSource.clip = checkPointAudioClip;
            audioSource.Play();
            
            respawnPoint = other.transform.position;
        } else if (other.tag == "Coin") {
        	score += 1;
            audioSource.clip = coinAudioClip;
            audioSource.Play();
        } else if (other.tag == "Bomb") {
        	levelManager.respawn();
            audioSource.clip = blastAudioClip;
            audioSource.Play();
        } else if (other.tag == "LevelEnd") {
            levelManager.changeLevel(1);
            print("Level End");
        }
    }
}
