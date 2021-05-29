using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public enum Clip {
    jump,
    coin,
    blast,
    checkPoint,
    gameOver
}


public class PlayerController : MonoBehaviour
{
	public float speed = 5f;
	public float jumpSpeed = 5f;
	private float movement = 0f;

	private Rigidbody2D rigidBody;
	//Following are connected in Scene 
	public GameObject cap;
	public Transform groundCheckPoint;
	public float groundCheckRadius;
	public LayerMask groundLayer;
	private bool isTouchingGround;
	private Animator playerAnimator;
	private float originalScale;

    public Vector3 respawnPoint;
    public static LevelManager levelManager;

    //Score
    public Text scoreText;
    public int score = 0;
    public int stones = 3;

    // Audio 
    public AudioController audioController;
    public AudioClip coinAudioClip;
    public AudioClip blastAudioClip;
    public AudioClip jumpAudioClip;
    public AudioClip checkPointAudioClip;

	private Transform originalCapTransform;
    // public GameObject stone;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    	rigidBody = GetComponent<Rigidbody2D>(); 
        playerAnimator = GetComponent<Animator>();
        originalScale = transform.localScale.x;
        respawnPoint = transform.position;
        levelManager = FindObjectOfType<LevelManager>();
        audioController = FindObjectOfType<AudioController>();
        loadPlayer();
        // originalCapTransform = cap.transform;
    }

    // Update is called once per frame
    void Update()
    {   

    	GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    	isTouchingGround = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayer);
    	//Negative for left movement and +ve value for right movement
        movement = CrossPlatformInputManager.GetAxis("Horizontal");//CrossPlatformInputManager
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
            audioController.playClip(Clip.jump);

            // cap.transform.Rotate (0, 0, 200);
        }
        if (isTouchingGround) {
        	cap.transform.eulerAngles = Vector3.forward;
        	} else {
        		cap.transform.eulerAngles = Vector3.forward * 10;
        	}
        
        //Animation
        playerAnimator.SetFloat("Speed",Mathf.Abs(rigidBody.velocity.x));
        playerAnimator.SetBool("OnGround", isTouchingGround);
        
    }

    void FixedUpdate() {
        if (GameObject.FindGameObjectsWithTag("ScoreText").Length > 0){
            GameObject.FindGameObjectsWithTag("ScoreText")[0].GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + score.ToString();
        }
        if (GameObject.FindGameObjectsWithTag("StoneCountText").Length > 0){
            GameObject.FindGameObjectsWithTag("StoneCountText")[0].GetComponent<TMPro.TextMeshProUGUI>().text = stones.ToString();
        }
        
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        
    	if (other.tag == "FallDetector") {
            respawn();
    	} else if (other.tag == "Checkpoint") {
            audioController.playClip(Clip.checkPoint);
            
            respawnPoint = other.transform.position;
        } else if (other.tag == "Coin") {
        	score += 10;
            audioController.playClip(Clip.coin);
        } else if (other.tag == "Bomb") {
        	levelManager.respawn();
            audioController.playClip(Clip.blast);
        } else if (other.tag == "LevelEnd") {
            levelManager.changeLevel();
            audioController.playClip(Clip.gameOver);
        } else if (other.tag == "FireBall") {
            respawn();
        } else if (other.tag == "Ghost") {
            respawn();
        } else if (other.tag == "Saw") {
            respawn();
        }
    }

void respawn() {
    levelManager.respawn();
    audioController.playClip(Clip.gameOver);
}

void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ladybird") {
            levelManager.respawn();
            respawn();
        } else if (other.gameObject.tag == "Stone") {
            Destroy(other.gameObject);
            stones += 1;
        }
        
        
    }
    

    void loadPlayer() {
        PlayerProgress data = SaveSystem.loadPlayer();
        // int level = data.level;
        // levelManager.currentLevel = level;
        score = data.score;
    }

    // public void Throw(Vector3 location, float direction)
    //  {
    //     stone = Instantiate(stone);
    //     stone.transform.position = new Vector3(location.x + 1,location.y, location.z);

    //     Rigidbody2D rigidBody = stone.GetComponent<Rigidbody2D>() ;
    //     float dir = 1;
    //     if (direction < 0) {
    //         dir = -1;
    //     }
    //     rigidBody.velocity = new Vector2(dir * 10f, 0);
    //     Debug.Log(transform.localScale.x);
    //  }
}
