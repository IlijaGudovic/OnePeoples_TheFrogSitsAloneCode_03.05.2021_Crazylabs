using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyCustomization : MonoBehaviour
{

    [SerializeField] private bool customSize = false;
    [SerializeField] private Vector2 randomizeSize = Vector2.zero;

    [SerializeField] private float Range = 10;
    [SerializeField] private Color color = Color.blue;

    [Header("Flower Settings")]
    [SerializeField] private bool haveFlower = true;
    [SerializeField] private Vector3 flowerOffset = Vector3.zero;
    [SerializeField] private GameObject flower = null;

    [SerializeField] private List<Color> colorPicker = new List<Color>();

    private void Start()
    {

        if (customSize != true)
        {

            float scale = Random.Range(randomizeSize.x, randomizeSize.y);

            transform.localScale = new Vector3(scale, transform.localScale.y, scale);

        }

        Vector3 newRotation = transform.eulerAngles;
        newRotation.y = Random.Range(0, 360);
        transform.eulerAngles = newRotation;

        if (haveFlower == true)
        {
            spawnFlower();
        }

    }

    private void spawnFlower()
    {

        GameObject spawnedFlower = Instantiate(flower, transform.position + flowerOffset, Quaternion.identity);


        float scale = Random.Range(0.6f, 0.8f);
        spawnedFlower.transform.localScale = new Vector3(scale, scale, scale);

        spawnedFlower.transform.SetParent(transform);


        int randomColor = Random.Range(0, colorPicker.Count);

        Material reusableMatirial = null;

        for (int i = 0; i < 19; i++) //Painting Flower
        {

            reusableMatirial = spawnedFlower.transform.GetChild(i).GetComponent<MeshRenderer>().material;

            reusableMatirial.SetColor("_Color", colorPicker[randomColor]);

            reusableMatirial.SetFloat("_Metallic", 0.1f);

            reusableMatirial.SetFloat("_Glossiness", 0.5f);

        }

    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = color;

        Gizmos.DrawWireSphere(transform.position, Range);

    }

}
