using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private Transform containerSlots; 
    [SerializeField] private GameObject slotPrefab; 

    // Lista dos slots visuais (UI)
    private List<InventorySlotUI> slotsUI = new List<InventorySlotUI>();

    private void Start()
    {
        InventoryManager.Instance.OnInventoryChanged += AtualizarUI;

        InicializarUI();
    }

    private void OnDestroy()
    {
        InventoryManager.Instance.OnInventoryChanged -= AtualizarUI;
    }

    private void InicializarUI()
    {
        // Pega o número de slots da lógica
        int numSlots = InventoryManager.Instance.slots.Count;

        for (int i = 0; i < numSlots; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, containerSlots);
            InventorySlotUI slotUI = slotObj.GetComponent<InventorySlotUI>();
            slotsUI.Add(slotUI);
        }

        // Atualiza a UI pela primeira vez
        AtualizarUI();
    }

    // Este método é chamado AUTOMATICAMENTE pelo evento
    private void AtualizarUI()
    {
        List<InventorySlot> slotsLogicos = InventoryManager.Instance.slots;

        for (int i = 0; i < slotsUI.Count; i++)
        {
            if (i < slotsLogicos.Count)
            {
                // Atualiza o slot visual com os dados do slot lógico
                slotsUI[i].AtualizarSlot(slotsLogicos[i]);
            }
        }
    }
}