using System.Collections;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private Transform sprite;
    [SerializeField]
    float rotateSpeed = 5.0f;
    [SerializeField]
    GameObject targetCamera;

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
    }
    private void LateUpdate()
    {
        SetRotation(targetCamera.transform.eulerAngles.x, targetCamera.transform.eulerAngles.y);
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
        angleY = 360 - Mathf.Abs(_angleY);
    }
    IEnumerator RotateSprite(Direction direction)
    {
        isIdle = false;
        float startAngleX = sprite.transform.eulerAngles.x;
        float startAngleY = sprite.transform.eulerAngles.y;
        float t = 0f;
        float speed = 0f;

        if (direction == Direction.Left)
        {
            targetAngleX = Mathf.Abs(angleX);
            targetAngleY = Mathf.Abs(angleY);
        }
        else
        {
            targetAngleX = -Mathf.Abs(angleX);
            targetAngleY = 180 - Mathf.Abs(angleY);
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
}
