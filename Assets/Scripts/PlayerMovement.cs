using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float maxGroundDistance;
    [SerializeField] private int maxJumpCount = 2;
    [SerializeField] private float minYThreshhold, yReappearPosition;

    public bool _isGrounded;
    public float _inputX;

    private bool atWall;
    private Rigidbody2D rb;
    private Vector2 movement;
    private int jumpCount;
    private int playerMoveNumber;
    private PlayerManager playerManager;
    private Animator animator;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
        playerMoveNumber = playerManager.playerNumber;
        jumpCount = 0;
    }

    private void Update()
    {
        if (GameManager.self.isGameOver)
            return;

        if(isGrounded)
        {
            jumpCount = 0;
            if(animator!=null)
            {
                animator.SetTrigger("IsLanded");
                animator.ResetTrigger("IsLanding");
            }
        }
        

        if(Input.GetButtonDown("Jump"+playerMoveNumber)&&jumpCount<maxJumpCount)
        {
            Jump(jumpHeight);
            jumpCount++;
        }

        if (rb.velocity.y < 0 && animator!=null)
        {
            animator.SetTrigger("IsLanding");
            animator.ResetTrigger("IsJumping");
            animator.ResetTrigger("IsLanded");
        }
        else if (rb.velocity.y > 0 && animator != null && !isGrounded)
        {
            animator.SetTrigger("IsJumping");
            animator.ResetTrigger("IsLanding");
            animator.ResetTrigger("IsLanded");
        }

        _inputX = Input.GetAxis("Horizontal" + playerMoveNumber);
        //Move(Input.GetAxis("Horizontal" + playerMoveNumber) * speed*Time.deltaTime);
        Move(_inputX * speed * Time.deltaTime);

        if(transform.position.y < minYThreshhold)
        {
            transform.position = new Vector2(transform.position.x,yReappearPosition);
        }
    }
    

    public void Move(float inputX)
    {
        rb.velocity = new Vector2(inputX, rb.velocity.y);
    }
    
    public void Jump(float height)
    {
        rb.velocity = new Vector2(rb.velocity.x,height);
    }

    public bool isGrounded
    {
        get
        {
            Vector3 offset = Vector3.down * (maxGroundDistance *(3.0f / 4.0f));
            Vector3 startPosition = transform.position + offset;
            float distance = maxGroundDistance / 3;
            Debug.DrawRay(startPosition, Vector2.down * distance, Color.green);
            if (Physics2D.Raycast(startPosition, Vector2.down * maxGroundDistance, distance, LayerMask.GetMask("Platform")) /*&& Mathf.Abs(rb.velocity.y) <=0.01f*/)
            {
                _isGrounded = true;
                return true;
            }
            _isGrounded = false;
            return false;
        }
    }
}
