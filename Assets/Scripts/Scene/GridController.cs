using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scene
{
    public class GridController : MonoBehaviour
    {

        public GridLayout gridLayout;
        public Tilemap tilemap;

        private static Dictionary<TileType, TileBase> tileBases = new();
        private Building temp;
        private Vector3 prevPosition;

        public void InitializeWithBuilding(GameObject building)
        {
            temp = Instantiate(building, new Vector3(0, 0, 35.3f), Quaternion.identity).GetComponent<Building>();
            // buttonToHide.SetActive(false);
            print(gridLayout.LocalToCell(Vector3.zero) + " : local");
            print(gridLayout.WorldToCell(Vector3.zero) + " : world");
        }

        public void HideButton(GameObject button)
        {
            button.SetActive(false);
        }
    

        // Start is called before the first frame update
        void Start()
        {
            var tilePath = @"Tiles/";
            tileBases.Add(TileType.Empty, null);
            tileBases.Add(TileType.White, Resources.Load<TileBase>(path: tilePath + "white"));
            tileBases.Add(TileType.Green, Resources.Load<TileBase>(path: tilePath + "green"));
            tileBases.Add(TileType.Red, Resources.Load<TileBase>(path: tilePath + "white"));
        }

        // Update is called once per frame
        void Update()
        {

        }

        private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
        {
            TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
            int counter = 0;
            foreach (var v in area.allPositionsWithin)
            {
                Vector3Int pos = new Vector3Int(v.x, v.y, 0);
                array[counter] = tilemap.GetTile(pos);
                counter++;
            }

            return array;
        }

        private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
        {
            int size = area.size.x * area.size.y * area.size.z;
            TileBase[] tileArray = new TileBase [size];
            FillTiles(tileArray, type);
            tilemap.SetTilesBlock(area, tileArray);
        }

        private static void FillTiles(TileBase[] arr, TileType type)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                arr[i] = tileBases[type];
            }
        }
    }

    public enum TileType
    {
        Empty,
        White,
        Green,
        Red
    }
}