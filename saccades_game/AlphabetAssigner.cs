using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

public class AlphabetAssigner : MonoBehaviour
{
    public double time;
    bool timerOn = false;
    bool correctPick = true;
    bool buttonPressed = false;
    Text timerText;
    public string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; // Alphabets to assign
    public void InitializeGame()
    {
        time = 10;
        char[] alphabetArray = alphabets.ToCharArray();

        // Generate all permutations of 2 alphabets
        List<string> allPermutations = GeneratePermutations(alphabetArray, 2);

        // Shuffle the permutations using Unity's Random
        List<string> shuffledPermutations = ShuffleList(allPermutations);

        // Select the first 112 shuffled permutations
        List<string> uiCombos = shuffledPermutations.Take(110).ToList();
        int k = UnityEngine.Random.Range(0, 110);
        string randomPick = uiCombos[k];
        Debug.Log(randomPick);
        // Get reference to the panel containing the child elements
        Transform panel = transform.GetChild(0); // Assuming the panel is the first child
        Transform timer = transform.GetChild(1);
        Transform code = transform.GetChild(2);

        Image timerImage = timer.GetComponent<Image>();
        timerText = timerImage.GetComponentInChildren<Text>();

        Text codeText = code.GetComponent<Text>();
        codeText.text = "Code: " + randomPick;
        Canvas.ForceUpdateCanvases();

        // Iterate through all child elements of the panel
        for (int i = 0; i < panel.childCount; i++)
        {
            // Get reference to the child element
            Transform childElement = panel.GetChild(i);

            // Get reference to the Image component of the child
            Image imageComponent = childElement.GetComponent<Image>();
            imageComponent.gameObject.AddComponent<UnityEngine.EventSystems.EventTrigger>();


            // Get the Event Trigger component
            UnityEngine.EventSystems.EventTrigger eventTrigger = imageComponent.GetComponent<UnityEngine.EventSystems.EventTrigger>();
            eventTrigger.enabled = true;
            // Create a new entry for the event trigger
            UnityEngine.EventSystems.EventTrigger.Entry entry = new UnityEngine.EventSystems.EventTrigger.Entry();

            // Set the event type to "Pointer Enter" (or desired event)
            entry.eventID = UnityEngine.EventSystems.EventTriggerType.PointerClick;

            // Set the callback function for the event
            entry.callback = new UnityEngine.EventSystems.EventTrigger.TriggerEvent();
            entry.callback.AddListener((eventData) => OnSelectEvent(imageComponent, randomPick, panel));

            // Add the new entry to the Event Trigger component
            eventTrigger.triggers.Add(entry);

            if (imageComponent != null)
            {
                // Access the Text (Legacy) component of the child (assuming it's the first child of the Image)
                Text textComponent = imageComponent.GetComponentInChildren<Text>();

                if (textComponent != null && i < uiCombos.Count)
                {
                    // Assign alphabet to the Text (Legacy) component
                    textComponent.text = uiCombos[i].ToString();
                    textComponent.color = Color.white;
                    textComponent.fontSize = 40;
                }
            }
        }
        Debug.Log("Number of child elements in the panel: " + panel.childCount);
        Debug.Log("Working");
        timerOn = true;
        timerText.color = Color.green;
        buttonPressed = true;
    }
    void Update()
    {
        if (buttonPressed)
        {
            if(timerOn && time > 0)
            {
                time -= Time.deltaTime;
                time = Math.Round(time, 2);
                string timeAsString = time.ToString();
                string[] parts = timeAsString.Split('.');
                string minutes = (parts[0].Length > 1) ? parts[0] : "0" + parts[0];
                string seconds = (parts.Length > 1) ? parts[1] : "00"; // Handle case where there's no decimal part
                timerText.text = "Timer: " + minutes + ":" + seconds;
            }
            else if(timerOn == false && time > 0)
            {
                string timeAsString = time.ToString();
                string[] parts = timeAsString.Split('.');
                string minutes = (parts[0].Length > 1) ? parts[0] : "0" + parts[0];
                string seconds = (parts.Length > 1) ? parts[1] : "00";
                timerText.text = "Timer: " + minutes + ":" + seconds;
                if (correctPick == false)
                {
                    timerText.color = Color.red;
                }
                else
                {
                    timerText.color = Color.green;
                }
            }
            else
            {
                timerText.text = "Timer: 00:00";
                timerText.color = Color.red;
            }
        }
        
    }

    public static List<string> GeneratePermutations(char[] alphabetArray, int k)
    {
        List<string> permutations = new List<string>();
        GeneratePermutationsHelper(alphabetArray, k, "", permutations);
        return permutations;
    }

    private static void GeneratePermutationsHelper(char[] alphabetArray, int k, string prefix, List<string> permutations)
    {
        if (k == 0)
        {
            permutations.Add(prefix);
            return;
        }

        foreach (char c in alphabetArray)
        {
            string newPrefix = prefix + c;
            GeneratePermutationsHelper(alphabetArray, k - 1, newPrefix, permutations);
        }
    }

    // Function to shuffle a list of strings using Unity's Random
    public static List<string> ShuffleList(List<string> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            string value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    void OnSelectEvent(Image imageComponent, string codeText, Transform panel)
    {
        StopTrigger(panel);
        Text textComponent = imageComponent.GetComponentInChildren<Text>();
        if (textComponent != null)
        {
            timerOn = false;
            if (textComponent.text == codeText)
            {
                textComponent.color = Color.green;
                correctPick = true;
            }
            else
            {
                correctPick = false;
                textComponent.color = Color.red;
            }
        }
    }
    
    void StopTrigger(Transform panel)
    {
        for (int i = 0; i < panel.childCount; i++)
        {
            // Get reference to the child element
            Transform childElement = panel.GetChild(i);

            // Get reference to the Image component of the child
            Image imageComponent = childElement.GetComponent<Image>();
            UnityEngine.EventSystems.EventTrigger eventTrigger = imageComponent.GetComponent<UnityEngine.EventSystems.EventTrigger>();
            eventTrigger.enabled = false;

        }
    }
    public void OnPointerClick()
    {
        InitializeGame();
    }
}
