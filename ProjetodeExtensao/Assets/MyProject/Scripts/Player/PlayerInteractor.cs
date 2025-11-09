using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float raioInteracao = 0.5f;
    [SerializeField] private LayerMask camadaInteragivel;

    private IInteractable itemProximo;
    private PlayerInput playerInput;
    private InputAction acaoInteragir;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        acaoInteragir = playerInput.actions["Interact"]; 
    }

    private void OnEnable()
    {
        acaoInteragir.performed += OnInteract;
    }

    private void OnDisable()
    {
        acaoInteragir.performed -= OnInteract;
    }

    private void Update()
    {
        // Procura por itens interativos em um círculo ao redor do jogador
        Collider2D colisor = Physics2D.OverlapCircle(transform.position, raioInteracao, camadaInteragivel);

        if (colisor != null)
        {
            // Tenta pegar o componente 
            itemProximo = colisor.GetComponent<IInteractable>();

            if (itemProximo != null)
            {
                // TODO: Mostrar UI de prompt (ex: "E) Coletar")
                // Debug.Log(itemProximo.GetPrompt()); 
            }
        }
        else
        {
            itemProximo = null;
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (itemProximo != null)
        {
            itemProximo.Interact();
            itemProximo = null; 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raioInteracao);
    }
}