using Domain.Models;
using Domain.Services;

namespace Tests;

public class BarcodeServiceTests
{
    [Test]
    public void Should_Generate_Code_Using_Supplier_Code()
    {
        var service = new BarcodeService();

        var input = new BarcodeInput
        {
            SupplierCode = "ANFU",
            PartNumber = "96510BP100",
            SequenceCode = "DCU1",
            TraceabilityCode = "260128AD11ABRL35883037602096000021"
        };

        var result = service.Generate(input);

        Assert.That(result, Does.Contain("VANFU"));
    }

    [Test]
    public void Should_Generate_Code_Using_Sequence_Code()
    {
        var service = new BarcodeService();

        var input = new BarcodeInput
        {
            SupplierCode = "ANFU",
            PartNumber = "96510BP100",
            SequenceCode = "DCU1",
            TraceabilityCode = "260128AD11ABRL35883037602096000021"
        };

        var result = service.Generate(input);

        Assert.That(result, Does.Contain("SDCU1"));
    }

    [Test]
    public void Should_Generate_Code_Using_Part_Number()
    {
        var service = new BarcodeService();

        var input = new BarcodeInput
        {
            SupplierCode = "ANFU",
            PartNumber = "96510BP100",
            SequenceCode = "DCU1",
            TraceabilityCode = "260128AD11ABRL35883037602096000021"
        };

        var result = service.Generate(input);

        Assert.That(result, Does.Contain("P96510BP100"));
    }

    [Test]
    public void Should_Generate_Code_Using_Traceability_Code()
    {
        var service = new BarcodeService();

        var input = new BarcodeInput
        {
            SupplierCode = "ANFU",
            PartNumber = "96510BP100",
            SequenceCode = "DCU1",
            TraceabilityCode = "260128AD11ABRL35883037602096000021"
        };

        var result = service.Generate(input);

        Assert.That(result, Does.Contain("T260128AD11ABRL35883037602096000021"));
    }

    [Test]
    public void Should_Use_Group_Separator_GS()
    {
        var service = new BarcodeService();

        var input = new BarcodeInput
        {
            SupplierCode = "ANFU",
            PartNumber = "96510BP100",
            SequenceCode = "DCU1",
            TraceabilityCode = "260128AD11ABRL35883037602096000021"
        };

        var result = service.Generate(input);

        Assert.That(result, Does.Contain("\u001D"));
    }

    [Test]
    public void Should_Use_Default_Header()
    {
        var service = new BarcodeService();

        var input = new BarcodeInput
        {
            SupplierCode = "ANFU",
            PartNumber = "96510BP100",
            SequenceCode = "DCU1",
            TraceabilityCode = "260128AD11ABRL35883037602096000021"
        };

        var result = service.Generate(input);

        Assert.That(result, Does.StartWith("[)>\u001E06\u001D"));
    }

    [Test]
    public void Generate_ShouldThrowException_WhenSupplierCodeOrSequenceCodeContainsSpaces()
    {
        var service = new BarcodeService();

        var input = new BarcodeInput
        {
            SupplierCode = "AN FU",
            PartNumber = "96510BP100",
            SequenceCode = "DCU ",
            TraceabilityCode = "260128AD11ABRL35883037602096000021"
        };

        var ex = Assert.Throws<ArgumentException>(() => service.Generate(input));
        Assert.That(ex.Message, Is.EqualTo("Supplier code and sequence code cannot contain spaces."));
    }

    [Test]
    public void Should_Generate_Complete_Code()
    {
        var service = new BarcodeService();

        var input = new BarcodeInput
        {
            SupplierCode = "ANFU",
            PartNumber = "96510BP100",
            SequenceCode = "DCU1",
            TraceabilityCode = "260128AD11ABRL35883037602096000021"
        };

        var result = service.Generate(input);

        Assert.That(result,
            Is.EqualTo("[)>\u001E06\u001DVANFU\u001DP96510BP100\u001DSDCU1\u001DT260128AD11ABRL35883037602096000021\u001E\u0004"));
    }
}