using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    public float MoveSpeed = 5f;
    public float SteerSpeed = 180f;
    public float BodySpeed = 5f;
    public int Gap = 10;
    public GameObject BodyPrefab;
    public GameObject FoodPrefab;
    private  ScoreManager scoreManager;
    public GameObject playAgainButton;
    public TextMeshProUGUI lose;

    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();

     void Start()
     {

            Vector3 randomPosition = new Vector3(Random.Range(-31.0f, -14.0f), 0.4f, Random.Range(7.0f, -10.0f));
            Instantiate(FoodPrefab, randomPosition, Quaternion.identity);

     }

    void Update() 
    {

        MoveForward();
        Steer();
        StorePositionHistory();
        MoveBodyParts();
    }

    void MoveForward() {
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
    }

    void Steer() {
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);
    }

    void StorePositionHistory() {
        PositionsHistory.Insert(0, transform.position);
    }
    void GrowSnake() 
    {
         GameObject body = Instantiate(BodyPrefab);
         if (BodyParts.Count > 0) {
                body.transform.position = BodyParts[BodyParts.Count - 1].transform.position;
         }
         BodyParts.Add(body);
    }
    void MoveBodyParts() {
        int index = 0;
        foreach (var body in BodyParts) {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject); 
            GrowSnake();
           
            Vector3 randomPosition = new Vector3(Random.Range(-31.0f, -14.0f), 0.5f, Random.Range(7.0f, -10.0f));
            Instantiate(FoodPrefab, randomPosition, Quaternion.identity);
                ScoreManager.score+=1; 
        }
        if (other.CompareTag("fence"))
        {
            Debug.Log("you lose Try Again");
            Time.timeScale = 0f;
            lose.enabled = true;
            playAgainButton.SetActive(true);
        }
    }


   
}
