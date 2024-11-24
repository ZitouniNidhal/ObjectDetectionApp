using System;

namespace ObjectDetectionApp
{
    public class Configuration
    {
        public string ApiEndpoint { get; set; }
        public int CameraIndex { get; set; }
        public int FrameWidth { get; set; }
        public int FrameHeight { get; set; }
        public int TimerInterval { get; set; }

        public Configuration()
        {
            // Default settings
            ApiEndpoint = "http://localhost:5000/predict";
            CameraIndex = 0; // Default to the first camera
            FrameWidth = 640; // Default width
            FrameHeight = 480; // Default height
            TimerInterval = 30; // Default timer interval in milliseconds
        }

        // Method to load settings from a file (for example, JSON or XML)
        public void LoadSettings(string filePath)
        {
            // Implementation to read settings from a file
            // This could involve deserializing a JSON or XML file
        }

        // Method to save settings to a file
        public void SaveSettings(string filePath)
        {
            // Implementation to write settings to a file
            // This could involve serializing to a JSON or XML file
        }
    }
}