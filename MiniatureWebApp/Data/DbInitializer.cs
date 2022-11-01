using ClosedXML.Excel;
using MiniatureWebApp.Models;
using System.Collections;
using System.Data;
using System.Diagnostics;

namespace MiniatureWebApp.Data
{
    public class DbInitializer
    {
        public static DataTable ImportSheet(string fileName)
        {
            var datatable = new DataTable();
            var workbook = new XLWorkbook(fileName);
            var xlWorksheet = workbook.Worksheet(1);
            var range = xlWorksheet.Range(xlWorksheet.FirstCellUsed(), xlWorksheet.LastCellUsed());

            var col = range.ColumnCount();
            var row = range.RowCount();

            //if a datatable already exists, clear the existing table 
            datatable.Clear();
            for (var i = 1; i <= col; i++)
            {
                var column = xlWorksheet.Cell(1, i);
                datatable.Columns.Add(column.Value.ToString());
            }

            var firstHeadRow = 0;
            foreach (var item in range.Rows())
            {
                if (firstHeadRow != 0)
                {
                    var array = new object[col];
                    for (var y = 1; y <= col; y++)
                    {
                        array[y - 1] = item.Cell(y).Value;
                    }

                    datatable.Rows.Add(array);
                }
                firstHeadRow++;
            }
            return datatable;
        }

        public static void Initialize(MiniatureWebAppContext context)
        {
            //If the PowerStations database is empty                        
            if (context.PowerStations.Any())
            {
                //There are alreay power stations in the database
                Debug.WriteLine("Hello World!");

            }
            else
            {
                DataTable _dt = new DataTable();
                _dt = ImportSheet("PowerStations.xlsx");

                var powerStations = new List<PowerStation>();
                foreach (DataRow dtRow in _dt.Rows)
                {
                    var Name = dtRow[_dt.Columns["Name"]].ToString();
                    float Latitude = float.Parse(dtRow[_dt.Columns["Latitude"]].ToString());
                    float Longitude = float.Parse(dtRow[_dt.Columns["Longitude"]].ToString());
                    var Address = dtRow[_dt.Columns["Address"]].ToString();
                    var PhoneNumber = dtRow[_dt.Columns["PhoneNumber"]].ToString();

                    powerStations.Add(new PowerStation { Name = Name, Latitude = Latitude, Longitude = Longitude, Address = Address, PhoneNumber = PhoneNumber });
                }
                context.PowerStations.AddRange(powerStations);
                context.SaveChanges();
            }


            //If the Inspections database is empty                        
            if (context.Inspections.Any())
            {
                //There are alreay inspections in the database
                Debug.WriteLine("Hello World!");
            }
            else
            {
                DataTable _dt = new DataTable();
                _dt = ImportSheet("Inspections.xlsx");

                var inspections = new List<Inspection>();
                foreach (DataRow dtRow in _dt.Rows)
                {
                    DateTime myDate = DateTime.Parse(dtRow[_dt.Columns["Date"]].ToString());
                    int powerStationId = int.Parse(dtRow[_dt.Columns["PowerStationId"]].ToString());
                    string inspectorName = dtRow[_dt.Columns["InspectorName"]].ToString();
                    string comment = dtRow[_dt.Columns["Comment"]].ToString();
                    var status = (Status)Enum.Parse(typeof(Status), dtRow[_dt.Columns["Status"]].ToString());

                    inspections.Add(new Inspection { PowerStationId = powerStationId, Date= myDate, InspectorName = inspectorName, Comment = comment, Status = status});
                }
                context.Inspections.AddRange(inspections);
                context.SaveChanges();
            }


        }
    }
}
