using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//public enum side
//{
//    Left,
//    Right
//}
public class Move : MonoBehaviour
{
    public GameObject movingPlatform;
    public GameObject TextPrice;
    public GameObject Camera;
    public Transform endPoint;
    public int Speed;

    private bool offsetActivate = false;
    private float offsetValue;

    [SerializeField] private bool start;

    //private DataObj data;

    private void Awake()
    {
        offsetValue = 0;
        EasyInputManager.instance.swipeCallback += Moving;
        //data = (DataObj)Resources.Load("DataObj");
    }

    private void Update()
    {
        if (start)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
                Vector3.MoveTowards(gameObject.transform.position, endPoint.position, Time.deltaTime * Speed).z);

            if (gameObject.transform.position.z == endPoint.position.z)
            {
                if (movingPlatform.transform.position.x > 0 && movingPlatform.transform.eulerAngles.y > -160f)
                {
                    offsetValue -= 1f * Time.deltaTime;
                    movingPlatform.transform.eulerAngles = new Vector3(0, Mathf.Lerp(-160f, -70f, offsetValue), 0);

                    Camera.transform.localPosition = new Vector3(0, Mathf.Lerp(3f, 6f, offsetValue), Mathf.Lerp(0f, -1f, offsetValue));
                    Camera.transform.eulerAngles = new Vector3(Mathf.Lerp(30f, 40f, offsetValue), Mathf.Lerp(25f, 0f, offsetValue), 0);
                }
                else if (movingPlatform.transform.position.x < 0 && movingPlatform.transform.eulerAngles.y < 90f)
                {
                    offsetValue += 1f * Time.deltaTime;
                    movingPlatform.transform.eulerAngles = new Vector3(0, Mathf.Lerp(0f, 90f, offsetValue), 0);

                    Camera.transform.localPosition = new Vector3(0, Mathf.Lerp(6f, 3f, offsetValue), Mathf.Lerp(-1f, 0f, offsetValue));
                    Camera.transform.eulerAngles = new Vector3(Mathf.Lerp(40f, 30f, offsetValue), Mathf.Lerp(0f, -25f, offsetValue), 0);
                }
                TextPrice.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Finish")
        {
            Debug.Log("Finish Game");
            MainData.carsAvailable[PlayerController.instance.currentCarId] = 1;
            PlayerPrefs.SetInt("carsAvailable" + PlayerController.instance.currentCarId.ToString(), MainData.carsAvailable[PlayerController.instance.currentCarId]);
            Invoke(nameof(StopGame), 5f);
        }
    }

    private void StopGame()
    {
        start = false;
        Debug.Log("Game Stopped");
    }

    public void Moving(SwipeType swipeType)
    {
        if (gameObject.transform.position.z != endPoint.position.z && !offsetActivate)
        {
            offsetActivate = true;
            StopAllCoroutines();
            StartCoroutine(offset(swipeType));
        }
    }

    public IEnumerator offset(SwipeType _side)
    {
        while (offsetActivate)
        {
            if (_side == SwipeType.RIGHT)
            {
                offsetValue += 0.02f;
            }
            else if (_side == SwipeType.LEFT)
            {
                offsetValue -= 0.02f;
            }
            movingPlatform.transform.position = new Vector3(Mathf.Lerp(-2.75f, 2.75f, offsetValue), movingPlatform.transform.position.y, movingPlatform.transform.position.z);
            movingPlatform.transform.eulerAngles = new Vector3(0, Mathf.Lerp(0f, -70f, offsetValue), 0);
            TextPrice.transform.position = new Vector3(Mathf.Lerp(3.5f, 8.4f, offsetValue), TextPrice.transform.position.y, TextPrice.transform.position.z);

            if (offsetValue >= 1f ||  offsetValue <= 0f)
            {
                if (offsetValue > 1)
                {
                    offsetValue = 1;
                }
                else if (offsetValue < 0)
                {
                    offsetValue = 0;
                }
                offsetActivate = false;
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
}
