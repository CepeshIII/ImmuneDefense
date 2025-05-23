using UnityEngine;

public static class CellsLoader
{
    static string folderName = "Cells";

    public static CellsData[] LoadCellsData()
    {
        var cellsData = Resources.LoadAll<CellsData>(folderName);
        return cellsData;
    }
}
