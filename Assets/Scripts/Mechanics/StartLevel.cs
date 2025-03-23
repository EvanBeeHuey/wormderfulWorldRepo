using UnityEngine;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    void Start() => GameManager.Instance.InstantiatePlayer(startPos);
}
