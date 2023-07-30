using UnityEngine;
using Unity.Barracuda;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.UI;
using System;

public class Prediction : MonoBehaviour {

	public AudioClip soundClip; // the audio clip for the correct and wrong answer sound
    public AudioSource audioSource; // the audio source for the correct and wrong answer sound

	bool running = false; // a boolean to check if the prediction button is clicked

	int same_answers_counter = 0; // a counter to check if the same answer is given twice 
	const int IMAGE_SIZE = 224; // the size of the image that will be fed to the model
	const string INPUT_NAME = "images"; // the name of the input layer of the model
	const string OUTPUT_NAME = "Softmax"; // the name of the output layer of the model

	[Header("Model Stuff")]
	public NNModel modelFile; // the model file
	public TextAsset labelAsset; // the label file

	[Header("Scene Stuff")]
	public Camera_Manager camera_manager; // the camera manager script
	public Prepare prepare; // the prepare script
	public Text uiText_language; // the text that displays the language
	public Text uiText; // the text that displays the prediction

	public Text buttonTextCorrect; // the text that displays the number of correct answers
	public Text buttonTextWrong; // the text that displays the number of wrong answers
	public GameObject Button1_object; // the first button (option)
	public GameObject Button2_object; // the second button (option)
	public GameObject Button3_object; // the third button (option)
	public GameObject Button4_object; // the fourth button (option)
	public Text buttonText1; // the text that displays the first choice/option text
	public Text buttonText2; // the text that displays the second choice/option text
	public Text buttonText3; // the text that displays the third choice/option text
	public Text buttonText4;  // the text that displays the fourth choice/option text

	public GameObject Button_prediction; // the prediction button

	public GameObject TurkishButton_object; // the Turkish button to choose Turkish as the prediction language
	public GameObject EnglishButton_object; // the English button to choose English as the prediction language
	public GameObject GermanButton_object; // the German button to choose German as the prediction language

	public GameObject FrenchButton_object; // the French button to choose French as the prediction language

	public GameObject CorrectButton_object; // the correct button (only displays but not clickable)
	public GameObject WrongButton_object; // the wrong button (only displays but not clickable)

	public GameObject CloseButton_object; // the close button to close the application

	public GameObject NewPredictionButton_object; // the new prediction button to make a new prediction

	public GameObject Back_object; // the back button to go back to the main screen

	public GameObject ShowButton; // the show button to show the results of the predictions
	Text[] array_options = new Text[4]; // the array of options

	public string objects_names_txt; // the name of the text file that contains the objects

	int number_of_objects_in_file; // the number of objects in the text file

	bool ShowButton_clicked = false; // a boolean to check if the show button is clicked

	private List<string> objectsList = new List<string>(); // a list of campus objects are stored in this list
	string[] labels; // an array of 1000 pretrained objects
	int answer; // the answer of the prediction (index)
	IWorker worker; // the worker that will run the model
	int[] same_answers = new int[2]; // an array to check if the same answer is given twice

	string language = "nothing"; // do not predict if no language is chosen
	bool button_clicked = false; // a boolean to check if any of the options is chosen
	int correction = -1; // the correction of the prediction (index)
	int[] numbers; // indices of the possible options (19 campus objects)

	int correct_counter = 0; // a counter to count the number of correct answers
	int wrong_counter = 0; // a counter to count the number of wrong answers
	int actual_answer = -1; // the actual answer of the prediction (index)

	void Start() {
        var model = ModelLoader.Load(modelFile);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, model);
        LoadLabels();
		uiText_language.text = "No language is chosen";

		// set the text of the buttons to the text of the options in the screen right now
		array_options[0] = buttonText1; 
		array_options[1] = buttonText2;
		array_options[2] = buttonText3;
		array_options[3] = buttonText4;

		// set the same answers to -1 since there is no answer yet
		same_answers[0] = -1;
		same_answers[1] = -1;

	}
	// Load the labels from the text file of the pretrained objects
	void LoadLabels()
	{	
		var stringArray = labelAsset.text.Split('"').Where((item, index) => index % 2 != 0);		
		labels = stringArray.Where((x, i) => i % 2 != 0).ToArray();
	}
	// Set running true to start the prediction
	public void ButtonToRunPrediction()
	{
		running = true;
	}
	// Update every frame and check if the prediction button and any of the option buttons are clicked
	void Update() {

		if (running == true)
		{
			WebCamTexture webCamTexture = camera_manager.GetCamImage(); // get the image from the camera

			if (webCamTexture.didUpdateThisFrame && webCamTexture.width > 100) { 
				prepare.ScaleAndCropImage(webCamTexture, IMAGE_SIZE, RunModel); // scale and crop the image
			}
			running = false;
		}

		if(button_clicked == true)
		{
			//if the text of the button1 is not "Button 1", compare the prediction with the actual answer
			if(buttonText1.text != "Option 1")
			{		
				prediction(actual_answer); // Compare if the chosen option is the same as the actual answer		
			}

			
		}
	
	}

	// Run the model
	void RunModel(byte[] pixels) 
	{
		StartCoroutine(RunModelRoutine(pixels));
	}
	// French button is clicked
	public void FrenchButton() 
	{
		objects_names_txt = "Objects_with_indices_French";
		language = "French";
		uiText.text = "";
		uiText_language.text = "French is chosen";
		number_of_objects_in_file = LoadObjectsFromFile(objects_names_txt);
		
		buttonText1.text = "Option 1";
		buttonText2.text = "Option 2";
		buttonText3.text = "Option 3";
		buttonText4.text = "Option 4";
		Button1_object.SetActive(true);
		Button2_object.SetActive(true);
		Button3_object.SetActive(true);
		Button4_object.SetActive(true);
	}

	// Turkish button is clicked
	public void TurkishButton() 
	{
		objects_names_txt = "Objects_with_indices_Turkish";
		language = "Turkish";
		uiText.text = "";
		uiText_language.text = "Turkish is chosen";
		number_of_objects_in_file = LoadObjectsFromFile(objects_names_txt);
		
		buttonText1.text = "Option 1";
		buttonText2.text = "Option 2";
		buttonText3.text = "Option 3";
		buttonText4.text = "Option 4";
		Button1_object.SetActive(true);
		Button2_object.SetActive(true);
		Button3_object.SetActive(true);
		Button4_object.SetActive(true);
	}

	//Close	the application
	public void CloseButton() 
	{
		Application.Quit();
	}

	// English button is clicked
	public void EnglishButton() 
	{
		objects_names_txt = "Objects_with_indices_English";
		language = "English";
		uiText.text = "";
		uiText_language.text = "English is chosen";
		number_of_objects_in_file = LoadObjectsFromFile(objects_names_txt);
		
		buttonText1.text = "Option 1";
		buttonText2.text = "Option 2";
		buttonText3.text = "Option 3";
		buttonText4.text = "Option 4";
		Button1_object.SetActive(true);
		Button2_object.SetActive(true);
		Button3_object.SetActive(true);
		Button4_object.SetActive(true);
		
	}

	// Show button is clicked
	public void ShowButtonFunc()
	{

		Button1_object.SetActive(false);
		Button2_object.SetActive(false);
		Button3_object.SetActive(false);
		Button4_object.SetActive(false);

		Button_prediction.SetActive(false);

		TurkishButton_object.SetActive(false);
		EnglishButton_object.SetActive(false);
		GermanButton_object.SetActive(false);
		FrenchButton_object.SetActive(false);

		CorrectButton_object.SetActive(true);
		WrongButton_object.SetActive(true);
		
		uiText.text = "";
		uiText_language.text = "";
		
		ShowButton.SetActive(false);
		ShowButton_clicked = true;

		NewPredictionButton_object.SetActive(false);

		CloseButton_object.SetActive(false);

		Back_object.SetActive(true);

		buttonTextCorrect.text = "Correct: " + "\n\n" + correct_counter;
		buttonTextWrong.text = "Wrong: " + "\n\n" + wrong_counter;

				
	}

	// Back button is clicked
	public void Back_objectFunc()
	{
		Button1_object.SetActive(true);
		Button2_object.SetActive(true);
		Button3_object.SetActive(true);
		Button4_object.SetActive(true);
		
		buttonText1.text = "Option 1";
		buttonText2.text = "Option 2";
		buttonText3.text = "Option 3";
		buttonText4.text = "Option 4";

		Button_prediction.SetActive(true);
		TurkishButton_object.SetActive(true);
		EnglishButton_object.SetActive(true);
		GermanButton_object.SetActive(true);
		FrenchButton_object.SetActive(true);

		CorrectButton_object.SetActive(false);
		WrongButton_object.SetActive(false);

		ShowButton.SetActive(true);
		ShowButton_clicked = false;

		NewPredictionButton_object.SetActive(true);

		CloseButton_object.SetActive(true);

		Back_object.SetActive(false);

		correct_counter = 0;
		wrong_counter = 0;
	}

	
	public void CorrectButton() 
	{		
		// The number of correct predictions is shown
	}


	public void WrongButton() 
	{
		// The number of wrong predictions is shown	
	}


	// German button is clicked
	public void GermanButton() 
	{
		objects_names_txt = "Objects_with_indices_German";
		language = "German";
		uiText_language.text = "German is chosen";
		uiText.text = "";
		number_of_objects_in_file = LoadObjectsFromFile(objects_names_txt);
		
		buttonText1.text = "Option 1";
		buttonText2.text = "Option 2";
		buttonText3.text = "Option 3";
		buttonText4.text = "Option 4";
		Button1_object.SetActive(true);
		Button2_object.SetActive(true);
		Button3_object.SetActive(true);
		Button4_object.SetActive(true);
	}

	// Option 1 is chosen
	public void Button1()
	{		
		if (buttonText1.text != "Option 1") {
		button_clicked = true;
		correction = 0;
		}
	}

	// Option 2 is chosen
	public void Button2()
	{
		if (buttonText2.text != "Option 2") {
		button_clicked = true;
		correction = 1;
		}
	}

	// Option 3 is chosen
	public void Button3()
	{	
		if (buttonText3.text != "Option 3") {
		button_clicked = true;
		correction = 2;
		}
	}

	// Option 4 is chosen
	public void Button4()
	{		
		if (buttonText4.text != "Option 4") {
		button_clicked = true;
		correction = 3;
		}
	}

	// Check if the answer of the user is correct or wrong
	public void prediction(int answer)
	{					
		if (correction == answer) {
			
			string name_correct = "correct";
			audioSource.clip = Resources.Load<AudioClip>(name_correct); 
			audioSource.Play(); //play correct sound
			
			uiText.text = "Correct!";	
			uiText.color = Color.green;
			correct_counter++;
		}
		else
		{
			
			string name_wrong = "wrong";
			audioSource.clip = Resources.Load<AudioClip>(name_wrong);
			audioSource.Play(); //play wrong sound

			uiText.text = "Wrong!";
			uiText.color = Color.red;
			wrong_counter++;
		}	

		
		button_clicked = false;

		Button1_object.SetActive(false);
		Button2_object.SetActive(false);
		Button3_object.SetActive(false);
		Button4_object.SetActive(false);
		Button_prediction.SetActive(false);

		language = "";	
		
	}

	// New prediction button is clicked
	public void new_prediction()
	{
		uiText_language.text = "No language is chosen";

		button_clicked = false;

		correction = -1;

		language = "nothing";

		buttonText1.text = "Option 1";
		buttonText2.text = "Option 2";
		buttonText3.text = "Option 3";
		buttonText4.text = "Option 4";

		Button1_object.SetActive(true);
		Button2_object.SetActive(true);
		Button3_object.SetActive(true);
		Button4_object.SetActive(true);

		Button_prediction.SetActive(true);

		uiText.text = "";
	}
	
	// Load the object names which will be used in this project to detect (campus objects)
    public int LoadObjectsFromFile(string objects_names_txt)
    {
        TextAsset txtAsset = Resources.Load<TextAsset>(objects_names_txt);

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

        numbers = parsedNumbers.ToArray(); // the array of actual indices of the objects taken from the txt file (labels_map.txt) 
        objectsList = parsedObjects; // the array of the names of the objects that can be predictable
        return objectsList.Count;	
	}

	// Create the options for the user to choose by putting the correct answer randomly in one of the options
	public int CreateRandom(int predicted_actual_index)
	{
		int which_index = Array.IndexOf(numbers, predicted_actual_index); // the index of the predicted object by the model

		int randomIndexNumber = UnityEngine.Random.Range(0, array_options.Length); // random index number for the correct option
		answer = randomIndexNumber; // index of the answer

		for (int i = 0; i < array_options.Length; i++)
		{
			if(i == randomIndexNumber)
			{
				array_options[randomIndexNumber].text = objectsList[which_index]; // put the correct answer in the random index
			}

			else
			{

				int randomIndex_ = UnityEngine.Random.Range(0, objectsList.Count); // random index for the wrong options
				
				while (randomIndex_ == which_index || randomIndex_ == same_answers[0] || randomIndex_ == same_answers[1]) // if the random index is the same as the correct answer or the same as one of the wrong answers
				{
					randomIndex_ = UnityEngine.Random.Range(0, objectsList.Count); // choose another random index for the wrong options
				}

				array_options[i].text = objectsList[randomIndex_]; // put the wrong answers in the other options
				
				if (same_answers_counter < 2) // there is no need to check for the last option
				{
					same_answers[same_answers_counter] = randomIndex_; // put the wrong answers in the same_answers array to check if there is a repetition
					same_answers_counter++;
				}
													
			}
				
		}
		
		return answer; // return the index of the correct answer
	}
	
	// IEnumerator to run the model and get the output of the model
	IEnumerator RunModelRoutine(byte[] pixels) {

		Tensor tensor = TransformInput(pixels); // transform the input image to a tensor
		
		var inputs = new Dictionary<string, Tensor> { 
			{ INPUT_NAME, tensor } // input the tensor to the model
		};

		worker.Execute(inputs); // execute the model
		Tensor outputTensor = worker.PeekOutput(OUTPUT_NAME); // get the output of the model

		
		List<float> temp = outputTensor.ToReadOnlyArray().ToList(); // convert the output tensor to a list
		float max = temp.Max(); // get the maximum value of the list
		int index = temp.IndexOf(max); // get the index of the maximum value		
	
		// if the language of the prediction is not chosen yet,
		if (language == "nothing") {

			if(ShowButton_clicked == true) // if the show button is clicked, there is no need to show the prediction but only showing the number of correct and wrong answers
			{
				uiText.text = "";
			}
			else // warnt the user to choose the language of the prediction
			{
				uiText.text = "Waiting for language selection!";
				uiText.color = new Color32(48, 147, 133, 255);
			}
			
		}

		// if the language of the prediction is chosen, put the correct and wrong options to the buttons for user to choose and get the index of the correct answer
		else
		{
			uiText.text = "";

			if(index == numbers[0]) {
				
				actual_answer = CreateRandom(index);											
			}						
			else if	(index == numbers[1]) {
				
				actual_answer = CreateRandom(index);
			}			
			else if	(index == numbers[2]) {
				
				actual_answer = CreateRandom(index);
			}		
			else if	(index == numbers[3]) {
				
				actual_answer = CreateRandom(index);			
			}	
			else if	(index == numbers[4]) {
				
				actual_answer = CreateRandom(index);
			}
			else if	(index == numbers[5]) {				
				actual_answer = CreateRandom(index);
			}
			else if	(index == numbers[6]) {
				
				actual_answer = CreateRandom(index);
			}
			else if	(index == numbers[7]) {
				
				actual_answer = CreateRandom(index);
			}
			else if	(index == numbers[8]) {
				
				actual_answer = CreateRandom(index);
			}
			else if	(index == numbers[9]) {
				
				actual_answer = CreateRandom(index);
			}
			else if	(index == numbers[10]) {
				actual_answer = CreateRandom(index);
			}
			else if	(index == numbers[11]) {
				actual_answer = CreateRandom(index);
			}
			else if	(index == numbers[12]) {
				actual_answer = CreateRandom(index);
			}
			else if	(index == numbers[13]) {
				actual_answer = CreateRandom(index);
			}
			else if	(index == numbers[14]) {
				actual_answer = CreateRandom(index);
			}
			else if (index == numbers[15]) {
				actual_answer = CreateRandom(index);
			}
			else if (index == numbers[16]) {
				actual_answer = CreateRandom(index);
			}
			else if (index == numbers[17]) {
				actual_answer = CreateRandom(index);
			}
			else if (index == numbers[18]) {
				actual_answer = CreateRandom(index);
			}

			same_answers_counter = 0; // reset the counter for the same answers
		}
		

        
        tensor.Dispose(); // dispose the tensor
		outputTensor.Dispose(); // dispose the output tensor
		yield return null; // return null
	}

	//transform from 0-255 to -1 to 1
	Tensor TransformInput(byte[] pixels){
		float[] transformedPixels = new float[pixels.Length]; // create a new float array for the transformed pixels

		for (int i = 0; i < pixels.Length; i++){
			transformedPixels[i] = (pixels[i] - 127f) / 128f; // transform the pixels from 0-255 to -1 to 1
		}
		return new Tensor(1, IMAGE_SIZE, IMAGE_SIZE, 3, transformedPixels); // return the tensor
	}
}