using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewForceField : MonoBehaviour
{
    public static List<GameObject> allForceFields = new List<GameObject>(36);
    public static float lifeTime = 60f;
    public static float NewLifeTime;
    public float SecondsToDestroy;
    [SerializeField] private GameObject gameObjectMaterial;
    private Material _material;
    private SpriteRenderer _spriteRendere;
    [SerializeField] private Collider collider;
    private bool IsActive;
    
    public enum States
    {
        CloseToEnd,
        IDLE
    }
    private States state;

    void Start()
    {
        IsActive = true;
        state = States.IDLE;
        StartCoroutine(LaterColliderActivate());
        _material = gameObjectMaterial.GetComponent<Renderer>().material;
        _spriteRendere = gameObjectMaterial.GetComponent<SpriteRenderer>();
        ChangeAlpha(_material, 0.5f);
        NewLifeTime = lifeTime;
        SecondsToDestroy = 0.0001f;
    }
    private void Update()
    {
        AdjustedLifeTimer();
        LifeTimer();
        ChangeAlpha(_material, Transperency());
        Debug.Log(Transperency());
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
        

        if ((transperency = (0.4f * NewLifeTime) / SecondsToDestroy) < 0.05f)
        {
            
            return 0.05f;
        }
        if (transperency >= 1f)
        {
            return 1;
        }
        
        
            return transperency;
        

    }

    public void LifeTimer()
    {
        SecondsToDestroy += Time.deltaTime;
        if (SecondsToDestroy >= NewLifeTime)
        {
            Debug.Log(allForceFields.Count);
            Destroy(this.gameObject);
        }
        if(SecondsToDestroy+10f >= NewLifeTime && state == States.IDLE)
        {
            state = States.CloseToEnd;
            //StartCoroutine(Blink());
        }
    }






    IEnumerator Blink()
    {

        for (int i = 0; i < 20; i++)
        {
            IsActive = !IsActive;
            gameObjectMaterial.SetActive(IsActive);
            yield return new WaitForSeconds(0.3f);
            
        }
    }

    public void AdjustedLifeTimer()
    {

        if (allForceFields.Count == 1
            || allForceFields.Count == 0)
        {
            NewLifeTime = lifeTime;
        }
        else
        {
            NewLifeTime = Mathf.Pow(0.9f, allForceFields.Count - 1) * lifeTime;
        }
    }

    IEnumerator LaterDestroy(float seconds = 40)
    {

        yield return new WaitForSeconds(seconds);
        Debug.Log(allForceFields.Count);
        Destroy(this.gameObject);
    }

    IEnumerator LaterColliderActivate()
    {
        yield return new WaitForSeconds(PlayerControllerDrawPath.TimeToReachNextTile + 0.5f);
        collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<SoundManager>().PlaySoundOneShot(Sound.SoundName.MinusHP);
            PlayerHitPoints.HitPoints--;
            Debug.Log(allForceFields.Count);
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Meteorite"))
        {
            StartCoroutine(LaterDestroy(1));

        }
    }

    private void OnDestroy()
    {
        allForceFields.Remove(gameObject);
    }
}
