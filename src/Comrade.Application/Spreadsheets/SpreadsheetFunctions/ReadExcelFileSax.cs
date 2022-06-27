using Comrade.Application.Extensions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;

namespace Comrade.Application.Spreadsheets.SpreadsheetFunctions;

public static class ReadExcelFileSax
{
    public static List<Dictionary<string, string>> Execute(IFormFile fileImport)
    {
        using var streamFile = fileImport.OpenReadStream();
        using var spreadsheetDocument = SpreadsheetDocument.Open(streamFile, false);
        var workbookPart = spreadsheetDocument.WorkbookPart;

        var lineData = new List<Dictionary<string, string>>();

        if (workbookPart != null)
            foreach (var worksheetPart in workbookPart.WorksheetParts)
            {
                var lineInfo = new Dictionary<string, string>();
                var reader = OpenXmlReader.Create(worksheetPart);
                while (reader.Read())
                {
                    if (reader.ElementType == typeof(Row))
                    {
                        reader.ReadFirstChild();

                        do
                        {
                            if (reader.ElementType == typeof(Cell))
                            {
                                var cell = (Cell) reader.LoadCurrentElement()!;

                                string? cellValue;

                                if (cell.DataType != null &&
                                    cell.DataType == CellValues.SharedString)
                                {
                                    var cellPosition = cell.CellValue!.InnerText.ToInt32();

                                    var ssi = workbookPart.SharedStringTablePart
                                        ?.SharedStringTable
                                        .Elements<SharedStringItem>()
                                        .ElementAt(cellPosition);

                                    cellValue = ssi?.Text?.Text;
                                }
                                else
                                {
                                    cellValue = cell?.CellValue?.InnerText;
                                }

                                var infoCell = cell?.CellReference;

                                var column = Regex.Replace(infoCell!, @"[\d-]", string.Empty);
                                if (cellValue != null)
                                {
                                    lineInfo.Add(column, cellValue);
                                }
                            }
                        } while (reader.ReadNextSibling());
                    }

                    if (lineInfo.Count > 0)
                    {
                        var content = lineInfo.Select(x => x.Value != null).Any();

                        if (content)
                        {
                            lineData.Add(lineInfo);
                            lineInfo = new Dictionary<string, string>();
                        }
                    }
                }

                reader.Dispose();
            }

        streamFile.Close();
        return lineData;
    }
}