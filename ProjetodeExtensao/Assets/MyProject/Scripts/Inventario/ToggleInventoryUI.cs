using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class ToggleInventoryUI : MonoBehaviour
{
    [Tooltip("O GameObject do painel que será ligado/desligado")]
    [SerializeField] private GameObject painelInventario;

    private PlayerInput playerInput;
    private InputAction acaoInventario;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        acaoInventario = playerInput.actions["OpenInventory"];

        if (painelInventario == null)
        {
            Debug.LogError("Painel de Inventário não foi definido no script ToggleInventoryUI!");
        }
    }

    private void OnEnable()
    {
        acaoInventario.performed += OnToggleInventory;
    }

    private void OnDisable()
    {
        acaoInventario.performed -= OnToggleInventory;
    }

    // Este método é chamado quando o botão é pressionado
    private void OnToggleInventory(InputAction.CallbackContext context)
    {
        if (painelInventario != null)
        {
            bool isVisible = painelInventario.activeSelf;

            painelInventario.SetActive(!isVisible);
        }
    }
}