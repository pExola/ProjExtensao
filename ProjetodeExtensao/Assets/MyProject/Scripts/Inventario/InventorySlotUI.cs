using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icone;
    [SerializeField] private TextMeshProUGUI textoQuantidade;

    // (Opcional) Adicionar um "Button" no prefab para
    // detectar cliques e usar o item.
    // [SerializeField] private Button botao; 

    // Limpa o slot (deixa vazio)
    public void LimparSlot()
    {
        icone.enabled = false;
        textoQuantidade.enabled = false;
    }

    // Preenche o slot com dados do item
    public void AtualizarSlot(InventorySlot slot)
    {
        if (slot.item != null)
        {
            icone.sprite = slot.item.icone;
            icone.enabled = true;

            if (slot.item.eEmpilhavel && slot.quantidade > 1)
            {
                textoQuantidade.text = slot.quantidade.ToString();
                textoQuantidade.enabled = true;
            }
            else
            {
                textoQuantidade.enabled = false;
            }
        }
        else
        {
            LimparSlot();
        }
    }
}