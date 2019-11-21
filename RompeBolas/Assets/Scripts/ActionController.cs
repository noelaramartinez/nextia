using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ActionController : MonoBehaviour
{

    private GameObject block;
    private readonly int leftBound = -4;
    private readonly int downBound = -4;
    private readonly int width = 5;
    private readonly int lowerHeight = 5;
    private readonly int height = 15;
    private int consecutively = 0;
    private int lastR = -1;
    private Queue q;
    private float deltaX = 0;
    private float deltaY = 0;
    private Vector3 mousePosition;
    private Vector3 elementInitPosition;
    private GameObject instanceElement;
    private bool isMatch = false;
    private readonly float minlimitDelta = .4f;
    private Vector3 vecMinLimitDelta;
    private bool isGoingRight = true;
    private bool isGoingUp = true;
    private bool isGoingLeft = true;
    private bool isGoingDown = true;
    private bool isReleased = true;
    private List<Vector2> VliToDestroy = new List<Vector2>();
    private List<Vector2> HliToDestroy = new List<Vector2>();
    public static ActionController instance;

    public ActionController()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void GenerateFirstPattern(GameObject[] liFruits)
    {
        int index = 0;
        GameObject go;
        block = liFruits[liFruits.Length - 1];

        if (liFruits != null && liFruits.Length > 0)
            for (int y = downBound; y < height; ++y)
            {
                for (int x = leftBound; x < width; ++x)
                {
                    GameObject fruit;
                    fruit = SelectRandomElement(liFruits);

                    if (y < lowerHeight)
                    {
                        Instantiate(block, new Vector3(x, y, 0), Quaternion.identity);
                        go = Instantiate(fruit, new Vector3(x, y, 0), Quaternion.identity);

                        ActionBlock ab = CreateActionBlock(go, index++);

                        GameController.instance.MapMosaic.Add(go.GetInstanceID(), ab);
                        GameController.instance.MapPositions.Add(x + "" + y, ab);
                    }
                    else
                    {
                        go = Instantiate(fruit);
                        go.layer = 8;
                        go.SetActive(false);
                        ActionBlock ab = CreateActionBlock(go, index++);
                        GameController.instance.Q.Enqueue(ab);
                    }
                }
            }
        else
        {
            print("el array es nulo en action controller");
        }
    }

    private ActionBlock CreateActionBlock(GameObject go, int index)
    {
        ActionBlock ab = new ActionBlock
        {
            Id = go.GetInstanceID(),
            MapPosition = index,
            ActionMosaic = go
        };

        return ab;
    }

    private GameObject SelectRandomElement(GameObject[] liElements)
    {
        GameObject element;

        int r = Random.Range(0, 5);

        if (lastR == r)
        {
            consecutively++;
            while (consecutively > 1 && r == lastR)
            {
                r = Random.Range(0, 5);
                if (r != lastR)
                {
                    consecutively = 0;
                }
            }
        }

        lastR = r;

        switch (r)
        {
            case 0:
                element = liElements[(int)Fruits.Apple];
                break;
            case 1:
                element = liElements[(int)Fruits.Banana];
                break;
            case 2:
                element = liElements[(int)Fruits.Cherry];
                break;
            case 3:
                element = liElements[(int)Fruits.Orange];
                break;
            case 4:
                element = liElements[(int)Fruits.Strawberry];
                break;
            default:
                element = liElements[(int)Fruits.Strawberry];
                break;
        }

        return element;
    }

    public Queue GenComplementaryActionMosaicLi(int qty, GameObject[] liFruits)
    {
        int index = 0;
        GameObject go;
        block = liFruits[liFruits.Length - 1];
        q = new Queue();

        if (liFruits != null && liFruits.Length > 0)
            for (int y = 0; y < qty; ++y)
            {
                GameObject fruit;
                fruit = SelectRandomElement(liFruits);

                go = Instantiate(fruit);

                ActionBlock ab = CreateActionBlock(go, index++);
                q.Enqueue(ab);
            }

        return q;
    }

    public void DestroyActionMosaic(int id)
    {
        Destroy(GameController.instance.MapMosaic[id].ActionMosaic);
        GameController.instance.MapMosaic.Remove(id);
    }

    public void DropActionMosaicDown(GameObject go, int offsetX, int offsetY)
    {

        go.transform.Translate(new Vector3(offsetX, offsetY, 0));

    }

    void OnMouseDown()
    {
        SelectInstanceOfElement(gameObject);

        if (gameObject.name.Contains(GameController.instance.Apple.name))
        {
            Debug.Log(GameController.instance.Apple.name);
        }
        else if (gameObject.name.Contains(GameController.instance.Banana.name))
        {
            Debug.Log(GameController.instance.Banana.name);

        }
        else if (gameObject.name.Contains(GameController.instance.Cherry.name))
        {
            Debug.Log(GameController.instance.Cherry.name);
        }
        else if (gameObject.name.Contains(GameController.instance.Orange.name))
        {
            Debug.Log(GameController.instance.Orange.name);
        }
        else if (gameObject.name.Contains(GameController.instance.StrawBerry.name))
        {
            Debug.Log(GameController.instance.StrawBerry.name);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject compareObj = col.attachedRigidbody.gameObject;
        int res = 0;

        if (instanceElement != null)
        {
            if (!instanceElement.GetInstanceID().
            Equals(compareObj.GetInstanceID()))
            {
                Debug.Log(instanceElement.GetInstanceID() + "  interseccion con: "
                + compareObj.GetInstanceID());
                Match3(instanceElement.tag, (int)compareObj.transform.position.x, (int)compareObj.transform.position.y, 1);
                Debug.Log("hay match con " + res + " elementos para el tag: " + instanceElement.tag);
                Match3(compareObj.tag, (int)elementInitPosition.x, (int)elementInitPosition.y, 2);
                Debug.Log("hay match con " + res + " elementos para el tag: " + compareObj.tag);


                foreach(Vector2 v2 in HliToDestroy)
                {
                    Debug.Log("las coordenadas de los elementos para elminar horizontalvec: " + v2);

                }

                foreach(Vector2 v2 in VliToDestroy)
                {
                    Debug.Log("las coordenadas de los elementos para elminar vericalvec: " + v2);
                }

                //evalResponse(res);
                if (res == 3)
                {

                }
                else if (res == 4)
                {

                }
                else if (res == 5)
                {

                }
                else if (res >= 6)
                {

                }

                //antes delimpiar los arreglos se deben eliminar los elementos corresepondientes de los maps y otras 
                //colecciones que se ayan utilizado.
                VliToDestroy.Clear();
                HliToDestroy.Clear();
            }
        }
    }

    private void Match3(String compareTag, int i, int j, int step)
    {
        Vector2 v2U;
        Vector2 v2D;
        Vector2 v2R;
        Vector2 v2L;
        int rU, rD, rR, rL, res;
        rU = rD = rR = rL = 0;
        res = 1;

        if (isGoingDown)
        {
            if(step == 1)
            {
                rD = MatchLineChk(compareTag, i, j, 'D');
                rU = MatchLineChk(compareTag, i, j + 1, 'U');
            }
            else
            {
                rD = MatchLineChk(compareTag, i, j - 1, 'D');
                rU = MatchLineChk(compareTag, i, j, 'U');
            }
            rR = MatchLineChk(compareTag, i, j, 'R');
            rL = MatchLineChk(compareTag, i, j, 'L');
        }
        else if (isGoingUp)
        {
            if (step == 1)
            {
                rD = MatchLineChk(compareTag, i, j - 1, 'D');
                rU = MatchLineChk(compareTag, i, j, 'U');
            }
            else
            {
                rD = MatchLineChk(compareTag, i, j, 'D');
                rU = MatchLineChk(compareTag, i, j + 1, 'U');
            }
            rR = MatchLineChk(compareTag, i, j, 'R');
            rL = MatchLineChk(compareTag, i, j, 'L');
        }
        else if (isGoingRight)
        {
            if (step == 1)
            {
                rR = MatchLineChk(compareTag, i, j, 'R');
                rL = MatchLineChk(compareTag, i-1, j, 'L');
            }
            else
            {
                rR = MatchLineChk(compareTag, i+1, j, 'R');
                rL = MatchLineChk(compareTag, i, j, 'L');
            }
            rU = MatchLineChk(compareTag, i, j, 'U');
            rD = MatchLineChk(compareTag, i, j, 'D');
        }
        else if (isGoingLeft)
        {
            if (step == 1)
            {
                rR = MatchLineChk(compareTag, i + 1, j, 'R');
                rL = MatchLineChk(compareTag, i, j, 'L');
            }
            else
            {
                rR = MatchLineChk(compareTag, i, j, 'R');
                rL = MatchLineChk(compareTag, i - 1, j, 'L');
            }
            rU = MatchLineChk(compareTag, i, j, 'U');
            rD = MatchLineChk(compareTag, i, j, 'D');
        }

        //if (!isGoingDown)
        //rU = MatchLineChk(compareTag, i, j, 'U');
        //if (!isGoingUp)
        ///rD = MatchLineChk(compareTag, i, j, 'D');
        //if (!isGoingLeft)
        //rR = MatchLineChk(compareTag, i, j, 'R');
        //if (!isGoingRight)
        //rL = MatchLineChk(compareTag, i, j, 'L');

        if (isGoingRight || isGoingLeft)
        {
            res += rU + rD;
            if (res >= 3)
            {
                v2U = new Vector2(i,j+rU);
                v2D = new Vector2(i,j-rD);
                VliToDestroy.Add(v2U);
                VliToDestroy.Add(v2D);
                if (isGoingRight)
                {
                    if (res >= 5 || rR >= 2)
                    {
                        v2R = new Vector2(i+rR, j);
                        HliToDestroy.Add(new Vector2(i,j));
                        HliToDestroy.Add(v2R);
                    }
                }
                else if (isGoingLeft)
                {
                    if (res >= 5 || rL >= 2)
                    {
                        v2L = new Vector2(i - rL, j);
                        HliToDestroy.Add(new Vector2(i, j));
                        HliToDestroy.Add(v2L);
                    }
                }
            }
            else
            {
                res = 1;
                if (rR >= 2)
                {
                    v2R = new Vector2(i + rR, j);
                    HliToDestroy.Add(new Vector2(i, j));
                    HliToDestroy.Add(v2R);
                }
                else if (rL >= 2)
                {
                    v2L = new Vector2(i - rL, j);
                    HliToDestroy.Add(new Vector2(i, j));
                    HliToDestroy.Add(v2L);
                }
            }
        }
        else if (isGoingUp || isGoingDown)
        {
            res += rR + rL;
            if (res >= 3)
            {
                v2R = new Vector2(i + rR, j);
                v2L = new Vector2(i - rL, j);
                HliToDestroy.Add(v2R);
                HliToDestroy.Add(v2L);
                if (isGoingUp)
                {
                    if (res >= 5 || rU >= 2)
                    {
                        v2U = new Vector2(i, j + rU);
                        VliToDestroy.Add(new Vector2(i, j));
                        VliToDestroy.Add(v2U);
                    }

                }
                else if (isGoingDown)
                {
                    if (res >= 5 || rD >= 2)
                    {
                        v2D = new Vector2(i, j - rD);
                        VliToDestroy.Add(new Vector2(i, j));
                        VliToDestroy.Add(v2D);
                    }
                }
            }
            else
            {
                if (rU >= 2)
                {
                    v2U = new Vector2(i, j + rU);
                    VliToDestroy.Add(new Vector2(i, j));
                    VliToDestroy.Add(v2U);
                }
                else if (rD >= 2)
                {
                    v2D = new Vector2(i, j - rD);
                    VliToDestroy.Add(new Vector2(i, j));
                    VliToDestroy.Add(v2D);
                }
            }
        }
    }

    private int MatchLineChk(String compareTag, int i, int j, char direction)
    {
        direction = Char.ToUpper(direction);
        switch (direction)
        {
            case 'U':
                if (HasNext(compareTag, i, j + 1))
                {
                    return 1 + MatchLineChk(compareTag, i, j + 1, 'U');
                }
                break;
            case 'D':
                if (HasNext(compareTag, i, j - 1))
                {
                    return 1 + MatchLineChk(compareTag, i, j - 1, 'D');
                }
                break;
            case 'R':
                if (HasNext(compareTag, i + 1, j))
                {
                    return 1 + MatchLineChk(compareTag, i + 1, j, 'R');
                }
                break;
            case 'L':
                if (HasNext(compareTag, i - 1, j))
                {
                    return 1 + MatchLineChk(compareTag, i - 1, j, 'L');
                }
                break;
            default:
                break;
        }

        return 0;
    }

    private bool HasNext(String compareTag, int i, int j)
    {
        if (i >= leftBound && i < width && j >= downBound && j < lowerHeight)
        {
            int idObj = GameController.instance.MapPositions[i + "" + j].Id;
            if (compareTag.Equals(GameController.instance.MapMosaic[idObj].ActionMosaic.tag))
            {
                return true;
            }
        }

        return false;
    }

    private void SelectInstanceOfElement(GameObject gameObject)
    {
        instanceElement = GameController.instance.MapMosaic[gameObject.GetInstanceID()].ActionMosaic;
        //instanceElement.GetComponent<BoxCollider2D>().isTrigger = false;
        deltaX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        deltaY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        elementInitPosition = instanceElement.transform.position;
        vecMinLimitDelta = new Vector3(minlimitDelta, minlimitDelta);
        Debug.Log("la posicion inicial del elemento: " + elementInitPosition + " - " + instanceElement.GetInstanceID()
            + "  " + instanceElement.GetComponent<BoxCollider2D>().isTrigger);
    }

    private void OnMouseDrag()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ((mousePosition.x <= elementInitPosition.x + 1
             && mousePosition.x >= elementInitPosition.x - 1)
             && (mousePosition.y >= elementInitPosition.y - 1
             && mousePosition.y <= elementInitPosition.y + 1)
             && isReleased)
        {
            if (mousePosition.x > elementInitPosition.x + vecMinLimitDelta.x && isGoingRight)
            {
                isGoingLeft = false;
                isGoingDown = false;
                isGoingUp = false;
                instanceElement.transform.position = new Vector3(mousePosition.x - deltaX, elementInitPosition.y, 0);
            }
            else if (mousePosition.y >= elementInitPosition.y + vecMinLimitDelta.y && isGoingUp)
            {
                isGoingLeft = false;
                isGoingDown = false;
                isGoingRight = false;
                instanceElement.transform.position = new Vector3(elementInitPosition.x, mousePosition.y - deltaY, 0);
            }
            else if (mousePosition.x < elementInitPosition.x - vecMinLimitDelta.x && isGoingLeft)
            {
                isGoingDown = false;
                isGoingRight = false;
                isGoingUp = false;
                instanceElement.transform.position = new Vector3(mousePosition.x - deltaX, elementInitPosition.y, 0);
            }
            else if (mousePosition.y < elementInitPosition.y - vecMinLimitDelta.y && isGoingDown)
            {
                isGoingRight = false;
                isGoingUp = false;
                isGoingLeft = false;
                instanceElement.transform.position = new Vector3(elementInitPosition.x, mousePosition.y - deltaY, 0);
            }

        }
        else
        {
            isGoingLeft = true;
            isGoingDown = true;
            isGoingRight = true;
            isGoingUp = true;
            if (!isMatch)
            {
                isReleased = false;

                BackElementToInitPosition();
                instanceElement = null;
            }
        }
    }

    private void OnMouseUp()
    {
        isGoingLeft = true;
        isGoingDown = true;
        isGoingRight = true;
        isGoingUp = true;
        isReleased = true;

        if (!isMatch)
        {
            BackElementToInitPosition();
        }
    }

    public void BackElementToInitPosition()
    {
        if (instanceElement != null)
        {
            instanceElement.gameObject.transform.position = elementInitPosition;
        }
    }

    private void SelectMovingRB(GameObject gameObject, int offset)
    {
        Vector3 v3 = gameObject.transform.localPosition;
        int x = (int)v3.x;
        int y = (int)v3.y + offset;
        ActionBlock ab = GameController.instance.MapPositions[x + "" + y];
        //print("la posicion del objetto: " + v3);
        GameController.instance.ElementInstance = ab.ActionMosaic;
    }

    private enum Fruits
    {
        Apple, Banana, Cherry, Orange, Strawberry
    }
}
