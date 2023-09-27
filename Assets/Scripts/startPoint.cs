using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPoint : MonoBehaviour
{
    public string StartPoint; //�� �̵� �� �÷��̾� ���� ��ġ
    private MovingObject thePlayer;
    private CameraManager theCamera;

    private void Start()
    {
        thePlayer = FindObjectOfType<MovingObject>();
        theCamera = FindObjectOfType<CameraManager>();

        if (StartPoint == thePlayer.currentMapName)
        {
            thePlayer.transform.position = this.transform.position;
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
        }
    }
}
