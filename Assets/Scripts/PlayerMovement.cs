using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float maxGroundDistance;
    [SerializeField] private int maxJumpCount = 2;

    private bool _isGrounded;
    private bool atWall;
    private Rigidbody2D rb;
    private Vector2 movement;
    private int jumpCount;
    private int playerMoveNumber;
    private PlayerManager playerManager;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        playerManager = GetComponent<PlayerManager>();
        playerMoveNumber = playerManager.playerNumber;
        jumpCount = 0;
    }

    private void Update()
    {

        if(isGrounded)
        {
            jumpCount = 0;
        }
        

        if(Input.GetButtonDown("Jump"+playerMoveNumber)&&jumpCount<maxJumpCount)
        {
            Jump(jumpHeight);
            jumpCount++;
        }
    }

    void FixedUpdate () {

        Move(Input.GetAxis("Horizontal"+playerMoveNumber)*speed);
	}

    public void Move(float inputX)
    {
        rb.velocity = new Vector2(inputX, rb.velocity.y);
    }
    
    public void Jump(float heigt)
    {
        rb.velocity = new Vector2(rb.velocity.x,heigt);
    }

    public bool isGrounded
    {
        get
        {
            Vector3 offset = Vector3.down * (maxGroundDistance *(3.0f / 4.0f));
            Vector3 startPosition = transform.position + offset;
            float distance = maxGroundDistance / 3;
            Debug.DrawRay(startPosition, Vector2.down * distance, Color.green);
            if (Physics2D.Raycast(startPosition, Vector2.down * maxGroundDistance, distance, LayerMask.GetMask("Platform")) && Mathf.Abs(rb.velocity.y) <=0.001f)
            {
                return true;
            }
            return false;
        }
    }
}
