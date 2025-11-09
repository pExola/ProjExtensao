using UnityEngine;
using UnityEngine.InputSystem; 

[RequireComponent(typeof(Rigidbody2D), typeof(AtributosPersonagem), typeof(Animator))]
public class ControladorJogador : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    [SerializeField] private float velocidadeCaminhada = 3f;
    [SerializeField] private float velocidadeCorrida = 5f;
    [SerializeField] private float custoStaminaCorrida = 10f; 

    // Referências de componentes
    private Rigidbody2D rb;
    private Animator animator;
    private AtributosPersonagem atributos;
    private PlayerInput playerInput; 

    // Variáveis de estado
    private Vector2 inputMovimento;
    private Vector2 ultimoInputValido = new Vector2(0, -1); 
    private bool estaCorrendo = false;

    // Referências das Ações de Input
    private InputAction acaoMover;
    private InputAction acaoCorrer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        atributos = GetComponent<AtributosPersonagem>();
        playerInput = GetComponent<PlayerInput>();

        acaoMover = playerInput.actions["Move"];
        acaoCorrer = playerInput.actions["Run"];
    }

    private void Update()
    {
        ProcessarInputs();
        AtualizarAnimador();
    }

    private void FixedUpdate()
    {
        MoverPersonagem();
        ConsumirStamina();
    }

    private void ProcessarInputs()
    {
        inputMovimento = acaoMover.ReadValue<Vector2>();

        estaCorrendo = acaoCorrer.IsPressed();

        // Guarda a última direção de movimento para a animação de "Idle"
        if (inputMovimento.sqrMagnitude > 0.01f)
        {
            ultimoInputValido = inputMovimento.normalized;
        }
    }

    private void MoverPersonagem()
    {
        float velocidadeAtual = velocidadeCaminhada;

        if (estaCorrendo && inputMovimento.sqrMagnitude > 0.01f && atributos.TemStamina(0.1f))
        {
            velocidadeAtual = velocidadeCorrida;
        }
        else
        {
            estaCorrendo = false; 
        }

        rb.linearVelocity = inputMovimento.normalized * velocidadeAtual;
    }

    private void ConsumirStamina()
    {
        if (estaCorrendo && inputMovimento.sqrMagnitude > 0.01f)
        {
            // Multiplicamos por Time.fixedDeltaTime para ser um custo por segundo
            atributos.ModificarStamina(-custoStaminaCorrida * Time.fixedDeltaTime);
        }
    }

    private void AtualizarAnimador()
    {
        bool estaSeMovendo = inputMovimento.sqrMagnitude > 0.01f;

        // Atualiza o parâmetro "estaMovendo" do Animator
        animator.SetBool("estaMovendo", estaSeMovendo);

        // Se estiver se movendo, atualiza a direção do Blend Tree
        if (estaSeMovendo)
        {
            animator.SetFloat("moveX", inputMovimento.normalized.x);
            animator.SetFloat("moveY", inputMovimento.normalized.y);
        }
        // Se estiver parado, usa a última direção válida
        else
        {
            animator.SetFloat("moveX", ultimoInputValido.x);
            animator.SetFloat("moveY", ultimoInputValido.y);
        }
    }
}