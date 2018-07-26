using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    private float mouseX;
    private float mouseY;

    private GameObject Player;

    void LateStart()
    {
        Player = GameObject.Find("Player");
        //transform.LookAt(Player.transform);
        transform.position = Player.transform.position - Player.transform.forward + Player.transform.up;
    }

    void Update()
    {
        if (Input.GetKey("w"))
        {
            // Movemos hacia arriba la camara.
            UnityThread.executeInUpdate(() => { transform.position += transform.forward * 0.1f; });
        }
        if (Input.GetKey("s"))
        {
            // Movemos hacia abajo la camara.
            UnityThread.executeInUpdate(() => { transform.position -= transform.forward * 0.1f; });
        }
        if (Input.GetKey("a"))
        {
            // Movemos hacia la izquierda la camara.
            UnityThread.executeInUpdate(() => { transform.position -= transform.right * 0.1f; });
        }
        if (Input.GetKey("d"))
        {
            // Movemos hacia la derecha la camara.
            UnityThread.executeInUpdate(() => { transform.position += transform.right * 0.1f; });
        }

        // Si queremos subir la camara...
        if (Input.GetKey(KeyCode.Space))
        {
            UnityThread.executeInUpdate(() => { transform.position += transform.up * 0.1f; });
        }
        // Si queremos bajar la camara...
        if (Input.GetKey(KeyCode.X))
        {
            UnityThread.executeInUpdate(() => { transform.position -= transform.up * 0.1f; });
        }
        // Si queremos reiniciar la rotacion de la camara.
        if (Input.GetKey(KeyCode.R))
        {
            UnityThread.executeInUpdate(() => { transform.rotation = Quaternion.identity; });
        }

    }

    void LateUpdate()
    {
        HandleMouseRotation();

        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
    }

    void HandleMouseRotation()
    {
        if (Input.GetMouseButton(0))
        {
            UnityThread.executeInUpdate(() =>
            {
                var cameraRotationY = (Input.mousePosition.x - mouseX) * 10.0f * Time.deltaTime;
                transform.Rotate(0.0f, cameraRotationY, 0.0f);
            });
        }
        if (Input.GetMouseButton(1))
        {
            UnityThread.executeInUpdate(() =>
            {
                var cameraRotationX = (Input.mousePosition.y - mouseY) * 10.0f * Time.deltaTime;
                transform.Rotate(cameraRotationX, 0.0f, 0.0f);
            });
            
        }
    }

    private void Awake()
    {
        UnityThread.initUnityThread();
    }

}
