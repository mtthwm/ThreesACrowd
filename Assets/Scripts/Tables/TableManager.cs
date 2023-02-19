using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{

    [SerializeField] private List<Table> tables = new List<Table>();

    private void Start()
    {
        UpdatePositions();
    }

    public void Relocate (Customer c)
    {
        List<Table> otherTables = tables.FindAll((t) => t != c.CurrentTable);

        Table mostVacancies = null;
        foreach (Table t in otherTables)
        {
            if (mostVacancies == null)
            {
                if (t.Vacancies > 0)
                {
                    mostVacancies = t;
                }
                continue;
            }

            if (t.Vacancies > mostVacancies.Vacancies)
            {
                mostVacancies = t;
            }
        }

        if (mostVacancies != null)
        {
            c.CurrentTable.Remove(c);
            bool filledSpot = mostVacancies.FillVacancy(c);
        }

        RemovePairs();
    }

    private void RemovePairs ()
    {
        foreach (Table t in tables)
        {
            t.RemovePair();
        }
    }

    public void UpdatePositions ()
    {
        foreach (Table t in tables)
        {
            foreach (Customer c in t.occupants)
            {
                if (c != null)
                {
                    t.GetRealPosition(c);
                }
            }
        }
    }

    public override string ToString()
    {
        string buildString = "";

        foreach (Table t in tables)
        {
            buildString += " |";
            buildString += t.ToString();
            buildString += "| ";
        }

        return buildString;
    }
}
