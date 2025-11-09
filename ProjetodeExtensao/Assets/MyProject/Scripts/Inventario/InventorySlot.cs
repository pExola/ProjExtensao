using System;

[Serializable] 
public class InventorySlot
{
    public ItemData item;
    public int quantidade;

    public InventorySlot(ItemData item, int quantidade)
    {
        this.item = item;
        this.quantidade = quantidade;
    }

    // Adiciona quantidade e retorna o "excesso" se a pilha estiver cheia
    public int AdicionarQuantidade(int valor)
    {
        if (item == null || !item.eEmpilhavel) return valor; // Não pode adicionar

        int novaQuantidade = quantidade + valor;
        if (novaQuantidade > item.maximoPilha)
        {
            quantidade = item.maximoPilha;
            return novaQuantidade - item.maximoPilha; // Retorna o que sobrou
        }

        quantidade = novaQuantidade;
        return 0; // Conseguiu adicionar tudo
    }
}