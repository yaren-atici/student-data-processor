# Student Data Processor

A C# console application that reads student grade data from multiple file formats, processes it, and exports the results.

## What it does

- Reads student scores from a **Word (.docx)** file
- Reads subject information from a **JSON** file
- Joins and sorts the data by student name
- Calculates average grades per subject
- Exports results to **Excel (.xlsx)** and **JSON**

## Tech Stack

- C# / .NET 10
- [ClosedXML](https://github.com/ClosedXML/ClosedXML) — Excel file generation
- [OfficeIMO](https://github.com/EvotecIT/OfficeIMO) — Word file reading
- [NCalc](https://github.com/ncalc/ncalc) — Expression evaluation

## How to run

1. Clone the repository
```bash
   git clone https://github.com/yaren-atici/student-data-processor.git
```
2. Open `MyProject.sln` in Visual Studio or Rider
3. Place your input files in the `Assets/` folder:
   - `data1.docx` — student scores
   - `data2.json` — subject info
4. Run the project

## Output

- `Assets/result_xls.xlsx` — combined data + subject summary
- `Assets/result_json.json` — combined records in JSON format

## Project Structure

| File | Description |
|------|-------------|
| `Program.cs` | Entry point, orchestrates the pipeline |
| `WordReader.cs` | Reads student data from Word |
| `JsonReaderService.cs` | Reads subject data from JSON |
| `DataProcessor.cs` | Joins and aggregates data |
| `ExcelWriter.cs` | Writes results to Excel |
| `JsonWriterService.cs` | Writes results to JSON |
