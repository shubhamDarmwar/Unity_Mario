using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;




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
    private GameControls gameControls;

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
        if (gameControls == null){
            gameControls = FindObjectOfType<GameControls>();
        }
        
        if (GameObject.FindGameObjectsWithTag("ScoreText").Length > 0){
            GameObject.FindGameObjectsWithTag("ScoreText")[0].GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + score.ToString();
        }
        if (GameObject.FindGameObjectsWithTag("StoneCountText").Length > 0){
            GameObject.FindGameObjectsWithTag("StoneCountText")[0].GetComponent<TMPro.TextMeshProUGUI>().text = stones.ToString();
        }
        
        
    }

    public void resetPlayer() {
        stones = 3;
        score =0;
    }
    void OnTriggerEnter2D(Collider2D other) {
        
    	if (other.tag == "FallDetector") {
            accident();
            audioController.playClip(Clip.gameOver);
    	} else if (other.tag == "Checkpoint") {
            levelManager.checkpointReached = true;
            
            if (!other.gameObject.GetComponent<Checkpoint>().isChecked) {
                audioController.playClip(Clip.checkPoint);
            }
            
            
            respawnPoint = other.transform.position;
            GoogleAds.loadAdsIfNotLoaded();
        } else if (other.tag == "Coin") {
        	score += 10;
            audioController.playClip(Clip.coin);
        } else if (other.tag == "Bomb") {
        	accident();
            audioController.playClip(Clip.blast);
        } else if (other.tag == "LevelEnd") {
            levelManager.changeLevel();
            audioController.playClip(Clip.checkPoint);
        } else if (other.tag == "FireBall") {
            accident();
            audioController.playClip(Clip.gameOver);
        } else if (other.tag == "Ghost") {
            accident();
            audioController.playClip(Clip.gameOver);
        } else if (other.tag == "Saw") {
            accident();
            audioController.playClip(Clip.gameOver);
        }   
    }

void accident() {
    levelManager.respawn();

}

void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.tag == "Ladybird") {
        accident();
        audioController.playClip(Clip.gameOver);
    } else if (other.gameObject.tag == "Stone") {
        if (!other.gameObject.GetComponent<Stone>().thrown) {
            Destroy(other.gameObject);
            stones += 1;
            audioController.playClip(Clip.stoneCollected);
        }
        
    }
        
}
    

void loadPlayer() {
    PlayerProgress data = SaveSystem.loadPlayer();
    score = data.score;
}

}
