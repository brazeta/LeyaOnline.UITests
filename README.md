# LeyaOnline.UITests
To be able to open, build and run the automated UI tests in this project please follow these instructions:
1) Download and install Visual Studio Community Edition (https://visualstudio.microsoft.com/vs/community/)
2) Open Visual Studio
3) On the Startup window select the option Clone a Repository
![image](https://github.com/user-attachments/assets/61ae428b-f673-4334-91bf-d0896ff53a49)

4) On the Clone a repository section setup this repo URL (https://github.com/brazeta/LeyaOnline.UITests/)
![image](https://github.com/user-attachments/assets/f221c123-a71f-4b52-b7b0-040d2634e5b4)

5) Click on the Clone button
6) The Clone process should complete without errors and the VS Solution should open.
![image](https://github.com/user-attachments/assets/c20ff97b-e515-4535-a583-c84a8eb494f0)

7) If the Solution Explorer window is not visible please Click on View > Solution Explorer
![image](https://github.com/user-attachments/assets/9f8de989-fad3-4d89-afa3-f0d172366a58)

8) To open the Test Explorer window (if not open already) go to Test > Test Explorer
![image](https://github.com/user-attachments/assets/6d5c439e-ebe4-4b95-ab01-8fc10ccfe9ee)

9) Right rlick on the project "LeyaOnline.UITests" and Select the option "Build"
![image](https://github.com/user-attachments/assets/d3ad4111-5ad1-4963-b794-4d5a38cdff02)

10) The project should build without any errors. The Test Explorer window should be updated with the following tests:
![image](https://github.com/user-attachments/assets/2bda8f50-1eea-4432-85b7-c319ce770796)

11) To run the tests select one or more in the Test Explorer window, Right-Click on them, and Select the option "Run"
![image](https://github.com/user-attachments/assets/cd72e7d7-6ba1-4aaa-be0f-00a77181275c)

12) As the tests start running the browser will be open and closed automatically. All tests should succeed
![image](https://github.com/user-attachments/assets/a2309a8f-b67e-4b7a-b7bf-266e6eda1f32)

13) To select a browser to run the tests please open the app.config file under the "LeyaOnline.UITests" project and modify the "browser" appSetting (you can select either Chrome or Edge)
![image](https://github.com/user-attachments/assets/df12c3e9-e2ae-4899-93f5-95639f260f96)

14) To run the tests in headless mode modify the appSetting "RunTestsInHeadlessMode" (set it to either "true" or "false")
15) All test code is located under "LeyaOnline.UITests" > "UITestMethods" > "LeyaOnlineTestClass.cs"

