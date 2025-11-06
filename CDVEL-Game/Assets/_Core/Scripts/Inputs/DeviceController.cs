using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Inputs
{
    public class DeviceController : MonoBehaviour
    {
        private const int MIN_DEVICES = 2;
        
        private void Awake()
        {
            SetPlayerDevice();
        }

        private void SetPlayerDevice()
        {
            var playerArray = FindObjectsByType<PlayerInput>(FindObjectsSortMode.None);
            ReadOnlyArray<Gamepad> devices = Gamepad.all;

            if (devices.Count < MIN_DEVICES)
                Debug.LogError("There aren't enough controllers !");

            for (int i = 0; i < playerArray.Length; i++)
            {
                if (i > devices.Count)
                    break;
                
                playerArray[i].actions.devices = new InputDevice[] { devices[i] };
            }
        }
    }
}