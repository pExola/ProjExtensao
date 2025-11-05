using UnityEngine;
using UnityEngine.UI; 

public class UIAtributos : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private AtributosPersonagem atributosPersonagem;
    [SerializeField] private Slider barraVida;
    [SerializeField] private Slider barraStamina;

    private void Start()
    {
        if (atributosPersonagem == null)
        {
            Debug.LogError("Referência de AtributosPersonagem não definida na UI!");
            return;
        }

        // Inscreve os métodos nos eventos do personagem
        atributosPersonagem.AoMudarVida += AtualizarBarraVida;
        atributosPersonagem.AoMudarStamina += AtualizarBarraStamina;

        AtualizarBarraVida(atributosPersonagem.VidaAtual, 100f); // 100f é placeholder
        AtualizarBarraStamina(atributosPersonagem.StaminaAtual, 100f);
    }

    private void OnDestroy()
    {
        if (atributosPersonagem != null)
        {
            atributosPersonagem.AoMudarVida -= AtualizarBarraVida;
            atributosPersonagem.AoMudarStamina -= AtualizarBarraStamina;
        }
    }

    private void AtualizarBarraVida(float vidaAtual, float vidaMaxima)
    {
        if (barraVida != null)
        {
            barraVida.value = vidaAtual / vidaMaxima;
        }
    }

    private void AtualizarBarraStamina(float staminaAtual, float staminaMaxima)
    {
        if (barraStamina != null)
        {
            barraStamina.value = staminaAtual / staminaMaxima;
        }
    }
}