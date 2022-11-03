using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPaintingsScript : MonoBehaviour
{
    GameObject paintinsParentObject;
    [SerializeField] GameObject[] paintings;

    public void ShowNewPaintings()
    {
        for (int i = 0; i < paintings.Length; i++)
        {
            int show = Random.Range(0, 1);
            if (show == 0) { paintings[i].SetActive(true); }
            else { paintings[i].SetActive(false); }
        }
    }
}
