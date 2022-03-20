using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public Text countCubes;
    public Text winText;
    public Text negText;
    public Text velocityBallText;
    public Text CollisionPoint;
    private int count;
    private int negCount;
    public float velocityBall;
    public List<string> cordinatesList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        negCount = 0;
        SetCountText(count);
        winText.text = "";
        negText.text = "";
        CollisionPoint.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        velocityBall = rb.velocity.magnitude;
        velocityBallText.text = "Ball Speed: " + velocityBall.ToString();

    }

    //For physics controls
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText(count);
            CollisionPoint.text = "Collision Point: " + other.transform.position.ToString();
            cordinatesList.Add(CollisionPoint.text);
        }

        if (other.gameObject.CompareTag("Negpick"))
        {
            other.gameObject.SetActive(false);
            count = count - 1;
            negCount = negCount + 1;
            SetCountText(count);
            StartCoroutine(ShowMessage("Negative point!", 2));
        }
    }

    void SetCountText(int cube_count)
    {
        countCubes.text = "Cubes collected: " + cube_count.ToString();
        if (count >= 5 || negCount > 2)
            {
            SetWinText();
            }
    }

    void SetWinText()
    {
        winText.text = "Game Over!";
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        negText.text = message;
        negText.enabled = true;
        yield return new WaitForSeconds(delay);
        negText.enabled = false;
    }
}
