using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.ParticleSystem;

public class BubbleComponent : MonoBehaviour
{
    //public GameObject mainPanel;
    public RectTransform rt;
    private float sway = 0;
    private float swaydivide = 0.1f;
    private float startbounds = 890;
    private float upwardsspeed = 50f;
    public bool canPop = true;
    public GameObject particle;
    public AudioSource AudioComponent;


    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upwardsspeed = upwardsspeed + Random.Range(1,20);
        // rt.anchoredPosition = new Vector3(Random.Range(left.position.x,right.position.x),-417,0);

        StartCoroutine(LifeSpan());
    }


    // Update is called once per frame
    void Update()
    {
        sway = sway + 7 *Time.deltaTime;
        //Debug.Log(Mathf.Sin(sway));
        rt.position = rt.position + new Vector3(Mathf.Sin(sway)*swaydivide,upwardsspeed * Time.deltaTime,0);//new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);
    }


    public void Popped(){

        if (!canPop)
        {
            return;
        }
        Debug.Log("Pop!");

        AudioComponent.Play();
        FindObjectOfType<BubbleSpawners>().spawnBubbles = false;
        FindObjectOfType<BubbleSpawners>().DestroyAllBubbes();



            EventManager eventManager = FindObjectOfType<EventManager>();
            if (eventManager)
            {
                eventManager.TriggerChoice();
            }
        

        DestroyMe();
    }

    public void DestroyMe()
    {
        if (canPop)
        {
            StartCoroutine(DestroyBubble());
        }
    }

    IEnumerator DestroyBubble()
    {
        canPop = false;

        // DO SOMETHING HERE LIKE PARTICLE EFFECT
        // PLAY POP SOUND
        GetComponent<Image>().enabled = false;
        particle.SetActive(true);


        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    //lifespawn
    IEnumerator LifeSpan()
    {  
        yield return new WaitForSeconds(70);
        Destroy(gameObject);
    }
}
