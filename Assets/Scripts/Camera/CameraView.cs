using UnityEngine;

namespace CameraSettings
{
    public class CameraView : MonoBehaviour
    {
        private Camera mainCamera;

        void Start()
        {
            mainCamera = GetComponent<Camera>();
        }
        void OnGUI()
        {
            int size = 12;
            float posX = mainCamera.pixelWidth / 2 - size / 4;
            float posY = mainCamera.pixelHeight / 2 - size / 2;
            GUI.Label(new Rect(posX, posY, size, size), "*"); // Команда GUI.Label() отображает на экране символ.
        }
    }
}
