using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Prepare : MonoBehaviour {

    RenderTexture renderTexture; // the render texture that will be used to crop the image
    Vector2 scale = new Vector2(1, 1); // the scale of the image
    Vector2 offset = Vector2.zero; // the offset of the image

    UnityAction<byte[]> callback; // the callback that will be called when the image is ready

    // this function scales and crops the image and then calls the callback
    public void ScaleAndCropImage(WebCamTexture webCamTexture, int desiredSize, UnityAction<byte[]> callback) {

        this.callback = callback; // set the callback

        if (renderTexture == null) {
            renderTexture = new RenderTexture(desiredSize, desiredSize,0,RenderTextureFormat.ARGB32); // create the render texture
        }

        scale.x = (float)webCamTexture.height / (float)webCamTexture.width; // calculate the scale
        offset.x = (1 - scale.x) / 2f; // calculate the offset
        Graphics.Blit(webCamTexture, renderTexture, scale, offset); // scale and crop the image
        AsyncGPUReadback.Request(renderTexture, 0, TextureFormat.RGB24, OnCompleteReadback); // read the image
    }

    // this function is called when the image is ready and calls the callback
    void OnCompleteReadback(AsyncGPUReadbackRequest request) {

        if (request.hasError) {
            Debug.Log("GPU readback error detected.");
            return;
        }

        callback.Invoke(request.GetData<byte>().ToArray()); // call the callback
    }
}