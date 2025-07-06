using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ARModelSwitcher : MonoBehaviour
{
    public GameObject burger;  // Referência ao modelo do hambúrguer
    public GameObject banana;  // Referência ao modelo da banana
    public TextMeshProUGUI buttonText; // Texto do botão
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SwitchModel);

        // Garante que o hambúrguer começa visível e a banana oculta
        burger.SetActive(true);
        banana.SetActive(false);
        buttonText.text = "Mostrar Banana";
    }

    void SwitchModel()
    {
        bool isBurgerActive = burger.activeSelf;

        // Alterna entre os modelos
        burger.SetActive(!isBurgerActive);
        banana.SetActive(isBurgerActive);

        // Atualiza o texto do botão
        buttonText.text = isBurgerActive ? "Mostrar Hambúrguer" : "Mostrar Banana";
    }
}