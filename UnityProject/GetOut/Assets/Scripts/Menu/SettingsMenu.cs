using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using System.Runtime.InteropServices;

public class SettingsMenu : MonoBehaviour {
    //note: slider goes from -80 to 0 because that's what our audio mixer does
    //for audio
    public AudioMixer audioMixer;
    public void SetVolume(float volume) {
        audioMixer.SetFloat("volume", volume);
    }

    // set game quality
    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //for Resolution
     [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex;


    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for(int i = 0; i< resolutions.Length; i++) {
            if(resolutions[i].refreshRate == currentRefreshRate) {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for(int i = 0; i < filteredResolutions.Count; i++) {
            string resolutionOption = filteredResolutions[i].width + " x " + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + " Hz";
            options.Add(resolutionOption);
            if(filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height) {
                currentResolutionIndex = 1;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //für bildschirm erkennung
        //PopulateScreenDropdown();

    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }


    /*[DllImport("user32.dll")]
    static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DISPLAY_DEVICE
    {
        public int cb;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceString;
        public uint StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceKey;
    }

    public TMP_Dropdown screenDropdown;

    void PopulateScreenDropdown()
    {
        screenDropdown.ClearOptions();
        var options = new System.Collections.Generic.List<string>();

        DISPLAY_DEVICE device = new DISPLAY_DEVICE();
        device.cb = Marshal.SizeOf(device);

        uint i = 0;
        int displayIndex = 0;
        while (EnumDisplayDevices(null, i, ref device, 0))
        {
            // Überprüfen, ob das Gerät ein echter Monitor ist
            if ((device.StateFlags & 0x00000001) != 0) // DISPLAY_DEVICE_ACTIVE flag
            {
                // Prüfen, ob das Display in Unity verfügbar ist, bevor wir es hinzufügen
                if (displayIndex < Display.displays.Length)
                {
                    string option = $"{device.DeviceString} ({Display.displays[displayIndex].systemWidth}x{Display.displays[displayIndex].systemHeight})";
                    options.Add(option);
                    displayIndex++;
                }
            }
            i++;
        }

        screenDropdown.AddOptions(options);
        screenDropdown.onValueChanged.AddListener(delegate { SetScreen(screenDropdown.value); });
    }

    public void SetScreen(int screenIndex)
    {
        if (screenIndex < Display.displays.Length)
        {
            if (!Display.displays[screenIndex].active)
            {
                Display.displays[screenIndex].Activate();
            }

            Debug.Log($"Bildschirm gewechselt zu: {Display.displays[screenIndex].systemWidth}x{Display.displays[screenIndex].systemHeight}");
        }
        else
        {
            Debug.LogError("Ungültiger Bildschirmindex: " + screenIndex);
        }
    }*/

}
