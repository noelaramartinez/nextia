using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBlock
{
    private int id;
    private int mapPosition;
    private GameObject actionMosaic;

    public ActionBlock()
    {

    }

    public ActionBlock(int id, int mapPosition, GameObject actionMosaic)
    {
        this.id = id;
        this.mapPosition = mapPosition;
        this.actionMosaic = actionMosaic;
    }

    public int Id { get => id; set => id = value; }
    public int MapPosition { get => mapPosition; set => mapPosition = value; }
    public GameObject ActionMosaic { get => actionMosaic; set => actionMosaic = value; }
}
