using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public readonly string horizontalAxisName = "Horizontal";
    public readonly string verticalAxisName = "Vertical";

    public readonly int playerNormalAttackNum = 0;

    public Vector2 moveInput { get; private set; }
    public bool Shot { get; private set; }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw(horizontalAxisName), Input.GetAxisRaw(verticalAxisName));
        Shot = Input.GetMouseButton(playerNormalAttackNum);
    }
}
