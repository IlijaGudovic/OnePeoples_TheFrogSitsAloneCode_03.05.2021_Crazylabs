using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFolow3D : MonoBehaviour
{

    [Header("Manual Settings")]
    public Vector3 cameraOffset = Vector3.zero;
    public int perspectiveAngle = 60; //Camera point of view
    public float smoothSpeed = 2f; //Camera movment speed

    public bool followAlwayse = false;


    [Header("Default Settings")]
    [SerializeField] private Transform target = null;
    [SerializeField] private Transform pivot = null;


    private Vector3 desiredPosition;
    private Vector3 smoothPosition;
    private FrogMovment3D targetScript;

    private void Start()
    {

        targetScript = target.gameObject.GetComponent<FrogMovment3D>();

        //Set Offset
        pivot.localPosition = cameraOffset;

    }

    public void onChangeValue()
    {

        pivot.localPosition = cameraOffset;

    }

    private void Update()
    {


        //Camera Rotations

        Vector3 newRotation = new Vector3(perspectiveAngle, target.transform.eulerAngles.y, 0);

        gameObject.transform.eulerAngles = newRotation;


        //Camera Position

        if (followAlwayse == true)
        {

            transform.position = pivot.position;

            return;

        }

        if (targetScript.jumpRestarting == true) //Transfom camera position only if frog stands
        {

            smoothPosition = Vector3.Lerp(transform.position, pivot.position, smoothSpeed * Time.fixedDeltaTime);

            smoothPosition.y = cameraOffset.y;

            transform.position = smoothPosition;

        }

    }

}
