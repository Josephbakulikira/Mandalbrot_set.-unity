using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{
    private float smoothA;
    private Vector2 smoothP;
    private float smoothS;

    [SerializeField]
    private Material material;
    [SerializeField]
    private Vector2 p; // position
    [SerializeField]
    private float s; //scale of the mandalbrot
    [SerializeField]
    private float a;
    
    

    void FixedUpdate()
    {
        UpdateShafer();
        handleInput();
    }
    void handleInput()
    {
        //scale
        if (Input.GetKey(KeyCode.KeypadPlus) || Input.GetKey(KeyCode.E))
            s *= 0.99f;
        if (Input.GetKey(KeyCode.KeypadMinus) || Input.GetKey(KeyCode.Q))
            s *= 1.01f;
        //angle
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Keypad4))
            a -= 0.01f;
        if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.Keypad6))
            a += 0.01f;
        //directtions

        Vector2 direction = new Vector2(0.01f * s, 0);
        float si = Mathf.Sin(a);
        float co = Mathf.Cos(a);
        direction = new Vector2(direction.x * co - direction.y * si, direction.x * si + direction.y * co);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            p -= direction;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            p += direction;
        direction = new Vector2(-direction.y, direction.x);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            p += direction;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            p -= direction;
        
    }
    private void UpdateShafer() {
        smoothP = Vector2.Lerp(smoothP, p, 0.04f);
        smoothS = Mathf.Lerp(smoothS, s, 0.04f);
        smoothA = Mathf.Lerp(smoothA, a, 0.04f);
        float aspect = (float)Screen.width / (float)Screen.height;
        float sx = smoothS; // scale x
        float sy = smoothS; // scale y

        if (aspect > 1f)
        {
            sy /= aspect;
        }
        else
        {
            sx *= aspect;
        }

        material.SetVector("_Surface", new Vector4(smoothP.x, smoothP.y, sx, sy));
        material.SetFloat("_Angle", smoothA);
    }
}
