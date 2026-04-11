using System;
using Domain.Models;

namespace Domain.Services;

public class BarcodeService
{
    private const string Header = "[)>\u001E06\u001D"; // Standard header for GS1-128 barcodes
    private const string GS = "\u001D"; // Group Separator
    private const string RS = "\u001E"; // Record Separator
    private const string EOT = "\u0004"; // End of Transmission

    public string Generate(BarcodeInput input)
    {
        var parts = new List<string>();

        if (string.IsNullOrWhiteSpace(input.SupplierCode) ||
            string.IsNullOrEmpty(input.PartNumber) ||
            string.IsNullOrWhiteSpace(input.SequenceCode) ||
            string.IsNullOrWhiteSpace(input.TraceabilityCode))
        {
            throw new ArgumentException("One or more required fields are null or whitespace.");   
        }
        else if (input.SupplierCode.Contains(' ') 
            || input.SequenceCode.Contains(' '))
        {
            throw new ArgumentException("Supplier code and sequence code cannot contain spaces.");  
        }
        else
        {
            parts.Add($"V{input.SupplierCode}");
            parts.Add($"P{input.PartNumber}");
            parts.Add($"S{input.SequenceCode}");
            parts.Add($"T{input.TraceabilityCode}");

        }

        return Header + string.Join(GS, parts) + RS + EOT;
    }
}
