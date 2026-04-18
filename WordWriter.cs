using OfficeIMO.Word;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProject
{
    public class WordWriter
    {
        readonly string _path;

        public WordWriter(string path)
        {
            _path = path;
        }

        public void WriteTable(List<string> equations, List<object>results, double xValue) 
        {

            using var document = WordDocument.Create(_path);

            var table = document.AddTable(equations.Count + 1, 3);

            //headers
            table.Rows[0].Cells[0].Paragraphs[0].AddText("Equation");
            table.Rows[0].Cells[1].Paragraphs[0].AddText("x Value");
            table.Rows[0].Cells[2].Paragraphs[0].AddText("Result");

            //body
            for(int i=0;i<equations.Count; i++)
            {
                table.Rows[i + 1].Cells[0].Paragraphs[0].AddText(equations[i]);
                table.Rows[i + 1].Cells[1].Paragraphs[0].AddText(xValue.ToString());
                table.Rows[i + 1].Cells[2].Paragraphs[0].AddText(results[i]?.ToString() ?? "Error");
            }
             document.Save();

        }

    }
}
