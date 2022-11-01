using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    const float hour = 60f;
    const float second = 1f / hour;
    //double timeStep = 0f;

    float currentTime;
    [SerializeField] Image targetImage;

    private void Start()
    {
        currentTime = 0f;
        targetImage.fillAmount = currentTime;

    }

    private void Update()
    {
        currentTime += second * Time.deltaTime;

        targetImage.fillAmount = currentTime;
    }
}
