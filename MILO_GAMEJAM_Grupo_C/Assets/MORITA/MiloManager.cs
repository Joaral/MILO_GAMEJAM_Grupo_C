using UnityEngine;

public class MiloManager : MonoBehaviour
{


    public GameObject miloHappy;
    public GameObject miloConcentrated;
    public GameObject miloIdle;
    public GameObject miloSad;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
        miloHappy.SetActive(false);
        miloConcentrated.SetActive(false);
        miloIdle.SetActive(true);
        miloSad.SetActive(false);

    }

    public void Happy()
    {
        miloHappy.SetActive(true);
        miloConcentrated.SetActive(false);
        miloIdle.SetActive(false);
        miloSad.SetActive(false);
    }

    public void Concentrated()
    {
        miloHappy.SetActive(false);
        miloConcentrated.SetActive(true);
        miloIdle.SetActive(false);
        miloSad.SetActive(false);
    }

    public void Idle()
    {
        miloHappy.SetActive(false);
        miloConcentrated.SetActive(false);
        miloIdle.SetActive(true);
        miloSad.SetActive(false);
    }

    public void Sad()
    {
        miloHappy.SetActive(false);
        miloConcentrated.SetActive(false);
        miloIdle.SetActive(false);
        miloSad.SetActive(true);
    }


}
