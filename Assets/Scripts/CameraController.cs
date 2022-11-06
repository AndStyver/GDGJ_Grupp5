using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 currentRoom;
    Vector3 nextRoom;

    [SerializeField] [Range(0.1f, 4)] float transitionTime = 1f;

    bool moveToNext = false;
    private float timer = 0;
    private float lerp = 0;
    // Start is called before the first frame update
    void Start()
    {
        nextRoom = new Vector3(17.1800003f, 0, transform.position.z);
    }

    void Update()
    {
        if(moveToNext)
            MoveCamera(currentRoom, nextRoom);
    }
    void MoveCamera(Vector3 startPosition, Vector3 endPosition)
    {
        //Save previous lerp value 
        float prevLerp = lerp;
        timer += Time.deltaTime/transitionTime;
        lerp = Mathf.Sin(timer);
        //if we are on the down slope of the sine angle
        if (prevLerp > lerp)
            lerp = 1;

        transform.position = Vector3.Lerp(startPosition, endPosition, lerp);

        //Mathf.Cos(180f / timer);

        if (lerp == 1)
        {
            //transform.position = endPosition;
            moveToNext = false;
            nextRoom = startPosition;
            timer = 0;
            lerp = 0;
        }
    }

    public void moveToNextRoom()
    {
        moveToNext = true;
        currentRoom = transform.position;
    }
}
