using UnityEngine;
using UnityEngine.UI;

public class Camera_Manager : MonoBehaviour {

    RawImage raw_image; // the raw image that will display the camera feed
    AspectRatioFitter fitter; // the aspect ratio fitter that will make the raw image fit the screen
    WebCamTexture cam_texture; // the webcam texture that will be used to get the camera feed
    bool ratio_set; // a boolean to check if the aspect ratio has been set

    void Start()
    {
        raw_image = GetComponent<RawImage>(); 
        fitter = GetComponent<AspectRatioFitter>(); 
        InitWebCam();
    }
    // this function is called every frame
    void Update()
    {
        if (cam_texture.width > 100 && !ratio_set) {
            ratio_set = true; 
            SetAspectRatio(); 
        }       
    }
    // this function initializes the webcam texture and sets it as the texture of the raw image
    void InitWebCam()
    {
        string camName = WebCamTexture.devices[0].name;
        cam_texture = new WebCamTexture(camName, Screen.width, Screen.height, 30);
        raw_image.texture = cam_texture;
        cam_texture.Play();
    }
    // this function sets the aspect ratio of the raw image to the aspect ratio of the camera feed
    void SetAspectRatio()
    { 
        fitter.aspectRatio = (float)cam_texture.width / (float)cam_texture.height;
    }

   
    // this function returns the webcam texture
    public WebCamTexture GetCamImage()
    {
        return cam_texture;
    }
}