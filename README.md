# MemorizenPredict - Augmented Reality App for Language Learning

MemorizenPredict is an application developed for the Augmented Reality course of HTW (Hochschule für Technik und Wirtschaft Berlin). The app aims to assist language learners in memorizing school objects' names in four different languages (English, German, Turkish, and French) using augmented reality and machine learning techniques.

## Features

### Training Part

Upon opening the app, users are presented with the Training Part, which allows them to scan 20 pre-uploaded image targets from the Vuforia database. When an image target for a school object is detected, a corresponding 3D model appears on the screen. On the left side, there are four buttons displaying the names of the objects in the four supported languages. Users can select a language by clicking on one of these buttons, and the chosen language is shown in the top-right corner.

After selecting a language, users can click on any of the language buttons to mark their choice. Clicking the "Pronounce" button allows users to hear the pronunciation of the selected language. The Training Part continues until the "Start" image target is detected, signaling the transition to the Testing Part.

### Testing Part

The Testing Part presents users with eight buttons at the beginning: four representing the supported languages on the left and four additional buttons on the right. The top-right button is used to predict the school object shown on the screen using a pretrained machine learning model (Efficientnet-lite4-11).

Once the object is displayed on the screen, users can click the "Prediction" button. The four option buttons will then be updated with four possible answers in the chosen language, with one of them being the correct answer. Users need to guess the correct answer, and upon selection, the app provides immediate feedback by showing "Correct" or "Wrong" text along with corresponding sound effects.

Users can continue making predictions by clicking the "New Prediction" button. When they wish to see their overall results, they can click the "Show" button, which displays the number of correct and wrong answers. Additionally, there is a "Finish" button to close the app.

## Important Notes

- The app utilizes Vuforia for AR camera functionality during the Training Part and the main device camera for machine learning predictions during the Testing Part.
- The 3D objects used in the app are mostly downloaded from Unity Asset Store and other websites like Sketchfab and All3DP.
- A pretrained machine learning model, "Efficientnet-lite4-11," along with its label map is used for object prediction.
- The app is designed to work with 19 school objects, and separate label map text files have been created for the four supported languages.
- Pronunciation audio, correct and wrong sound effects, flag images, and object label map text files can be found in the Resources folder.
- The app is currently designed for Android devices only.
- A GitHub repository and a drive link are provided to download the project files, including the apk file, image targets, demo video, and screenshots.

## GitHub Repository

Please find the GitHub repository [here](https://github.com/your-username/repo-name).

## Drive Link

For access to the project files, including the apk file, image targets, demo video, and screenshots, please visit the provided drive link.

[Drive Link](https://drive.google.com/drive/folder/your-drive-folder)

## Note

The app requires a functional AR camera and some missing parts related to AR camera, image targets, and 3D objects in the repository. Additionally, scripts and game objects are not attached to the project. Please refer to the drive link for the complete and functional app.

Please note that when you open the apk file, rotate your phone to horizontal mode to ensure proper display of buttons on the screen.

## License

The MemorizenPredict app is distributed under the [MIT License](LICENSE).

---

_This README provides a brief overview of the MemorizenPredict app, its features, important notes, and how to access the project files. For detailed instructions on installation, usage, and development, please refer to the GitHub repository._


