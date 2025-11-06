using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

namespace Inputs
{
    public class DeviceController : MonoBehaviour
    {
        private void Awake()
        {
            SetPlayerDevice();
        }

        private void SetPlayerDevice()
        {
            var playerArray = FindObjectsByType<PlayerInput>(FindObjectsSortMode.None);
            ReadOnlyArray<Gamepad> devices = Gamepad.all;

            for (int i = 0; i < playerArray.Length; i++)
            {
                if (i > devices.Count)
                    break;
                
                playerArray[i].actions.devices = new InputDevice[] { devices[i] };
            }
        }
    }
}