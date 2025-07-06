using UnityEngine;
using System.Collections.Generic;

// Este script configura automaticamente a posição e os degraus das contas inferiores (à esquerda e à direita).
// This script automatically configures the position and steps of lower beads (left and right).

public class BeadAutoConfigurator : MonoBehaviour
{
    [Header("Contas Inferiores")]
    public BeadController[] contasDireita;   // Contas da coluna direita
    public BeadController[] contasEsquerda;  // Contas da coluna esquerda

    [Header("Configuração de Escada")]
    public float minY = -562.1f;    // Posição mais baixa (bottom)
    public float maxY = -39.7f;     // Posição mais alta (top)
    public int steps = 6;          // Número de degraus/interações possíveis (steps on the abacus)

    void Start()
    {
        // Calcula as posições verticais entre minY e maxY
        // Calculates vertical positions between minY and maxY
        List<float> positionsY = CalcularDegraus(minY, maxY, steps);

        // Aplica as posições às duas colunas de contas
        // Applies the positions to both bead columns
        ConfigurarColuna(contasDireita, positionsY);
        ConfigurarColuna(contasEsquerda, positionsY);
    }

    // Gera uma lista de posições verticais com espaçamento uniforme
    // Generates a list of evenly spaced Y positions
    List<float> CalcularDegraus(float minY, float maxY, int total)
    {
        List<float> list = new List<float>();
        float stepHeight = (maxY - minY) / (total - 1);

        for (int i = 0; i < total; i++)
        {
            float y = minY + (stepHeight * i);
            list.Add(y);
        }

        return list;
    }

    // Aplica as posições a cada conta da coluna
    // Applies the calculated positions to each bead in a column
    void ConfigurarColuna(BeadController[] contas, List<float> positionsY)
    {
        for (int i = 0; i < contas.Length; i++)
        {
            var bead = contas[i];
            bead.index = i;
            bead.group = contas;
            bead.positionsY = new List<float>(positionsY); // Cria uma cópia da lista

            // Define o degrau mais próximo da posição inicial da conta
            // Finds the closest step to the current Y position of the bead
            float atualY = bead.GetY();
            float maisProximo = positionsY[0];
            int passoMaisProximo = 0;

            for (int j = 1; j < positionsY.Count; j++)
            {
                if (Mathf.Abs(atualY - positionsY[j]) < Mathf.Abs(atualY - maisProximo))
                {
                    maisProximo = positionsY[j];
                    passoMaisProximo = j;
                }
            }

            bead.currentStep = passoMaisProximo;
        }
    }
}
