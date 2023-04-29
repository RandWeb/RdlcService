namespace RdlcService.Services;

public interface IReportService
{
    byte[] GenerateReportService(string reportName, string reportFile);
}
