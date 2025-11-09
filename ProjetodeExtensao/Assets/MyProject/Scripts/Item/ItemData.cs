using UnityEngine;

// Isso cria um menu no Unity: Assets > Create > Jogo > Item
[CreateAssetMenu(fileName = "NovoItem", menuName = "Jogo/Item")]
public class ItemData : ScriptableObject
{
    [Header("Informações Básicas")]
    public string nomeItem;
    [TextArea(3, 5)]
    public string descricao;
    public Sprite icone;

    [Header("Configurações")]
    public bool eEmpilhavel;
    public int maximoPilha = 99;

    // VIRTUAL: Permite que itens futuros (ex: ItemPocao)
    // sobrescrevam esta função para ter um comportamento único.
    public virtual void Usar()
    {
        Debug.Log($"Usando item: {nomeItem}");
    }
}