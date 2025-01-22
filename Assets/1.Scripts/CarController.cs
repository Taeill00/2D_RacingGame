using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float horizontalMoveSpeed = 0.6f;
    [SerializeField] private LayerMask groundLayer;
    private float touchedXPos;

    [Header("Fuel")]
    [SerializeField] private float maxFuel;
    [SerializeField] private float curFuel;
    private WaitForSeconds decreaseTime = new WaitForSeconds(0.1f);

    [Header("UI Fuel")]
    public GameObject uiInGame;
    [SerializeField] private TextMeshProUGUI textFuel;
    [SerializeField] private Slider sliderFuel;

    [Header("UI Score")]
    public float score = 0;
    [SerializeField] private TextMeshProUGUI textScore;

    private void Start()
    {
        uiInGame.SetActive(false);

        curFuel = maxFuel;
        StartCoroutine(FuelDecreaseCo());
    }

    private void Update()
    {
        HoritontalMove();
        FuelUIUpdate();
        ScoreUIUpdate();

        if (curFuel <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

    #region Move

    private void HoritontalMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine("MoveCo");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, groundLayer);

            if (rayHit.collider != null)
            {
                touchedXPos = rayHit.point.x;
                touchedXPos = Mathf.Clamp(touchedXPos, -1, 1.2f);
            }

            Vector2 toPos = new Vector2(touchedXPos, -3);
            StartCoroutine(MoveCo(toPos));
        }
    }

    private IEnumerator MoveCo(Vector2 toPos)
    {
        while(Vector2.Distance(transform.position, toPos) >= 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, toPos, horizontalMoveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = toPos;
    }

    #endregion

    #region Fuel

    private IEnumerator FuelDecreaseCo()
    {
        while (true)
        {
            yield return decreaseTime;

            score++;
            curFuel--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fuel"))
        {
            curFuel += 30;

            if(curFuel > maxFuel)
            {
                curFuel = maxFuel;
            }

            Destroy(collision.gameObject);
        }
    }

    private void FuelUIUpdate()
    {
        textFuel.text = curFuel.ToString(); 
        sliderFuel.value = curFuel / maxFuel;
    }

    #endregion

    private void ScoreUIUpdate()
    {
        textScore.text = "Score\n" + score.ToString();
    }

}
