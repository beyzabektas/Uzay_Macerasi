using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    PolygonCollider2D polygonCollider2D;
    float randomHiz;
    bool hareket;

    float min, max;

    public bool Hareket
    {
        get
        {
            return hareket;
        }
        set
        {
            hareket = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        
        
        if (Secenekler.KolayDegerOku() == 1)
        {
            randomHiz = Random.Range(0.2f, 0.8f);
        }
        if (Secenekler.OrtaDegerOku() == 1)
        {
            randomHiz = Random.Range(0.8f, 1.0f);
        }
        if (Secenekler.ZorDegerOku() == 1)
        {
            randomHiz = Random.Range(1.0f, 1.5f);
        }


        float objeGenislik = polygonCollider2D.bounds.size.x / 2;
        //Obje ekranin sag tarafindaysa
        if (transform.position.x > 0)
        {
            min = objeGenislik;
            max = EkranHesaplayicisi.instance.Genislik - objeGenislik;
        }
        else
        {
            min = -EkranHesaplayicisi.instance.Genislik + objeGenislik;
            max = -objeGenislik;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hareket)
        {
            float pingPongX = Mathf.PingPong(Time.time * randomHiz, max - min) + min;
            Vector2 pingPong = new Vector2(pingPongX, transform.position.y);
            transform.position = pingPong;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ayaklar")
        {
            GameObject.FindGameObjectWithTag("Player").transform.parent = transform;
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<OyuncuHareket>().ZiplamayiSifirla();
        }
    }

}
