// using UnityEngine;

// public class HandManager : MonoBehaviour
// {

//     private List<GameObject> hand = new List<GameObject>();

//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
//         // spawnCard()
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     // void spawnCard()
//     // {
//     //     for (int i = 0; i < 5; i++)
//     //     {
//     //         int offset = i * 5;

//     //         GameObject newcard = Instantiate(cardObject, cardSpawnPoint.position + new Vector3(offset, 0, 0), Quaternion.identity);
//     //         newcard.GetComponent<Card>().currentShape = (Card.ShapeType)Random.Range(0, 2);

//     //         List<List<bool>> shapeMatrix = newcard.GetComponent<Card>().GetShapeMatrix();

//     //         // Add clicker to Object
//     //         var clicker = newcard.AddComponent<ClickCallback>();
//     //         clicker.OnClickAction = () => updatePreview(shapeMatrix, "OR");

//     //         // Add card to hand
//     //         hand.Add(newcard);
//     //     }

//     // }
// }
