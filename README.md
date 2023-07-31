# MemorizenPredict - Augmented Reality App for Language Learning

<p align="center">
  <img src="https://github.com/SafakGun/ARProject-HTW/assets/99952412/44b61a24-a620-41a1-a305-fb4425c79f36" alt="playstore" width="200" height="200">
</p>

MemorizenPredict is an application developed for the Augmented Reality course of HTW (Hochschule für Technik und Wirtschaft Berlin). The app aims to assist language learners in memorizing school objects' names in four different languages (English, German, Turkish, and French) using augmented reality and machine learning techniques.

## Features

### Training Part

Upon opening the app, users are presented with the Training Part, which allows them to scan 20 pre-uploaded image targets (19 for objects 1 to start the Testing Part) from the Vuforia database. When an image target for a school object is detected, a corresponding 3D model appears on the screen. On the left side, there are four buttons displaying the names of the objects in the four supported languages. Users can select a language by clicking on one of these buttons, and the chosen language is marked.

After selecting a language, clicking the "Pronounce" button allows users to hear the pronunciation of the selected language. The Training Part continues until the "Start" image target is detected, signaling the transition to the Testing Part.

![Wallet](https://github.com/SafakGun/ARProject-HTW/assets/99952412/89404f0f-f433-44c3-9fcd-5150018a90b2)

### Testing Part

The Testing Part presents users with eight buttons at the beginning: four representing the supported languages on the left and four additional buttons on the right. 

First, users can select one of the supported languages on the left side, in which they want to guess the names of the objects. The selected language can be seen top-right corner of the screen.

Once the object is displayed on the screen and one of the supported languages is selected, users can click the "Prediction" button which predicts the object on the screen by using a pre-trained machine learning model (Efficientnet-lite4-11). The four option buttons will then be updated with four possible answers in the chosen language, with one of them being the correct answer. Users need to guess the correct answer, and upon selection, the app provides immediate feedback by showing "Correct" or "Wrong" text along with corresponding sound effects.

Users can continue making predictions by clicking the "New Prediction" button. When they wish to see their overall results, they can click the "Show Results" button, which displays the number of correct and wrong answers. Additionally, there is a "Close" button to close the app.

![3](https://github.com/SafakGun/ARProject-HTW/assets/99952412/38bbde71-5961-4415-a00a-348578327aee)

## Important Notes

- The app utilizes Vuforia for AR camera functionality during the Training Part and the main device camera for machine learning predictions during the Testing Part.
- The 3D objects used in the app are mostly downloaded from Unity Asset Store and other websites like Sketchfab and All3DP.
- A pretrained machine learning model, "Efficientnet-lite4-11," along with its label map is used for object prediction and the files are located in "Models" folder in the project.
- However, there are 1000 pre-trained objects in that model. Since it will take too much time to create 1000 3D objects and 1000 image targets for the first part and it won’t be possible to test    the objects like pirate ship or great white shark, we decided to create a concept which is “School Objects” with 19 objects taken from the labels map file. 
- The app is designed to work with 19 school objects, and separate label map text files have been created for the four supported languages.
- Pronunciation audio, correct and wrong sound effects, flag images, and object label map text files can be found in the "Resources" folder.
- The good part about using machine learning is that any type of these 19 objects in the world can be detected by this model and it can be used in different classes, schools, universities etc.      (For instance, Volvic, Ja, Rewe or Kaufland water bottles are predicted as water bottle regardless their shape.) 
- The app is currently designed for Android devices only.
- When the github repo is downloaded, some of the parts which are related to AR camera, image targets and 3D Objects on the scene are not installed so the project is not running. Also, the          scripts and the game objects are not attached. Therefore, we also created a drive link where you can download the project files and run on your local device.
- The drive link also includes the apk file, image targets, demo video, and screenshots from the app.
- Please note that when you open the apk file, rotate your phone to horizontal mode to ensure proper display of buttons on the screen.

## Drive Link

For access to the project files, including the apk file, image targets, demo video, and screenshots, please visit the provided drive link.

[Drive Link](https://drive.google.com/drive/folder/your-drive-folder)

## ONNX Model (Efficientnet-lite4-11) Github Repository

For access to the pre-trained machine learning model and its labels that are used in this project, please visit the provided link.

[Github Repository] (https://github.com/onnx/models.git)

