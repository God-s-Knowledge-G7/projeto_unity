using Unity.VisualScripting;
using UnityEngine;

public class Player_Base_Controller : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;

    public float playerSpeed = 1f;

    public Vector2 playerDirection;

    private bool Walking;
    private bool FacingRight = true;

    private Animator playerAnimator;
    
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        PlayerMove();

        UpdateAnimator();

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

        playerRigidBody.MovePosition(playerRigidBody.position + playerSpeed * Time.fixedDeltaTime * playerDirection);

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
}
