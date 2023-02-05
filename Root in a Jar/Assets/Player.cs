using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    private Transform player;
    private Camera mainCamera;
    private int playerTopScore;
    private int playerDeathDistance;

    public TextMeshProUGUI scoreText;

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayers;

    private Rigidbody2D rigidBody;
    private bool isGrounded;
    private bool isJumping;

    void Start()
    {
        
        mainCamera = Camera.main;
        player = gameObject.transform;
        rigidBody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(gameObject.transform.position, groundCheckRadius, groundLayers);
        if (isGrounded==true)
        {
            isJumping = false;
        }

        if (!isJumping && isGrounded)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            isJumping = true;
        }
        UpdatePlayerPosition();

        if (transform.position.y<playerDeathDistance)
        {
            StopAllCoroutines();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }


    void UpdatePlayerPosition()
    {
        if ((int)transform.position.y> playerTopScore)
        {
            playerTopScore = (int)transform.position.y;
            playerDeathDistance = playerTopScore - 10;
            scoreText.text = playerTopScore.ToString();
        }
        
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(horizontalInput * moveSpeed, rigidBody.velocity.y);


    }
}
