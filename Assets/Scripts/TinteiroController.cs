using UnityEngine;

public class TinteiroController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    public bool isDead;

    //Verifica se est� olha para a direita
    public bool facingRight;
    public bool previousDirectionR;

    //variavel para armazenar posi��o do player
    private Transform player;

    //variaveis para movimenta��o
    private float inkSpeed = 0.4f;
    private float currentSpeed;

    private bool isWalking;

    private float xForce;
    private float yForce;

    private float walkTimer;

    //variaveis para ataque
    private float nextAtk;
    private float atkRate = 3f;
    private float atkDestiny = 5f;

    public int maxHealth = 20;
    public int currentHealth;


    public Vector3 targetDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //busca o player e armazena a posi��o
        player = FindAnyObjectByType<Player_Base_Controller>().transform;

        //inicializa velocidade e vida
        currentSpeed = inkSpeed;
        currentHealth = maxHealth;
    }

    void Update()
    {
        //verifica se o player est� a direita/esquerda e vira o inimigo para o lado que o player est�
        if (xForce < 0)
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
        if (!isDead)
        {
            //MOVIMENTA��O

            //Variavel para armezanar a distancia com o player
            targetDistance = player.position - transform.position;

          
            //xForce = targetDistance.x / Mathf.Abs(targetDistance.x);

            
            
            //Entre 1 e 3 seg , ser� feita uma defini��o de dire��o vertical
            if (walkTimer >= Random.Range(2f, 3f))
            {
                yForce = Random.Range(-1, 2);
                xForce = Random.Range(-1, 2);

                walkTimer = 0;
            }

            
            //aplica velocidade e faz com que se movimente
            Moviment();
           

            //ATAQUE

            if (Mathf.Abs(targetDistance.x) < 0.9f && Mathf.Abs(targetDistance.y) <= 0.25f)
            {
                print("PErtooo");
                
                animator.SetTrigger("Attack");

                ZeroSpeed();
            }

        }

    }

    void UptadeAnimator()
    {
        animator.SetBool("isWalking", isWalking);
    }

    void Moviment()
    {
        rb.linearVelocity = new Vector2(xForce * currentSpeed, yForce * currentSpeed);
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = inkSpeed;
        
    }

    void AtkDash()
    {
        currentSpeed = 5f;

        rb.linearVelocity = new Vector2(1 * currentSpeed, 0);

        
    }
}
