using AspNetCore.Reporting;
using RdlcService.Model;
using System.Reflection;
using System.Text;

namespace RdlcService.Services;
public class ReportService : IReportService
{
    public byte[] GenerateReportService(string reportName, string reportType)
    {
        var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location); ;
        //string path = Assembly.GetExecutingAssembly().Location.Replace("RdlcService.dll", "");
        string rdlcFilePath = string.Format("{0}\\ReportFiles\\{1}.rdlc", path, reportName);
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        Encoding.GetEncoding("utf-8");

        LocalReport report = new(rdlcFilePath);

        var users = new List<UserModel>
        {
            new UserModel {
            FirstName  = "Mehrdad",
            LastName = "tabesh",
            Email = "mehrdadit1@gmail.com",
            Phone = "dfdf"
            },
            new UserModel {
            FirstName  = "reza",
            LastName = "tabesh",
            Email = "mehrdadit1@gmail.com",
            Phone = "dfdf"
            },
            new UserModel {
            FirstName  = "yasin ",
            LastName = "tabesh",
            Email = "mehrdadit1@gmail.com",
            Phone = "09166"
            },
            new UserModel {
            FirstName = "ali",
            LastName = "tabesh",
            Email = "mehrdadit1@gmail.com",
            Phone = "sff"
            },

        };
        var buyers = new List<Buyer>{
            new Buyer
            {
                FullName = "مهرداد تابش",
                Address = "ایران"
            },
         };


        report.AddDataSource("dsUsers", users);
        report.AddDataSource("buyers", buyers);

        Dictionary<string, string> parameters = new();

        parameters.Add("CompanyName", "کاربران بزرگ شرکت داده ");

        var result = report.Execute(GetRenderType(reportType), 1, parameters);

        return result.MainStream;
    }

    private RenderType GetRenderType(string reportType)
    {
        RenderType renderType = reportType.ToUpper() switch
        {
            "PDF" => RenderType.Pdf,
            "WORD" => RenderType.Word,
            "EXEL" => RenderType.Excel,
            _ => RenderType.Pdf,
        };
        return renderType;
    }
}
