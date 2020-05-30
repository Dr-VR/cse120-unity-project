# CSE120-Project

Outlining the progress Children's Hospital Team 16 makes.


###### To Run: (Install and setup headset first by following manufacturer instructions)

    1. Install :
                 - Unity
                    - https://unity3d.com/get-unity/download
                 - SRanipal Eye Tracking SDK and SR_Runtime
                    - https://developer.vive.com/resources/knowledgebase/vive-sranipal-sdk/
                 - SteamVR
                    - https://store.steampowered.com/app/250820/SteamVR/
    2. Prep project :
                - In Unity Settings:
                    - select **'Edit'** Tab
                    - then **'Project Settings'**
                        - under **'Player'** check the **'XR Settings'**
                        - check the box for **'Virtual Reality Supported'** and make sure OpenVR is in the list.
                - Clone and import project into Unity from this repository.
    3. Additional Support and Documentation :
                - Vive Community Forums
                    - https://forum.vive.com/forum/78-vive-eye-tracking-sdk/
                - TobiiXR SDK (Note: Does not provide raw eye data, this is what is currently being used in the project)
                    - https://vr.tobii.com/sdk/
    4. Running Project
                - Headset must be connected to PC (hardware requirements on manufacturer’s website)
                - There are different windows and different tabs you can add to help you create your scene/environment.
                    - Add ‘Console’ to your list of tabs to help debug and see current data output.
                - Press play button at the top of the project in Unity. To stop/end scene press play again.
    5. Understanding the Console outputs
                - Currently the logs are outputting a normalized gaze data that comes from the ‘Handle2DEyes’ script from
                the TobiiXR SDK. We originally thought we could use this SDK to get the raw eye data, but this is not
                the case. This is why you will need to use the ViveSR SDK or another if you can find one.
                - The convergence distance is the distance in meters of where the two eye’s gaze converges on screen, this
                turned out to not be so useful.
                - Timestamp is the time in seconds from the start of the application.
                - All of the above used the TobiiXR API which will not get the raw eye data.
                - The eye_data.cs script was made towards the end of this project to start utilizing the ViveSR SDK.
                        - To find out more about it, the Vive Community Forums were useful
                        - Check the folder, SRanipal_SDK_1.1.0.1, for additional documentation on how to use the APIs
                        and for more information.
    6. Lastly, Good Luck and Have Fun!
