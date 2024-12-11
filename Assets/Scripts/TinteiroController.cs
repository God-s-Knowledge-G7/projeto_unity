using UnityEngine;

public class TinteiroController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public bool isDead;

    //Verifica se está olha para a direita
    public bool facingRight;
    public bool previousDirectionR;

    //variavel para armazenar posição do player
    private Transform player;

    //variaveis para movimentação
    private float inkSpeed = 0.4f;
    private float currentSpeed;

    private bool isWalking;

    private float xForce;
    private float yForce;

    private float walkTimer;

    public int maxHealth = 20;
    public int currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //busca o player e armazena a posição
        player = FindAnyObjectByType<Player_Base_Controller>().transform;

        //inicializa velocidade e vida
        currentSpeed = inkSpeed;
        currentHealth = maxHealth;
    }

    void Update()
    {
        //verifica se o player está a direita/esquerda e vira o inimigo para o lado que o player está
        if (player.position.x < this.transform.position.x)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }

        if (facingRight && !previousDirectionR)
        {
            this.transform.Rotate(0, 180, 0);
            previousDirectionR = true;
        }
        if (!facingRight && previousDirectionR)
        {
            this.transform.Rotate(0, -180, 0);
            previousDirectionR = false;
        }

        walkTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
            //MOVIMENTAÇÃO

            //Variavel para armezanar a distancia com o player
            Vector3 targetDistance = player.position - transform.position;

          
            xForce = targetDistance.x / Mathf.Abs(targetDistance.x);

            //Entre 1 e 3 seg , será feita uma definição de direção vertical
            if (walkTimer >= Random.Range(1f, 2f))
            {
                yForce = Random.Range(-1, 2);

                walkTimer = 0;
            }

            //parar a movimentação quando estiver perto do player
            if (Mathf.Abs(targetDistance.x) < 0.25)
            {
                xForce = 0;
            }

            //aplica velocidade e faz com que se movimente

            rb.linearVelocity = new Vector2(xForce * currentSpeed, yForce * currentSpeed);
        
    }

    void UptadeAnimator()
    {
        animator.SetBool("isWalking", isWalking);
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = inkSpeed;
    }

    void AtkSpeed()
    {
        currentSpeed = 3f;
    }
}
