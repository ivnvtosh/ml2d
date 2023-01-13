using UnityEngine;

public class RewardController : MonoBehaviour
{
    private Environment _environment;

    private void Start()
    {
        var gameObject = GameObject.FindWithTag("GameController");
        _environment = gameObject.GetComponent<Environment>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agent"))
        {
            // _environment.isCollided = true;
        }
    }
}
