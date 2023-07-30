using UnityEngine;
using Vuforia;

public class Access_Camera : MonoBehaviour
{
    public GameObject ARCamera; // Vuforia AR camera
    public GameObject Canvas; // canvas to open the webcam or android camera
    public GameObject Inference; // game object to display the inference results


    public void OnTrackableStateChanged() // this function is called when the "Start" image target is found
    {                                  
        
        ARCamera.SetActive(false); // disable the AR camera
        Canvas.SetActive(true); // enable the canvas to open the webcam or android camera
        Inference.SetActive(true); // enable the game object
        
    }
}
