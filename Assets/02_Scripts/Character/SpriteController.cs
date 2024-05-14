using System.Collections;
using UnityEditor.Build;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private Transform sprite;
    [SerializeField]
    float rotateSpeed = 5.0f;

    GameObject[] targetCameras;
    [SerializeField]
    GameObject mainCamera;

    int carmeraIndex;
    Animator anim;
    float targetAngleX;
    float targetAngleY;
    float angleX;
    float angleY;
    bool isIdle;
    Direction direction;
    void Awake()
    {
        sprite = this.transform.GetChild(0);
        anim = sprite.GetComponent<Animator>();
        isIdle = true;
        direction = Direction.Left;
        carmeraIndex = 0;
    }
    public void SetMainCamera(GameObject _mainCamera, GameObject[] _targetCameras)
    {
        mainCamera = _mainCamera;
        targetCameras = _targetCameras;
        mainCamera.GetComponent<CameraController>().ChangePos(targetCameras[carmeraIndex].transform);
    }
    private void LateUpdate()
    {
        SetRotation(mainCamera.transform.eulerAngles.x, mainCamera.transform.eulerAngles.y);
        if (isIdle)
        {
            if (direction == Direction.Left)
            {
                StartCoroutine(RotateSprite(Direction.Left));
            }
            else
            {
                StartCoroutine(RotateSprite(Direction.Right));
            }
        }
    }
    private void OnTriggerEnter(Collider other)  //임시 카메라 변경
    {
        Debug.Log("collider Enter");
        if (other.gameObject.name == "Camera1Zone")
        {
            carmeraIndex = 1;
            mainCamera.GetComponent<CameraController>().ChangePos(targetCameras[carmeraIndex].transform);
        }
    }
    private void OnTriggerExit(Collider other)  //임시 카메라 변경
    {
        Debug.Log("collider Exit");
        if (other.gameObject.name == "Camera1Zone")
        {
            carmeraIndex = 0;
            mainCamera.GetComponent<CameraController>().ChangePos(targetCameras[carmeraIndex].transform);
        }
    }
    public void SetAnimState(AnimState state)
    {
        switch(state)
        {
            case (AnimState.Idle):
                anim.SetInteger("State", (int)AnimState.Idle);
                break;
            case (AnimState.Move):
                anim.SetInteger("State", (int)AnimState.Move);
                break;
            case (AnimState.Hit):
                anim.SetInteger("State", (int)AnimState.Hit);
                break;
            case (AnimState.Attack):
                anim.SetInteger("State", (int)AnimState.Attack);
                break;
            //case (AnimState.Roll):
            //    anim.SetInteger("State", (int)AnimState.Roll);
            //    break;
        }
    }
    public void Flip(Direction _direction)
    {
        direction = _direction;
        if (direction == Direction.Left)
        {
            StartCoroutine(RotateSprite(Direction.Left));
        }
        else if (direction == Direction.Right)
        {
            StartCoroutine(RotateSprite(Direction.Right));
        }
    }
    public void SetRotation(float _angleX, float _angleY)
    {
        angleX = _angleX;
        angleY = _angleY;
        Debug.Log(angleY);
    }
    IEnumerator RotateSprite(Direction direction)
    {
        isIdle = false;
        float startAngleX = sprite.transform.eulerAngles.x;
        float startAngleY = sprite.transform.eulerAngles.y;
        float t = 0f;
        float speed = 0f;

        //if (mainCamera.transform.eulerAngles.y)
        if (direction == Direction.Left)
        {
            targetAngleX = Mathf.Abs(angleX);
            targetAngleY = Mathf.Abs(angleY) %360;
        }   
        else
        {
            targetAngleX = -Mathf.Abs(angleX);
            targetAngleY = 180 + Mathf.Abs(angleY);
        }

        while (speed < 1.0f)
        {
            t += Time.deltaTime;
            speed = rotateSpeed * t;

            float xRotation = Mathf.LerpAngle(startAngleX, targetAngleX, speed);
            float yRotation = Mathf.LerpAngle(startAngleY, targetAngleY, speed);
            sprite.transform.eulerAngles = new Vector3(xRotation, yRotation, sprite.transform.eulerAngles.z);

            yield return null;
        }
        isIdle = true;
    }
    //public int UpdateMoveDirection()
    //{
    //    if (targetCameras[carmeraIndex].transform.rotation.y < 60 && targetCameras[carmeraIndex].transform.rotation.y < 120)
    //    {

    //    }
    //    if (targetCameras[carmeraIndex].transform.rotation.y < 150 && targetCameras[carmeraIndex].transform.rotation.y < 210)
    //    {

    //    }
    //    if (targetCameras[carmeraIndex].transform.rotation.y < 240 && targetCameras[carmeraIndex].transform.rotation.y < 300)
    //    {

    //    }
    //    else
    //    {

    //    }
    //}
}
