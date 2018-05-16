using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class MyTools
{
#if UNITY_EDITOR
    [MenuItem("MyTools/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("MyTools/Screen Shot #a")]
    private static void GetScreenShot()
    {
        // File path
        string folderPath = "E:/screenshots/";
        string fileName = "scr";

        // Create the folder beforehand if not exists
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);
        int i = 0;
        while (System.IO.File.Exists(folderPath + fileName + ".png"))
        {
            fileName = "scr" + i;
            i++;
        }

        // Capture and store the screenshot
        Application.CaptureScreenshot(folderPath + fileName + ".png");
    }

   
#endif
}
