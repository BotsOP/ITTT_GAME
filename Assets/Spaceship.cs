using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class Spaceship : MonoBehaviour
{
    public Rigidbody2D rb;
    public float brakingSpeed;
    public float shipSpeed;
    
    private SerialPort dataStream = new SerialPort("COM3", 115200);
    private string receivedString;
    private int frameCount;
    void Start()
    {
        dataStream.Open();
    }

    void Update()
    {
        receivedString = dataStream.ReadLine();

        string[] datas = receivedString.Split(',');
        
        RotateShip(int.Parse(datas[0]));
        MoveShip(int.Parse(datas[1]));

        bool isBreaking = int.Parse(datas[2]) == 0;
        if (isBreaking)
        {
            BrakeShip();
        }
        
        if (frameCount % 20 == 0)
        {
            
        }

        frameCount++;
    }

    private void MoveShip(int input)
    {
        //Debug.Log(input);
        float amount = 0;
        if (input > 25 && rb.velocity.magnitude > 0)
        {
            Debug.Log("not inputting but moving");
            Vector2 force = -rb.velocity * brakingSpeed;
            rb.AddForce(force);
        }
        else
        {
            amount = Remap(input, 30, 0, 0, shipSpeed);
            Debug.Log(amount);
            Vector2 force = transform.up * amount;
            rb.AddForce(force);
        }
    }

    private void RotateShip(int input)
    {
        float rotation = Remap(input, 0, 1023, -180, 180);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
    }

    private void BrakeShip()
    {
        Vector2 force = -rb.velocity * brakingSpeed * 25;
        rb.AddForce(force);
    }
    
    public float Remap (float from, float fromMin, float fromMax, float toMin,  float toMax)
    {
        var fromAbs  =  from - fromMin;
        var fromMaxAbs = fromMax - fromMin;      
       
        var normal = fromAbs / fromMaxAbs;
 
        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;
 
        var to = toAbs + toMin;
       
        return to;
    }
}
