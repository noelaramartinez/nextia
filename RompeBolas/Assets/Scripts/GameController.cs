using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject block;
    public GameObject Apple;
    public GameObject StrawBerry;
    public GameObject Banana;
    public GameObject Cherry;
    public GameObject Orange;
    private GameObject[] liFruits;
    private Rigidbody2D rb;
    private Queue q;
    private Dictionary<int, ActionBlock> mapMosaic;
    private Dictionary<string, ActionBlock> mapPositions;
    private GameObject elementInstance;
    public static GameController instance;

    public Dictionary<int, ActionBlock> MapMosaic { get => mapMosaic; set => mapMosaic = value; }
    public Queue Q { get => q; set => q = value; }
    public Dictionary<string, ActionBlock> MapPositions { get => mapPositions; set => mapPositions = value; }
    public Rigidbody2D Rb { get => rb; set => rb = value; }
    public GameObject ElementInstance { get => elementInstance; set => elementInstance = value; }

    void Awake()
    {
        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);

        mapMosaic = new Dictionary<int, ActionBlock>();
        mapPositions = new Dictionary<string, ActionBlock>();
        q = new Queue();
    }

    void Start()
    {
        liFruits = new GameObject[] { Apple, Banana, Cherry, Orange, StrawBerry, block};

        if (liFruits!=null)
        {
            ActionController.instance.GenerateFirstPattern(liFruits);
        }
        else
        {
            print("el arreglo defrutas es nulo");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }


}
