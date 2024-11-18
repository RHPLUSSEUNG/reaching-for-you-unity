using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotator : MonoBehaviour
{
    GameObject camera;
    [SerializeField]
    bool isVertical;
    void Start()
    {
        camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        SetSpriteRotation(camera.transform.eulerAngles.x, camera.transform.eulerAngles.y);
    }

    public void SetSpriteRotation(float _angleX, float _angleY)   //main카메라에 맞게 스프라이트를 회전
    {
        
        if (isVertical)
        {
            this.transform.eulerAngles = new Vector3(_angleX, (_angleY+90),this.transform.eulerAngles.z);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(_angleX, _angleY, this.transform.eulerAngles.z);
        }
       
    }

}
