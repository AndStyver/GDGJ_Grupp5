using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    const float hour = 60f;
    const double second = 1f / 60f;
    double timeStep = 0f;

    float currentTime;
    [SerializeField] Image targetImage;

    private void Start()
    {
        currentTime = 0f;
        targetImage.fillAmount = currentTime;
        timeStep = 0f;
    }

    private void Update()
    {
        currentTime += (1 / hour) * Time.deltaTime;

        targetImage.fillAmount = currentTime;
        timeStep = 0;
    }
}
