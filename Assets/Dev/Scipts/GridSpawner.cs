using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    #region Variables for Grid
    public int column, row;
    public GameObject gridPrefab;
    [SerializeField] private float gridYValue;
    public Material lightGridMat, darkGridMat;
    #endregion

    private void Awake()
    {
        ObjectManager.GridSpawner = this;
    }

    void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                GameObject newGrid = Instantiate(gridPrefab);
                newGrid.transform.SetParent(transform);
               //Set Pos
                newGrid.transform.localPosition = new Vector3(j * newGrid.transform.localScale.x * 10, gridYValue, -i * newGrid.transform.localScale.z * 10);
               
                //Set Color
                if ((i + j) % 2 == 0)
                    newGrid.GetComponent<MeshRenderer>().sharedMaterial = lightGridMat;
                else
                    newGrid.GetComponent<MeshRenderer>().sharedMaterial = darkGridMat;

            }
        }
    }
}
