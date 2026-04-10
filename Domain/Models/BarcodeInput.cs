using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class BarcodeInput
{
    [Required(ErrorMessage = "Supplier code is required.")]
    public required string SupplierCode { get; set; }

    [Required(ErrorMessage = "Part number is required.")]
    public required string PartNumber { get; set; }
    
    [Required(ErrorMessage = "Sequence code is required.")]
    public required string SequenceCode { get; set; }
    
    [Required(ErrorMessage = "Traceability code is required.")]
    public required string TraceabilityCode { get; set; }
}
