using UnityEngine;

public class RewardController : MonoBehaviour
{
    private GameController _gameController;

    private void Start()
    {
        var gameObject = GameObject.FindWithTag("GameController");
        _gameController = gameObject.GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agent"))
        {
            _gameController.Success();
        }
    }
}
