using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    public string nameOfTerrain = "Terrain";
    public int camMoveSpeed = 80;
    public int mapBorder = 50;
    private static string myTerrain;
    private Vector3 camShift;
    private static int firstJumpMove;
    private static int smoothMoveSpeed;
    private static int borderForMouse;
    private static int borderForMapx1;
    private static int borderForMapx2;
    private static int borderForMapy1;
    private static int borderForMapy2;
    private float firstRayDistance;
    private Ray rayRangeFromScreen;
    private RaycastHit hit;
    

	// Inicializace, dosazeni hodnot do promennych
    void Start () {
        myTerrain = nameOfTerrain;
        firstJumpMove = 20;
        smoothMoveSpeed = camMoveSpeed;
        borderForMouse = 30;
        borderForMapx1 = mapBorder;
        borderForMapx2 = mapBorder;
        borderForMapy1 = mapBorder;
        borderForMapy2 = mapBorder;
        firstRayDistance = -1;
	}
	
	// Update is called once per frame

    
	void Update () {
        
        // if keyboard input
        if (keyboardMovementInput())
        {
            //ulozeni posunu kamery do vectoru
            camKeyboardShift();
            //test jestli se kamera muze posunout, v ramci zvoleneho ohraniceni
            if(camCanMoveByShift())
                //posun
                this.gameObject.transform.position = camShift;
        }

        // if mouse input
        if (mouseMovementInput())
        {
            //ulozeni posunu kamery do vectoru
            camMouseShift();
            //test jestli se kamera muze posunout, v ramci zvoleneho ohraniceni
            if (camCanMoveByShift())
                //posun
                this.gameObject.transform.position = camShift;
        }

        //raycast ze stredu obrazovky dolu
        rayRangeFromScreen = new Ray(this.gameObject.transform.position+new Vector3(0,0,20),-Vector3.up);
        //vykresleni paprsku
        //Debug.DrawRay(rayRangeFromScreen.origin, rayRangeFromScreen.direction * 1000, Color.blue);
        
        //jestlize paprsek neco hitne
        if (Physics.Raycast(rayRangeFromScreen, out hit, 1000))
        {
            // a jestlize to bude teren
            if (hit.collider.name == myTerrain)
            {
                // tak pokud se nejak zmeni delka paprsku od prvniho vystreleneho paprsku, pak se kamera oddali, ci priblizi
                if (firstRayDistance < 0)
                {
                    firstRayDistance = hit.distance;
                }
                else if (firstRayDistance > hit.distance + 2)
                {
                    Vector3 ff = this.gameObject.transform.position;
                    ff.y += 50F*Time.deltaTime;
                    this.gameObject.transform.position = ff;
                }
                else if (firstRayDistance < hit.distance - 2)
                {
                    Vector3 ff = this.gameObject.transform.position;
                    ff.y -= 50F*Time.deltaTime;
                    this.gameObject.transform.position = ff;
                }
                
            }
        }
        
	
	}

    private bool keyboardMovementInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            return true;
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            return true;
        else
            return false;
    }

    private void camKeyboardShift()
    {
        camShift.x=camShift.y=camShift.z=0;
        if (Input.GetKeyDown(KeyCode.W))
            camShift.z = firstJumpMove;
        if (Input.GetKeyDown(KeyCode.S))
            camShift.z = -firstJumpMove;
        if (Input.GetKeyDown(KeyCode.A))
            camShift.x = -firstJumpMove;
        if (Input.GetKeyDown(KeyCode.D))
            camShift.x = +firstJumpMove;
        if (Input.GetKey(KeyCode.W))
            camShift.z = smoothMoveSpeed*Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            camShift.z = -smoothMoveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            camShift.x = -smoothMoveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            camShift.x = +smoothMoveSpeed * Time.deltaTime;
        camShift += this.gameObject.transform.position;
    }

    private bool mouseMovementInput()
    {
        if (Input.mousePosition.x <= 0 || Input.mousePosition.x >= Screen.width || Input.mousePosition.y <= 0 || Input.mousePosition.y >= Screen.height)
            return false;
        else if (Input.mousePosition.x < borderForMouse || Input.mousePosition.x > (Screen.width - borderForMouse) || Input.mousePosition.y < borderForMouse || Input.mousePosition.y > (Screen.height - borderForMouse))
            return true;
        else
            return false;
    }

    private void camMouseShift()
    {
        camShift.x = camShift.y = camShift.z = 0;
        if (Input.mousePosition.x < borderForMouse)
            camShift.x = -smoothMoveSpeed * Time.deltaTime;
        if (Input.mousePosition.x > (Screen.width - borderForMouse))
            camShift.x = smoothMoveSpeed * Time.deltaTime;
        if (Input.mousePosition.y < borderForMouse)
            camShift.z = -smoothMoveSpeed * Time.deltaTime;
        if (Input.mousePosition.y > (Screen.height - borderForMouse))
            camShift.z = +smoothMoveSpeed * Time.deltaTime;
        camShift += this.gameObject.transform.position;
    }

    private bool camCanMoveByShift()
    {

        if (camShift.x < borderForMapx1 || camShift.z < borderForMapy1)
            return false;
        else
            return true;
    }
}
