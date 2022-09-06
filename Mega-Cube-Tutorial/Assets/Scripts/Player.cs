using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPosX;
    [Space] //inspector'da boþluk ekliyor.
    [SerializeField] private TouchSlider touchSlider; //touchslider script'ini çaðýrdýk.
    private Cube mainCube;

    private bool isPointerDown;
    private bool canMove;
    private Vector3 cubePos;

    private void Start()
    {
        SpawnCube();
        canMove = true;

        // listen to slider events;
        touchSlider.OnPointerDownEvent += OnPointerDown;
        touchSlider.OnPointerDragEvent += OnPointerDrag;
        touchSlider.OnPointerUpEvent += OnPointerUp;
    }
    private void Update()
    {
        if (isPointerDown)
        {
            mainCube.transform.position = Vector3.Lerp(mainCube.transform.position, cubePos, moveSpeed * Time.deltaTime);
        }
    }
    private void OnPointerDown()
    {
        isPointerDown = true;
    }    
    private void OnPointerDrag(float xMovement)
    {
        if (isPointerDown)
        {
            cubePos = mainCube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX;
        }
    }    
    private void OnPointerUp()
    {
        if (isPointerDown && canMove)
        {
            isPointerDown = false;
            canMove = false;

            //push the cube.
            mainCube.cubeRigidbody.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);

            Invoke("SpawnNewCube", 0.3f);

        }
    }

    private void SpawnNewCube()
    {
        mainCube.isMainCube = false;
        canMove = true;
        SpawnCube();
    }

    private void SpawnCube()
    {
        mainCube = CubeSpawner.Instance.SpawnRandom();
        mainCube.isMainCube = true;

        //reset cubepos variabla
        cubePos = mainCube.transform.position;
    }

    private void OnDestroy()
    {
        //remove listeners:
        touchSlider.OnPointerDownEvent -= OnPointerDown;
        touchSlider.OnPointerDragEvent -= OnPointerDrag;
        touchSlider.OnPointerUpEvent -= OnPointerUp;
    }
}
