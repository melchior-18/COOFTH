using UnityEngine;

public class CamPartController : MonoBehaviour
{
    private GameObject square;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        square = GameObject.Find("Idle_0");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(square.transform.position.x, square.transform.position.y, -10f);
    }
}
