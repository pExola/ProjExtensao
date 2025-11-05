using UnityEngine;
using System; 

public class AtributosPersonagem : MonoBehaviour
{
    [Header("Configurações de Vida")]
    [SerializeField] private float vidaMaxima = 100f;
    private float vidaAtual;

    [Header("Configurações de Stamina")]
    [SerializeField] private float staminaMaxima = 100f;
    [SerializeField] private float taxaRegeneracaoStamina = 5f;
    private float staminaAtual;
    private bool podeRegenerarStamina = true;

    // A UI e outros sistemas vão "ouvir" esses eventos
    public event Action<float, float> AoMudarVida; // (atual, max)
    public event Action<float, float> AoMudarStamina; // (atual, max)
    public event Action AoMorrer;

    public float VidaAtual => vidaAtual;
    public float StaminaAtual => staminaAtual;

    private void Start()
    {
        vidaAtual = vidaMaxima;
        staminaAtual = staminaMaxima;

        // Dispara os eventos uma vez para a UI inicializar correta
        AoMudarVida?.Invoke(vidaAtual, vidaMaxima);
        AoMudarStamina?.Invoke(staminaAtual, staminaMaxima);
    }

    private void Update()
    {
        // Regenera stamina ao longo do tempo se não estiver sendo usada
        if (podeRegenerarStamina && staminaAtual < staminaMaxima)
        {
            ModificarStamina(taxaRegeneracaoStamina * Time.deltaTime);
        }
    }

    // --- MÉTODOS PÚBLICOS DE MODIFICAÇÃO ---

    /// <summary>
    /// Modifica a vida. Positivo para curar, negativo para danificar.
    /// </summary>
    public void ModificarVida(float quantidade)
    {
        vidaAtual = Mathf.Clamp(vidaAtual + quantidade, 0, vidaMaxima);

        AoMudarVida?.Invoke(vidaAtual, vidaMaxima);

        if (vidaAtual <= 0)
        {
            AoMorrer?.Invoke();
            // Lógica de morte etc
        }
    }

    /// <summary>
    /// Modifica a stamina. Positivo para recuperar, negativo para gastar.
    /// </summary>
    public void ModificarStamina(float quantidade)
    {
        staminaAtual = Mathf.Clamp(staminaAtual + quantidade, 0, staminaMaxima);

        AoMudarStamina?.Invoke(staminaAtual, staminaMaxima);

        // Se gastamos stamina, paramos a regeneração por um momento
        if (quantidade < 0)
        {
            podeRegenerarStamina = false;
            // Cancela qualquer "Invoke" anterior para reiniciar o timer
            CancelInvoke(nameof(ReiniciarRegeneracaoStamina));
            // Aguarda X segundos antes de voltar a regenerar
            Invoke(nameof(ReiniciarRegeneracaoStamina), 1.5f);
        }
    }

    /// <summary>
    /// Verifica se o personagem tem uma quantidade X de stamina.
    /// </summary>
    public bool TemStamina(float quantidadeNecessaria)
    {
        return staminaAtual >= quantidadeNecessaria;
    }

    private void ReiniciarRegeneracaoStamina()
    {
        podeRegenerarStamina = true;
    }
}