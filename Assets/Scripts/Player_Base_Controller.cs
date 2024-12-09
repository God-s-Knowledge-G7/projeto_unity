using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Base_Controller : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;

    //Variaveis para movimentação
    public float playerSpeed = 1f;
    public float currentSpeed;

    public Vector2 playerDirection;

    private bool Walking;
    private bool FacingRight = true;


    //Variaveis para ataque
    private bool attackControl;
    private float atkTime = 0.6f;
    public float nextAtk;

    private bool isDefending;

    //Vida
    public int maxHealth = 20;
    public int currentHealth;

    public Sprite PlayerPortrait;
    
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();

        currentSpeed = playerSpeed;

        currentHealth = maxHealth;

        
    }

    
    void Update()
    {
        PlayerMove();

        UpdateAnimator();

        //Dash player
        if (Input.GetKeyUp(KeyCode.L) && (playerDirection.x != 0 || playerDirection.y != 0))
        {
            playerDash();
        }

        //Ataque player
        if (Input.GetKeyDown(KeyCode.J) && Time.time > nextAtk)
        {
            zeroSpeed();
            playerAtk();

            nextAtk = Time.time + atkTime;
        }

        //Defesa player
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartDefending();
            zeroSpeed();
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            EndDefending();
        }
    }

    private void FixedUpdate()
    { 
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            Walking = true;

        }
        else
        {
            Walking = false;
        }

        playerRigidBody.MovePosition(playerRigidBody.position + currentSpeed * Time.fixedDeltaTime * playerDirection);

        
    }

    void PlayerMove()
    {
        //Pega o input do jogador, e cria um Vector2 para usar no PlayerDirection
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (playerDirection.x < 0 && FacingRight)
        {
            flip();
        }

        else if (playerDirection.x > 0 && FacingRight == false)
        {
            flip();
        }
    }
    void UpdateAnimator()
    {
        playerAnimator.SetBool("Walking", Walking);
    }

    void flip()
    {
        FacingRight = !FacingRight;

        transform.Rotate(0, 180, 0);
    }

    void playerAtk()
    {
        playerAnimator.SetTrigger("Attacking");
    }

    void StartDefending ()
    {
        playerAnimator.SetTrigger("DefendON");
    }

    void EndDefending()
    {
        playerAnimator.SetTrigger("DefendOff");
    }

    void playerDash()
    {
        playerAnimator.SetTrigger("Dash");
    }

    void DashSpeed()
    {
        currentSpeed = 3.5f;
    }

    void zeroSpeed()
    {
        currentSpeed = 0;
    }

    void resetSpeed()
    {
        currentSpeed = playerSpeed;
    }
}
