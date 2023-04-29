using Microsoft.AspNetCore.Mvc;
using RdlcService.Services;
using System.Net.Mime;

namespace RdlcService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpGet("{reportName}/{reportType}")]
        public IActionResult Get(string reportName, string reportType)
        {
            var reportFileByteString = reportService.GenerateReportService(reportName, reportType);

            return File(reportFileByteString, MediaTypeNames.Application.Octet, getReportName(reportName, reportType));
        }

        private string getReportName(string reportName, string reportType)
        {
            return reportType.ToUpper() switch
            {
                "PDF" => $"{reportName}.pdf",
                "WORD" => $"{reportName}.doc",
                "EXEL" => $"{reportName}.xls",
                _ => $"{reportName}.pdf",
            };
        }
    }
}
