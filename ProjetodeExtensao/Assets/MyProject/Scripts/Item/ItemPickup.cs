using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))] 
public class ItemPickup : MonoBehaviour, IInteractable
{
    [Header("Item")]
    [SerializeField] private ItemData itemData; 
    [SerializeField] private int quantidade = 1;

    private void Awake()
    {
        // Garante que o colisor é um trigger
        GetComponent<Collider2D>().isTrigger = true;
    }

    public string GetPrompt()
    {
        return $"Coletar {itemData.nomeItem} (x{quantidade})";
    }

    public void Interact()
    {
        // Tenta adicionar ao inventário
        bool sucesso = InventoryManager.Instance.AdicionarItem(itemData, quantidade);

        if (sucesso)
        {
            Destroy(gameObject);
        }
    }
}