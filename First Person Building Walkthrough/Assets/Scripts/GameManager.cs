using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool toggleOn;
    public bool forMobile;
    
    public float counter=30;
    public int locationRef;
    
    public Button setSceneButton;
    public GameObject player;
    public GameObject buttonToggle;
    public Image selectedLocation;
    public GameObject[] UIButtons;
    // public GameObject xButtonUI;
    // public GameObject triangleUI;
    public GameObject cameraPath;
    public GameObject[] canvasUI;
    public Transform camTransform;
    public Transform[] keyLocations;
    public RectTransform[] locTransforms;

    private CharacterController controller;

    private void Awake()
    {
        controller = player.GetComponent<CharacterController>();
        buttonToggle.SetActive(false);
        locationRef = 0;
        if (forMobile)
        {
            selectedLocation.gameObject.SetActive(false);
            foreach (var UI in UIButtons)
            {
                UI.SetActive(false);
            }
            // xButtonUI.SetActive(false);
            // triangleUI.SetActive(false);
        }
        else
        {
            selectedLocation.gameObject.SetActive(true);
            foreach (var UI in UIButtons)
            {
                UI.SetActive(true);
            }
            // xButtonUI.SetActive(true);
            // triangleUI.SetActive(true);
        }
    }

    private void Start()
    {
        controller.enabled = false;
        print(PlayerPrefs.GetFloat("PlayerPosX"));
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"), PlayerPrefs.GetFloat("PlayerPosZ"));
        Vector3 playerRotation = new Vector3(PlayerPrefs.GetFloat("PlayerRotX"), PlayerPrefs.GetFloat("PlayerRotY"), PlayerPrefs.GetFloat("PlayerRotZ"));
        player.transform.eulerAngles = playerRotation;
        Vector3 camRotation = new Vector3(PlayerPrefs.GetFloat("CamRotX"), PlayerPrefs.GetFloat("CamRotY"), PlayerPrefs.GetFloat("CamRotZ"));
        camTransform.eulerAngles = camRotation;
        controller.enabled = true;
        PlayerPrefs.DeleteAll();
        
        if (player.transform.position.y == 0f)
            SetLocation(0);
    }

    private void Update()
    {
        selectedLocation.rectTransform.position = locTransforms[locationRef].position;
        
        if (Input.GetButtonDown("PS4 X"))
        //if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleButtons();
            Debug.Log("X Pressed");
            //counter = 20f;
        }

        if (Input.GetButtonDown("PS4 Triangle"))
        //if (Input.GetKeyDown(KeyCode.T))
        {
            setSceneButton.onClick.Invoke();
            counter = 20f;
        }

        if (toggleOn && Input.GetButtonDown("PS4 R1"))
        //if (toggleOn && Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            if (locationRef < keyLocations.Length-1)
                locationRef += 1;
            counter = 20f;
        }
        
        if (toggleOn && Input.GetButtonDown("PS4 L1"))
        //if (toggleOn && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (locationRef > 0)
                locationRef -= 1;
            counter = 20f;
        }

        if (toggleOn && Input.GetButtonDown("PS4 Square"))
        //if (toggleOn && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Enter Pressed");
            SetLocation(locationRef);
            //counter = 20f;
        }
        
        else
           counter -= Time.deltaTime;

        if (counter >= 0)
        {
            cameraPath.SetActive(false);
            foreach (var UIElement in canvasUI)
            {
                UIElement.SetActive(true);
            }
            
        }
        else
        {
            cameraPath.SetActive(true);
            foreach (var UIElement in canvasUI)
            {
                UIElement.SetActive(false);
            }
        }
    }

    public void ToggleButtons()
    {
        counter = 20f;
        Debug.Log("ToggleButtons Called");
        if (!toggleOn)
        {
            buttonToggle.SetActive(true);
            toggleOn = true;
        }
        else
        {
            buttonToggle.SetActive(false);
            toggleOn = false;
        }
        
    }
    
    public void SetLocation(int locationID)
    {
        counter = 20f;
        Debug.Log("SetLocation Called");
        controller.enabled = false;
        player.transform.position = keyLocations[locationID].position;
        player.transform.rotation = keyLocations[locationID].rotation;
        controller.enabled = true;
    }

    public void SetDay()
    {
        // load day scene
        PlayerPrefs.SetFloat("PlayerPosX",player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY",player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ",player.transform.position.z);
        PlayerPrefs.SetFloat("PlayerRotX",player.transform.eulerAngles.x);
        PlayerPrefs.SetFloat("PlayerRotY",player.transform.eulerAngles.y);
        PlayerPrefs.SetFloat("PlayerRotZ",player.transform.eulerAngles.z);
        PlayerPrefs.SetFloat("CamRotX",camTransform.eulerAngles.x);
        PlayerPrefs.SetFloat("CamRotY",camTransform.eulerAngles.y);
        PlayerPrefs.SetFloat("CamRotZ",camTransform.eulerAngles.z);
        SceneManager.LoadScene(0);
    }
    
    public void SetEvening()
    {
        // load evening scene
        PlayerPrefs.SetFloat("PlayerPosX",player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY",player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ",player.transform.position.z);
        PlayerPrefs.SetFloat("PlayerRotX",player.transform.eulerAngles.x);
        PlayerPrefs.SetFloat("PlayerRotY",player.transform.eulerAngles.y);
        PlayerPrefs.SetFloat("PlayerRotZ",player.transform.eulerAngles.z);
        PlayerPrefs.SetFloat("CamRotX",camTransform.eulerAngles.x);
        PlayerPrefs.SetFloat("CamRotY",camTransform.eulerAngles.y);
        PlayerPrefs.SetFloat("CamRotZ",camTransform.eulerAngles.z);
        SceneManager.LoadScene(1);
    }
}
