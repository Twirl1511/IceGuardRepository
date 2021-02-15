using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewForceField : MonoBehaviour
{
    public static float lifeTime = 60f;
    public static float NewLifeTime;
    public float SecondsToDestroy;
    [SerializeField] private GameObject gameObjectMaterial;
    private Material _material;
    private float _alphaValue;

    void Start()
    {
        _material = gameObjectMaterial.GetComponent<Renderer>().material;
        NewLifeTime = lifeTime;
        SecondsToDestroy = 0.0001f;
        //StartCoroutine(LaterDestroy(SecondsToDestroy));
    }
    private void Update()
    {
        AdjustedLifeTimer();
        LifeTimer();
        ChangeAlpha(_material, Transperency());
    }

    public void ChangeAlpha(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newClor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newClor);


    }

    public float Transperency()
    {
        float transperency;
        if((transperency = (0.1f * NewLifeTime) / SecondsToDestroy) < 0.1f)
        {
            return 0.1f;
        }
        else
        {
            return transperency;
        }
    }

    public void LifeTimer()
    {
        SecondsToDestroy += Time.deltaTime;
        if (SecondsToDestroy >= NewLifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    public void AdjustedLifeTimer()
    {
        
        if (PlayerControllerDrawPath.allForceFields.Count == 1 
            || PlayerControllerDrawPath.allForceFields.Count == 0)
        {
            NewLifeTime = lifeTime;
        }else
        {
            NewLifeTime = Mathf.Pow(0.9f, PlayerControllerDrawPath.allForceFields.Count - 1) * lifeTime;
        } 
    }

    IEnumerator LaterDestroy(float seconds = 40)
    {
        
        yield return new WaitForSeconds(seconds);
        
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHitPoints.HitPoints--;
            
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Meteorite"))
        {
            StartCoroutine(LaterDestroy(1));
            
        }
    }
}
