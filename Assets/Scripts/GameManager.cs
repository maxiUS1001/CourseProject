using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject elementsPanel;
    public GameObject tablePanel;
    public GameObject showElementsButton;

    public void ShowElementsPanel()
    {
        if (!elementsPanel.activeSelf)
        {
            elementsPanel.SetActive(true);
        }
        else
        {
            elementsPanel.SetActive(false);
        }
    }

    public void ShowTablePanel()
    {
        if (tablePanel.activeSelf)
        {
            tablePanel.SetActive(false);
            showElementsButton.SetActive(true);
        }
        else
        {
            tablePanel.SetActive(true);
            showElementsButton.SetActive(false);
        }
    }
}
