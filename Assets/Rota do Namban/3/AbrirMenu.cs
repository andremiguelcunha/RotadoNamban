using UnityEngine;

public class AbrirMenu : MonoBehaviour
{
    [Header("Referências")]
    public GameObject fundoTransparente; // Fundo escurecido ou translúcido por trás do menu
    public GameObject painelMenu;        // Painel principal do menu (com botões, texto, etc.)

    // Método para abrir o menu
    public void Abrir()
    {
        fundoTransparente.SetActive(true);
        painelMenu.SetActive(true);
    }

    // Método para fechar o menu
    public void Fechar()
    {
        fundoTransparente.SetActive(false);
        painelMenu.SetActive(false);
    }
}
