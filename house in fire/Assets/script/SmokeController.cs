using UnityEngine;

public class SmokeController : MonoBehaviour
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
        if (square.GetComponent<charactèrecontroleur>().dead == false)
        {
            transform.localScale = new Vector2(26.14528f, transform.localScale.y + 1 * Time.deltaTime);
        }
    }
}
