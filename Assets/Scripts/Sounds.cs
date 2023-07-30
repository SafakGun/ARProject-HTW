using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System;
using UnityEngine.UI;

public class Sounds : MonoBehaviour
{
    public AudioClip soundClip; // audio clip for the pronunciation of the objects
    public AudioSource audioSource; // audio source for the pronunciation of the objects

    public GameObject my3DModel; // 3D objects that will be displayed on the screen
    int[] numbers; // indices of the possible options (19 campus objects)
    private List<string> objectsList = new List<string>(); // a list of campus objects are stored in this list

    int number_of_objects_in_file; // number of campus objects in the file

    public string objectsTextFileName; // name of the file that contains the campus objects

    public Text ButtonTextEnglish; // English names of the campus objects are displayed on the buttons
    public Text ButtonTextGerman; // German names of the campus objects are displayed on the buttons
    public Text ButtonTextTurkish; // Turkish names of the campus objects are displayed on the buttons

    public Text ButtonTextFrench; // French names of the campus objects are displayed on the buttons

    public Text uitext; // text that stores the detected object name

    public GameObject Button_English; // English button
	public GameObject Button_German; // German button
	public GameObject Button_Turkish; // Turkish button

    public GameObject Button_French; // French button

    bool flag = false; // a boolean to check if the objects are loaded from the file

    bool on_found_flag = false; // a boolean to check if the object is found

    string[,] objects = new string[4, 19]; // a 2D array to store the names of the campus objects in different languages

    string modelName; // name of the 3D model

    int button_number; // number of the button that is pressed

    int index_of_object; // index of the object that is detected

    private TrackableBehaviour trackableBehaviour; // trackable behaviour of the image target

    public GameObject Canvas_Pronun; // canvas that plays the pronunciation of the objects
    public GameObject Canvas_Eng_Button; // canvas that is selected when the English button is pressed to be pronounced in English
    public GameObject Canvas_Ger_Button; // canvas that is selected when the German button is pressed to be pronounced in German
    public GameObject Canvas_Tr_Button; // canvas that is selected when the Turkish button is pressed to be pronounced in Turkish
    public GameObject Canvas_French_Button; // canvas that is selected when the French button is pressed to be pronounced in French
    public GameObject Canvas_Eng_Flag; // canvas for the English flag
    public GameObject Canvas_Ger_Flag; // canvas for the German flag
    public GameObject Canvas_Tr_Flag; // canvas for the Turkish flag
    public GameObject Canvas_French_Flag; // canvas for the French flag
    public GameObject Canvas_Sound_Flag; // canvas for the sound image


    // Load the object names which will be used in this project to detect (campus objects)
    public int LoadObjectsFromFile(string objectsTextFileName)
    {
        TextAsset txtAsset = Resources.Load<TextAsset>(objectsTextFileName);

        string[] lines = txtAsset.text.Split('\n');
        List<int> parsedNumbers = new List<int>();
        List<string> parsedObjects = new List<string>();

        foreach (string line in lines)
        {
            string[] parts = line.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[1], out int number))
            {
                parsedObjects.Add(parts[0].Trim());
                parsedNumbers.Add(number);
            }
        }

        numbers = parsedNumbers.ToArray();
        objectsList = parsedObjects;
        flag = true;
        return objectsList.Count;
	
    }

    // Play the pronunciation of the objects
    public void soundButton()
    {

        string name = objects[button_number, index_of_object]; //play the mp3 file with the same name as the model

        if (button_number == 0)
        {
            audioSource.clip = Resources.Load<AudioClip>("English_sounds/" + name.ToLower());
            audioSource.Play();
        }
        else if(button_number == 1)
        {
            audioSource.clip = Resources.Load<AudioClip>("German_sounds/" + name.ToLower());
            audioSource.Play();
        }
        else if(button_number == 2)
        {
            audioSource.clip = Resources.Load<AudioClip>("Turkish_sounds/" + name.ToLower());
            audioSource.Play();
        }
        else if(button_number == 3)
        {
            audioSource.clip = Resources.Load<AudioClip>("French_sounds/" + name.ToLower());
            audioSource.Play();
        }
     
    }

    // When the image target is found, the buttons and the 3D models are displayed on the screen
    public void onFound()
    {   
        Canvas_Pronun.SetActive(true);
        Canvas_Eng_Button.SetActive(true);
        Canvas_Ger_Button.SetActive(true);
        Canvas_Tr_Button.SetActive(true);
        Canvas_French_Button.SetActive(true);
        Canvas_Eng_Flag.SetActive(true);
        Canvas_Ger_Flag.SetActive(true);
        Canvas_Tr_Flag.SetActive(true);
        Canvas_French_Flag.SetActive(true);
        Canvas_Sound_Flag.SetActive(true);

        my3DModel = GameObject.Find(uitext.name);
        modelName = my3DModel.name;
        Debug.Log("Found 3D Model: " + modelName);
    
        if (my3DModel != null)
        {
            modelName = my3DModel.name;
            Debug.Log("Found 3D Model: " + modelName);
        }

        string[] objectsTextFileName_array = new string[4]; // storing the txt file names of the languages in an array
        objectsTextFileName_array[0] = "Objects_with_indices_English";
        objectsTextFileName_array[1] = "Objects_with_indices_German";
        objectsTextFileName_array[2] = "Objects_with_indices_Turkish";
        objectsTextFileName_array[3] = "Objects_with_indices_French";

        for (int i = 0; i < 4; i++)
        {     
            number_of_objects_in_file = LoadObjectsFromFile(objectsTextFileName_array[i]); // load the object names from different languages

            for (int j = 0; j < number_of_objects_in_file; j++)
            {
                objects[i, j] = objectsList[j]; // store the object names in the array
            }

            //if the model name is in the english list with tolower, then show the name of that object in different languages
            for (int j = 0; j < number_of_objects_in_file; j++)
            {
                if (modelName.ToLower() == objects[0, j].ToLower())
                {
                    ButtonTextEnglish.text = objects[0, j];
                    ButtonTextGerman.text = objects[1, j];
                    ButtonTextTurkish.text = objects[2, j];
                    ButtonTextFrench.text = objects[3, j];

                    index_of_object = j; // store the index of the object in the array
                }
              
            }      
        }        
    }

    // English button is pressed
    public void EnglishButton()
    {
        button_number = 0;  
        Button_English.GetComponent<UnityEngine.UI.Image>().color = Color.gray;     
        Button_German.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        Button_Turkish.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        Button_French.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }

    // German button is pressed
    public void GermanButton()
    {
        button_number = 1;
        Button_German.GetComponent<UnityEngine.UI.Image>().color = Color.gray; 
        Button_English.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        Button_Turkish.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        Button_French.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }

    // Turkish button is pressed
    public void TurkishButton()
    {
        button_number = 2;
        Button_Turkish.GetComponent<UnityEngine.UI.Image>().color = Color.gray; 
        Button_English.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        Button_German.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        Button_French.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }

    // French button is pressed
    public void FrenchButton()
    {
        button_number = 3;
        Button_French.GetComponent<UnityEngine.UI.Image>().color = Color.gray; 
        Button_English.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        Button_German.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        Button_Turkish.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }

    // When the image target is lost, nothing is displayed on the screen
    public void OnTrackingLost()
    {
        Debug.Log("Tracking Lost");
        Button_English.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        Button_German.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        Button_Turkish.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        Button_French.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        Canvas_Pronun.SetActive(false);
        Canvas_Eng_Button.SetActive(false);
        Canvas_Ger_Button.SetActive(false);
        Canvas_Tr_Button.SetActive(false);
        Canvas_French_Button.SetActive(false);
        Canvas_Eng_Flag.SetActive(false);
        Canvas_Ger_Flag.SetActive(false);
        Canvas_Tr_Flag.SetActive(false);
        Canvas_French_Flag.SetActive(false);
        Canvas_Sound_Flag.SetActive(false);

    }

    // when the image target is found, corresponding function is called since the name of the object is desired to known
    public void Banana_text()
    {
        uitext.name = "Banana";
    }

     public void Ballpoint_text()
    {
        uitext.name = "Ballpoint";
    }

    public void Water_Bottle_text()
    {
        uitext.name = "Water Bottle";
    }

    public void Coffee_Mug_text()
    {
        uitext.name = "Coffee Mug";
    }

    public void Mouse_text()
    {
        uitext.name = "Mouse";
    }

    public void Ruler_text()
    {
        uitext.name = "Ruler";
    }

    public void Pencil_Sharpener_text()
    {
        uitext.name = "Pencil Sharpener";
    }

    public void Eraser_text()
    {
        uitext.name = "Eraser";
    }

    public void Pencil_Box_text()
    {
        uitext.name = "Pencil Box";
    }

    public void Keyboard_text()
    {
        uitext.name = "Keyboard";
    }

    public void Wallet_text()
    {
        uitext.name = "Wallet";
    }

    public void Spoon_text()
    {
        uitext.name = "Spoon";
    }

    public void Fork_text()
    {
        uitext.name = "Fork";
    }

    public void Hat_text()
    {
        uitext.name = "Hat";
    }

    public void Orange_text()
    {
        uitext.name = "Orange";
    }

    public void Strawberry_text()
    {
        uitext.name = "Strawberry";
    }

    public void Laptop_text()
    {
        uitext.name = "Laptop";
    }

    public void Desk_text()
    {
        uitext.name = "Desk";
    }

    public void Desktop_Computer_text()
    {
        uitext.name = "Desktop Computer";
    }

}

