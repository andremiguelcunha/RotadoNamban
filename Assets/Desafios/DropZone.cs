using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DropZone : MonoBehaviour, IDropHandler
{
    public GameObject feedbackPanel;
    public TextMeshProUGUI feedbackText;

    public GameObject finalPanel; // Parabéns Panel

    private int correctCount = 0;
    private int totalCorrectRequired = 6;

    private HashSet<string> droppedCorrectItems = new HashSet<string>();

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        string itemName = dropped.name;

        bool isCorrect = false;
        string feedbackMessage = "";

        switch (itemName)
        {
            case "Espelho":
                feedbackMessage = "Espelhos eram objetos de luxo e muito valorizados no Japão.";
                isCorrect = true;
                break;
            case "Pimenta":
                feedbackMessage = "As especiarias, como a pimenta, eram raras e valiosas.";
                isCorrect = true;
                break;
            case "Tecido":
                feedbackMessage = "Tecidos exóticos eram apreciados na cultura japonesa.";
                isCorrect = true;
                break;
            case "Armas":
                feedbackMessage = "Armas de fogo revolucionaram a guerra no Japão.";
                isCorrect = true;
                break;
            case "Macaco":
                feedbackMessage = "Macacos exóticos eram vistos como presentes curiosos e valiosos.";
                isCorrect = true;
                break;
            case "Tabaco":
                feedbackMessage = "O tabaco, trazido pelos portugueses, espalhou-se rapidamente e tornou-se um hábito popular.";
                isCorrect = true;
                break;
            default:
                feedbackMessage = "Este tipo de objeto não atravessou os mares com os navegadores portugueses!";
                break;
        }

        ShowFeedback(feedbackMessage);
        dropped.GetComponent<DragItem>().SetDroppedInZone(isCorrect);

        if (isCorrect && !droppedCorrectItems.Contains(itemName))
        {
            droppedCorrectItems.Add(itemName);
            correctCount++;
            CheckCompletion();
        }
    }

    private void ShowFeedback(string message)
    {
        feedbackPanel.SetActive(true);
        feedbackText.text = message;
    }

    private void CheckCompletion()
    {
        if (correctCount >= totalCorrectRequired)
        {
            Debug.Log("Todos os objetos corretos foram entregues!");
            finalPanel.SetActive(true); // Mostrar painel de parabéns
        }
    }

    public void OnContinueButtonPressed()
    {
        SceneManager.LoadScene(2); // Voltar à cena inicial
    }
}
