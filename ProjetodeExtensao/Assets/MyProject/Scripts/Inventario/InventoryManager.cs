using UnityEngine;
using System.Collections.Generic;
using System; 

public class InventoryManager : MonoBehaviour
{
    // --- Singleton Pattern ---
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            InitializeInventory();
        }
    }
    // --- Fim do Singleton ---

    [Header("Configuração")]
    [SerializeField] private int tamanhoInventario = 20;

    public List<InventorySlot> slots;

    // EVENTO: A UI vai "ouvir" isso.
    // Quando o inventário mudar, ele avisa a UI para se redesenhar.
    public event Action OnInventoryChanged;


    private void InitializeInventory()
    {
        slots = new List<InventorySlot>(tamanhoInventario);
        for (int i = 0; i < tamanhoInventario; i++)
        {
            slots.Add(new InventorySlot(null, 0)); // Adiciona slots vazios
        }
    }

    public bool AdicionarItem(ItemData item, int quantidade)
    {
        int quantidadeRestante = quantidade;

        // 1. Tenta empilhar em slots existentes
        if (item.eEmpilhavel)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == item)
                {
                    quantidadeRestante = slot.AdicionarQuantidade(quantidadeRestante);
                    if (quantidadeRestante == 0) break;
                }
            }
        }

        if (quantidadeRestante == 0)
        {
            OnInventoryChanged?.Invoke(); 
            return true; 
        }

        // 2. Tenta adicionar em um slot vazio
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == null)
            {
                // Trata o excesso (caso a quantidade seja > maxPilha)
                if (item.eEmpilhavel && quantidadeRestante > item.maximoPilha)
                {
                    slot.item = item;
                    slot.quantidade = item.maximoPilha;
                    quantidadeRestante -= item.maximoPilha;
                }
                else
                {
                    slot.item = item;
                    slot.quantidade = quantidadeRestante;
                    quantidadeRestante = 0;
                    break; // Encontrou um slot, terminou
                }
            }
        }

        OnInventoryChanged?.Invoke(); 

        if (quantidadeRestante > 0)
        {
            Debug.Log("Inventário cheio. Sobraram " + quantidadeRestante);
            return false; // Inventário está cheio
        }

        return true;
    }

    // (Futuro) Função para escalar
    public void SwapSlots(int indexA, int indexB)
    {
        InventorySlot temp = slots[indexA];
        slots[indexA] = slots[indexB];
        slots[indexB] = temp;

        OnInventoryChanged?.Invoke(); // Avisa a UI que trocou
    }
}