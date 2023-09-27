using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transferMap : MonoBehaviour
{
    public string mapName; //이동할 맵의 이름
    private MovingObject thePlayer;

    private void Start()
    {
        thePlayer = FindObjectOfType<MovingObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
            {
                thePlayer.currentMapName = mapName;
                SceneManager.LoadScene(mapName);
            }
    }
}
