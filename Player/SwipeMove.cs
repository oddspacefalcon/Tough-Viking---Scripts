using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeMove : MonoBehaviour {

    Vector2 startPos, endPos, direction; // touch start, end position and awipe direction
    public float throwForce = 3f;
    private CharacterController2D _controllerSwipe;
    public bool directionChosen;

    public void Awake()
    {
        _controllerSwipe = GetComponent<CharacterController2D>();
        
    }


    // Update is called once per frame
    void Update () {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //if (!EventSystem.current.IsPointerOverGameObject()) {
            switch (touch.phase)
                {
                    //ifall man bara klickar
                   /* case TouchPhase.Stationary:
                        _controllerSwipe.Jump();
                        break;*/

                    //om man swipar
                    case TouchPhase.Began:
                        startPos = touch.position;
                        directionChosen = false;

                        break;

                    case TouchPhase.Ended:
                        endPos = touch.position;
                        direction = startPos - endPos; // räkna ut riktningsvektorn

                        //om riktingsvektorns längd är mindre än 9 och man ej swipar bakåt skickas vektorn till AdForce
                        float temp1 = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
                        if (temp1 > 15)
                        {
                            float theta = Mathf.Atan(direction.y / direction.x);
                            if (theta >= -Mathf.PI / 2 && theta <= Mathf.PI / 2 && -direction.x >= 0)
                            {
                                _controllerSwipe.AddForce(-direction * throwForce);

                            }
                        }
                        break;
                }
            //}
        }

 

       



        /*
        //if you touch the screen
		if(Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began)
        {
            //getting toush position and marking time when you touch the screen
          //  touchTimeStart = Time.time;
            startPos = Input.GetTouch (0).position;


            _controllerSwipe.Jump();
        }

        //if you release your finger
        if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended)
        {
           

            // getting release finger position 
            endPos = Input.GetTouch (0).position;

            //calculating swipe direction
            direction = startPos - endPos;

            
            //absolutbeloppet för direction för att se om vi bara tryckt eller swipat
            float temp1 = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
            
            
            if (temp1 < 10)
            {
                
            }        
            else
            {
                float theta = Mathf.Atan(direction.y / direction.x);
                if (theta >= -Mathf.PI/2 && theta <= Mathf.PI/2 && -direction.x>=0)
                {
                    _controllerSwipe.AddForce(-direction * throwForce);
                    
                }
            }

        }
        */
       
    }

   
}
