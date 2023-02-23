using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Tasks : MonoBehaviour
{
    public Light bulbLight;
    public GameObject startButton;
    public GameObject restartButton;
    public GameObject endButton;
    public GameObject restartLabButton;
    public GameObject fillInTheTableButton;
    public List<TextMeshProUGUI> r_Cu;
    public List<TextMeshProUGUI> r_W;
    public List<TextMeshProUGUI> electricConduction;
    public List<TextMeshProUGUI> thermalConduction;
    List<string> thermalConductionBuf = new List<string>
    {
        "384", "382,7", "381,7", "380,7", "379,8", "378,8", "378", "377,3"
    };
    public List<TextMeshProUGUI> ratio;

    public TextMeshProUGUI kiloometrValues;
    public TextMeshProUGUI taskText;
    public GameObject taskPanel;
    public TextMeshProUGUI temperatureText;
    int temperature = 0;
    float increment;
    bool task4Done;
    public bool task9Done;

    AnimationManager animScript;

    public void Task1() // turn on the device
    {
        startButton.SetActive(false);
        taskPanel.SetActive(true);
        animScript = GameObject.Find("Box015").GetComponent<AnimationManager>();
        animScript.canPress = true;
        taskText.text = "Включите программируемую электрическую печь нажатием на выключатель";
    }

    public void Task2() // close the door
    {
        taskText.text = "Закройте дверцу печи";
        animScript = GameObject.Find("Door").GetComponent<AnimationManager>();
        animScript.canOpen = true;
    }

    public void Task3() // start heating
    {
        taskText.text = "Включите нагревание печи через температурный регулятор";
        animScript = GameObject.Find("Line001").GetComponent<AnimationManager>();
        animScript.canStartHeating = true;
        StartCoroutine(WaitForStartHeatingButton(r_Cu));
    }

    public void Task4() //
    {
        taskText.text = "Поставьте переключатель в положение над W";
        animScript = GameObject.Find("Sphere001").GetComponent<AnimationManager>();
        animScript.canSwitch = true;
    }

    public void Task5() //
    {
        taskText.text = "Включите нагревание печи через температурный регулятор";
        animScript = GameObject.Find("Line001").GetComponent<AnimationManager>();
        animScript.canStartHeating = true;
        StartCoroutine(WaitForStartHeatingButton(r_W));
    }

    public void Task6()
    {
        taskText.text = "Все значения в таблице высчитываются автоматически после окончания измерений. Для расчета нажмите на соответствующую кнопку либо повторите измерения";
        restartButton.SetActive(true);
        fillInTheTableButton.SetActive(true);
    }

    public void Task7()
    {
        FillInTheTable();
        fillInTheTableButton.SetActive(false);
        endButton.SetActive(true);
    }

    public void Task8()
    {
        endButton.SetActive(false);
        restartButton.SetActive(false);
        taskText.text = "Отключите программируемую электрическую печь нажатием на выключатель";
        animScript = GameObject.Find("Box015").GetComponent<AnimationManager>();
        animScript.canPress = true;

        task9Done = true;
    }

    public void Task9()
    {
        taskText.text = "Откройте дверцу печи";
        animScript = GameObject.Find("Door").GetComponent<AnimationManager>();
        animScript.canOpen = true;  
    }

    public void Task10()
    {
        taskPanel.SetActive(false);
        restartLabButton.SetActive(true);
        //animScript.task3Done = false;

        //animScript = GameObject.Find("Box015").GetComponent<AnimationManager>();
        //animScript.task2Done = false;

        //startButton.SetActive(true);
    }

    IEnumerator WaitForStartHeatingButton(List<TextMeshProUGUI> rValues)
    {
        yield return new WaitUntil(() => animScript.canStartHeating == false);
        temperatureText.gameObject.SetActive(true);
        kiloometrValues.gameObject.SetActive(true);
        StartCoroutine(StartHeating(rValues));
    }

    IEnumerator StartHeating(List<TextMeshProUGUI> rValues)
    {
        bulbLight.enabled = true;
        taskText.text = "Нагревание проводится до 100 °C. Значения сопротивлений заносятся в таблицу автоматически";
        while (temperature < 100)
        {
            yield return new WaitForSeconds(0.2f);
            temperature += 1;
            temperatureText.text = $"{temperature} °C";
            KiloometrValues(rValues, temperature);
        }

        bulbLight.enabled = false;
        temperatureText.gameObject.SetActive(false);
        kiloometrValues.gameObject.SetActive(false);
        kiloometrValues.text = "0";
        temperature = 0;

        var anim = GameObject.Find("Line001").GetComponent<Animator>();
        var heatingState = !anim.GetBool("IsHeating");
        anim.SetBool("IsHeating", heatingState);

        temperatureText.text = $"{temperature} °C";
        increment += 11;

        if (task4Done)
        {
            Task6();
        }
        else
        {
            task4Done = true;
            Task4();
        }
    }

    void KiloometrValues(List<TextMeshProUGUI> rValues, int temperature)
    {
        if (temperature == 25)
        {
            rValues[0].text = Math.Round(Random.Range(37.5f + increment, 37.9f + increment), 2).ToString();
            Increment();
            kiloometrValues.text = rValues[0].text;
        }
        if (temperature == 35)
        {
            rValues[1].text = Math.Round(Random.Range(38.0f + increment, 38.4f + increment), 2).ToString();
            Increment();
            kiloometrValues.text = rValues[1].text;
        }
        if (temperature == 45)
        {
            rValues[2].text = Math.Round(Random.Range(38.5f + increment, 39.2f + increment), 2).ToString();
            Increment();
            kiloometrValues.text = rValues[2].text;
        }
        if (temperature == 55)
        {
            rValues[3].text = Math.Round(Random.Range(39.3f + increment, 40.0f + increment), 2).ToString();
            Increment();
            kiloometrValues.text = rValues[3].text;
        }
        if (temperature == 65)
        {
            rValues[4].text = Math.Round(Random.Range(40.1f + increment, 41.1f + increment), 2).ToString();
            Increment();
            kiloometrValues.text = rValues[4].text;
        }
        if (temperature == 75)
        {
            rValues[5].text = Math.Round(Random.Range(41.2f + increment, 42.0f + increment), 2).ToString();
            Increment(1.1f);
            kiloometrValues.text = rValues[5].text;
        }
        if (temperature == 85)
        {
            rValues[6].text = Math.Round(Random.Range(42.1f + increment, 43.0f + increment), 2).ToString();
            Increment(0.3f);
            kiloometrValues.text = rValues[6].text;
        }
        if (temperature == 95)
        {
            rValues[7].text = Math.Round(Random.Range(43.1f + increment, 44.0f + increment), 2).ToString();
            kiloometrValues.text = rValues[7].text;
        }
    }

    void Increment(float n = 0)
    {
        if (task4Done)
        {
            increment += 0.35f + n;
        }
    }

    void FillInTheTable()
    {
        for (int i = 0; i < electricConduction.Count; i++)
        {
            var value = Math.Round(6.572f / float.Parse(r_Cu[i].text), 4);
            electricConduction[i].text = value.ToString();
        }
        for (int i = 0; i < thermalConduction.Count; i++)
        {          
            thermalConduction[i].text = thermalConductionBuf[i];
        }
        for (int i = 0; i < ratio.Count; i++)
        {
            var value = Math.Round(float.Parse(thermalConduction[i].text) / float.Parse(electricConduction[i].text), 3);
            ratio[i].text = value.ToString();
        }
    }

    public void RestartMeasuring()
    {
        increment = 0;

        var anim = GameObject.Find("Sphere001").GetComponent<Animator>();
        var toggleState = !anim.GetBool("IsSwitched");
        anim.SetBool("IsSwitched", toggleState);

        task4Done = false;
        endButton.SetActive(false);
        restartButton.SetActive(false);
        fillInTheTableButton.SetActive(false);
        ClearTheTable();
        Task3();
    }

    void ClearTheTable()
    {
        foreach (var el in r_Cu)
        {
            el.text = "";
        }
        foreach (var el in r_W)
        {
            el.text = "";
        }
        foreach (var el in electricConduction)
        {
            el.text = "";
        }
        foreach (var el in thermalConduction)
        {
            el.text = "";
        }
        foreach (var el in ratio)
        {
            el.text = "";
        }
    }

    public void RestartLab()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}