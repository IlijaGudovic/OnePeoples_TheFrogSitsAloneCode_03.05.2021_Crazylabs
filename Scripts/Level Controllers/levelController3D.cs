using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelController3D : MonoBehaviour
{
    [Header("Scenes Settings")]
    [SerializeField] private int scenIndex = 3;
    [SerializeField] private FrogMovment3D frogScript = null;
    [SerializeField] private cameraFolow3D cameraScript = null;

    [Header("Frog Settings")]
    [SerializeField] private InputField speedFild = null;
    [SerializeField] private InputField jumpForceFild = null;
    [SerializeField] private InputField CooldownFild = null;
    [SerializeField] private InputField rotationSpeedFild = null;
    [SerializeField] private Button checkbox = null;
    [SerializeField] private GameObject settingsPanel = null;

    [Header("Camera Settings")]
    [SerializeField] private Text zoomText = null;
    [SerializeField] private Text angleText = null;
    [SerializeField] private Text distanceText = null;
    [SerializeField] private Slider zoomSllider = null;
    [SerializeField] private Slider angleSllider = null;
    [SerializeField] private Slider distanceSllider = null;
    [SerializeField] private Button cameraCheckbox = null;
    [SerializeField] private InputField cameraSpeedFild = null;
    [SerializeField] private GameObject camSettingsPanel = null;

    [Header("Score")]
    [SerializeField] private Text scoreText = null;
    private int score = 0;
    [SerializeField] private GameObject losePanel = null;
    [SerializeField] private GameObject winPanel = null;
    [SerializeField] private GameObject tutorial = null;

    private void Start()
    {

        //PlayerPrefs.DeleteAll();

        score = PlayerPrefs.GetInt("Collected Flowers", 0);
        score--;
        incromentScore();

        StartCoroutine(collectAgaing());

    }

    IEnumerator collectAgaing()
    {

        yield return new WaitForSeconds(2f);

        invertActivity(tutorial);

    }


    public void restartLevel()
    {

        SceneManager.LoadScene(scenIndex);

    }

    public void exitLevel()
    {

        Time.timeScale = 1;

        SceneManager.LoadScene(0);

    }

    #region Frog Settings

    public void openSettings()
    {

        frogScript.gamePaused = true;

        speedFild.placeholder.GetComponent<Text>().text = frogScript.speed.ToString();
        jumpForceFild.placeholder.GetComponent<Text>().text = frogScript.jumpForce.ToString();
        CooldownFild.placeholder.GetComponent<Text>().text = frogScript.jumpCooldown.ToString();
        rotationSpeedFild.placeholder.GetComponent<Text>().text = frogScript.rotationSensitivity.ToString();

        speedFild.text = frogScript.speed.ToString();
        jumpForceFild.text = frogScript.jumpForce.ToString();
        CooldownFild.text = frogScript.jumpCooldown.ToString();
        rotationSpeedFild.text = frogScript.rotationSensitivity.ToString();

        if (frogScript.fixedRotation == true)
        {
            checkbox.transform.GetChild(0).GetComponent<Text>().text = "Yes";
        }
        else
        {
            checkbox.transform.GetChild(0).GetComponent<Text>().text = "No";
        }

        invertActivity(settingsPanel);

        Time.timeScale = 0;

    }


    public void closeSettings()
    {

        frogScript.gamePaused = false;

        frogScript.speed = Mathf.Clamp(int.Parse(speedFild.text), 50 , 1000);

        frogScript.jumpForce = Mathf.Clamp(int.Parse(jumpForceFild.text), 50, 1000);

        frogScript.jumpCooldown = Mathf.Clamp(float.Parse(CooldownFild.text), 0.5f , 5f);

        frogScript.rotationSensitivity = Mathf.Abs(float.Parse(rotationSpeedFild.text));

        invertActivity(settingsPanel);

        Time.timeScale = 1;

    }

    public void chackbox()
    {

        frogScript.fixedRotation = !frogScript.fixedRotation;

        if (frogScript.fixedRotation == true)
        {
            checkbox.transform.GetChild(0).GetComponent<Text>().text = "Yes";
        }
        else
        {
            checkbox.transform.GetChild(0).GetComponent<Text>().text = "No";
        }

    }

#endregion

    #region Camera Settings

    public void openCameraSettings()
    {

        cameraSpeedFild.placeholder.GetComponent<Text>().text = cameraScript.smoothSpeed.ToString();
        cameraSpeedFild.text = cameraScript.smoothSpeed.ToString();

        zoomText.text = cameraScript.cameraOffset.y.ToString();
        angleText.text = cameraScript.transform.eulerAngles.x.ToString();
        distanceText.text = cameraScript.cameraOffset.z.ToString("F1");

        zoomSllider.value = cameraScript.cameraOffset.y;
        angleSllider.value = cameraScript.transform.eulerAngles.x;
        distanceSllider.value = cameraScript.cameraOffset.z;

        if (cameraScript.followAlwayse == true)
        {
            cameraCheckbox.transform.GetChild(0).GetComponent<Text>().text = "Yes";
        }
        else
        {
            cameraCheckbox.transform.GetChild(0).GetComponent<Text>().text = "No";
        }

        invertActivity(settingsPanel);
        invertActivity(camSettingsPanel);

    }

    public void closeCameraSettings()
    {

        cameraScript.smoothSpeed = Mathf.Clamp(float.Parse(cameraSpeedFild.text) , 1 , 5);

        invertActivity(camSettingsPanel);

        frogScript.gamePaused = false;
        Time.timeScale = 1;

    }

    public void onValueChange(int sliderIndex)
    {

        switch (sliderIndex)
        {
            case 0:
                zoomText.text = ((int)zoomSllider.value).ToString();
                cameraScript.cameraOffset.y = (int)zoomSllider.value;
                break;
            case 1:
                angleText.text = ((int)angleSllider.value).ToString();
                cameraScript.perspectiveAngle = (int)angleSllider.value;
                break;
            case 2:
                distanceText.text = distanceSllider.value.ToString("F1");
                cameraScript.cameraOffset.z = distanceSllider.value;
                break;
            default:
                Debug.Log(string.Format("The slider index '{0}' is assigned but its value is never used" , sliderIndex));
                break;
        }

        cameraScript.onChangeValue();

    }

    public void changeCameraCheckbox()
    {

        cameraScript.followAlwayse = !cameraScript.followAlwayse;

        if (cameraScript.followAlwayse == true)
        {
            cameraCheckbox.transform.GetChild(0).GetComponent<Text>().text = "Yes";
        }
        else
        {
            cameraCheckbox.transform.GetChild(0).GetComponent<Text>().text = "No";
        }

    }

    #endregion

    public void invertActivity(GameObject inputObj)
    {

        inputObj.SetActive(!inputObj.activeInHierarchy);

    }


  

    public void incromentScore()
    {

        score++;

        scoreText.text = score.ToString();

        PlayerPrefs.SetInt("Collected Flowers", score);

    }

    public void loseLevel()
    {
        invertActivity(losePanel);
    }

    public void winLevel()
    {
        invertActivity(winPanel);
    }

}
