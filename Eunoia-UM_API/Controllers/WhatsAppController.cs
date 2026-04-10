using Eunoia_UM_API.Helper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Eunoia_UM_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WhatsAppController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public WhatsAppController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("DispatchReportWhatsApp")]
        public async Task<IActionResult> DispatchReportWhatsApp()
        {
            try
            {

                //login to web server first
                SqlParameter[] login = new SqlParameter[]
                {
                new SqlParameter("@EmailId", "jaisingh"),
                new SqlParameter("@Passwordstr", "QbKdahhKaBdYfTXxspcC0XhZDNmizFC/h2TbTXGHYlQ="),
                new SqlParameter("@BranchId", 1)
                };
                DBOperation.FillDataSet("[dbo].[USP_MASTER_UserLoginCheck_Select]", login);

                //login to web server first
                SqlParameter[] loginBranch = new SqlParameter[]
                {
                new SqlParameter("@roleId", 2),
                new SqlParameter("@deptId", 5),
                new SqlParameter("@branchId", 1),
                new SqlParameter("@locationId", 3406),
                new SqlParameter("@usrcode", "U000002")
                };
                DBOperation.FillDataSet("[dbo].[USP_ADMIN_PremissionDetails_View]", loginBranch);



                var ownCnt = DBOperation.FillDataSet("[dbo].[USP_OwnerNameAndContactForWhatsApp_Get]");


                string mobileNumber = "0000000000";
                string ownerName = "User";

                if (ownCnt.Tables.Count > 0 && ownCnt.Tables[0].Rows.Count > 0)
                {
                    mobileNumber = ownCnt.Tables[0].Rows[0][0]?.ToString() ?? "0000000000";
                    ownerName = ownCnt.Tables[0].Rows[0][1]?.ToString() ?? "User";
                }

                DateTime fromDate = DateTime.Today.AddDays(-1).AddHours(8); // Yesterday 8 AM
                DateTime toDate = DateTime.Today.AddHours(8);              // Today 8 AM

                SqlParameter[] pdfParams = new SqlParameter[]
                {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate),
                new SqlParameter("@BranchId", 1)
                };

                var dsPdf = DBOperation.FillDataSet("[dbo].[USP_Report_DispatchReportForWhatsApp_GroupedByCustomerProduct]", pdfParams);

                if (dsPdf == null || dsPdf.Tables.Count == 0)
                {
                    return new JsonResult(new
                    {
                        responseCode = -1,
                        message = "No data found to generate PDF.",
                        statusCode = 0
                    });
                }

                //string uploadsDir = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "Docs", "WhatsApp");
                //if (!Directory.Exists(uploadsDir))
                //    Directory.CreateDirectory(uploadsDir);


                string uploadsDir = @"C:\Eunoia\eunoiaweb\Docs\WhatsApp";

                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);


                string apiUrl = "https://backend.api-wa.co/campaign/jam-research-services/api/v2";
                var apiResponses = new List<object>();

                for (int i = 0; i < dsPdf.Tables.Count; i++)
                {
                    DataTable table = dsPdf.Tables[i];
                    if (table.Rows.Count == 0) continue;

                    string customerName = $"{(table.Rows[0]["Customer"]?.ToString()?.Trim() ?? "Customer")} - {(table.Rows[0]["PoTypeName"]?.ToString()?.Trim() ?? "PoTypeName")}";
                    string routeName = table.Rows[0]["sRouteName"]?.ToString()?.Trim() ?? "Route";
                    string Material = table.Rows[0]["Product"]?.ToString()?.Trim() ?? "Product";
                    string companyName = table.Rows[0]["CompanyName"]?.ToString() ?? "";

                    decimal totalLoadingQty = 0;

                    foreach (DataRow r in table.Rows)
                    {
                        if (r["LoadingQty"] == DBNull.Value || string.IsNullOrWhiteSpace(r["LoadingQty"].ToString()))
                        {
                            break; // exit loop on first blank/null
                        }

                        totalLoadingQty += Convert.ToDecimal(r["LoadingQty"]);
                    }



                    totalLoadingQty = Math.Round(totalLoadingQty, 2);


                    var singleTableDs = new DataSet();
                    singleTableDs.Tables.Add(table.Copy());

                    byte[] pdfBytes = GeneratePdfFromDataSet(singleTableDs, companyName);

                    string guid = Guid.NewGuid().ToString();
                    string fileName = $"{guid}_Dispatch_{i + 1}.pdf";
                    string filePath = Path.Combine(uploadsDir, fileName);
                    await System.IO.File.WriteAllBytesAsync(filePath, pdfBytes);


                    var UrlForAccess = DBOperation.FillDataSet("[dbo].[USP_UrlForAccessForWhatsApp_Get]");

                    string UrlName = "http://122.163.13.47/eunoiaweb/Docs/WhatsApp";

                    if (UrlForAccess.Tables.Count > 0 && UrlForAccess.Tables[0].Rows.Count > 0)
                    {
                        UrlName = UrlForAccess.Tables[0].Rows[0][0]?.ToString() ?? "http://122.163.13.47/eunoiaweb/Docs/WhatsApp";
                    }
                    totalLoadingQty = (totalLoadingQty / 2);
                    string publicUrl = $"{UrlName}/{fileName}";
                    var whatsappPayload = new
                    {
                        apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY4NTUwOGUxM2RhYTE0NDJjY2U5ZThhZCIsIm5hbWUiOiJKQUhOQVZJIE1JTkVSQUxTIiwiYXBwTmFtZSI6IkFpU2Vuc3kiLCJjbGllbnRJZCI6IjY4NTUwOGUwM2RhYTE0NDJjY2U5ZThhNiIsImFjdGl2ZVBsYW4iOiJOT05FIiwiaWF0IjoxNzUwNDAzMjk3fQ.ZCsEqQUn-QjQ9e2WEX2DzI4vE8bdjJnGe13dpQbrxwA",
                        campaignName = "SendDispatchReportWithDoc_CompanyName",
                        destination = "91" + mobileNumber,
                        userName = "JAHNAVI MINERALS",
                        templateParams = new[] { ownerName, Material, customerName, totalLoadingQty.ToString("N2"), companyName },
                        source = "new-landing-page form",
                        media = new { url = "https://d3jt6ku4g6z5l8.cloudfront.net/FILE/6353da2e153a147b991dd812/4079142_dummy.pdf", filename = "DispatchReport" },
                        buttons = new object[] { },
                        carouselCards = new object[] { },
                        location = new { },
                        attributes = new { },
                        paramsFallbackValue = new { FirstName = "User" }
                    };

                    using var client = new HttpClient();
                    string json = System.Text.Json.JsonSerializer.Serialize(whatsappPayload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    apiResponses.Add(new
                    {
                        FileName = fileName,
                        Url = publicUrl,
                        WhatsAppResponse = apiResponse,
                        Success = response.IsSuccessStatusCode
                    });

                }

                return new JsonResult(new
                {
                    responseCode = 0,
                    message = "WhatsApp messages sent successfully.",
                    statusCode = 1,
                    data = apiResponses
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    responseCode = -1,
                    message = "An error occurred: " + ex.Message,
                    statusCode = 0
                });
            }
        }


        private byte[] GeneratePdfFromDataSet(DataSet ds, string companyName)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4.Rotate(), 20, 20, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Title
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.DARK_GRAY);
                Paragraph title = new Paragraph("Vehicle Dispatch Summary", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                document.Add(title);

                // Date & Time
                Font dateFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
                //Paragraph date = new Paragraph($"Generated on: {DateTime.Now:dd-MMM-yyyy HH:mm}", dateFont)
                //{
                //    Alignment = Element.ALIGN_LEFT,
                //    SpacingAfter = 10f
                //};
                //document.Add(date);

                Paragraph date = new Paragraph(
                $"Generated on: {DateTime.Now:dd-MMM-yyyy HH:mm}\nCompany Name: {companyName}",dateFont)
                {
                    Alignment = Element.ALIGN_LEFT,
                    SpacingAfter = 10f
                };
                document.Add(date);

                DataTable dataTable = ds.Tables[0];

                // Define display columns and widths
                var desiredColumns = new List<(string columnName, string displayName, float width)>
        {
            ("PlacementDate", "Placement Date", 10f),
            ("VehicleType", "Vehicle Type", 10f),
            ("VehicleNo", "Vehicle No", 10f),
            ("Supplier", "Supplier", 12f),
            ("Customer", "Customer", 12f),
            ("CompanyName", "Company Name", 12f),
            ("CNNO", "CN No", 10f),
            ("CNDate", "CN Date", 10f),
            ("sRouteName", "Route Name", 10f),
            ("Product", "Product", 10f),
            ("LoadingQty", "Loading Qty", 8f),
            ("UnloadingQty", "Unloading Qty", 8f),
            ("IsReceipt", "Receipt?", 5f),
            ("IsFMReceipt", "FM Receipt?", 5f)
        };

                int numberOfColumns = desiredColumns.Count;
                PdfPTable pdfTable = new PdfPTable(numberOfColumns + 1)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 10f,
                    SpacingAfter = 10f
                };

                float[] columnWidths = new float[numberOfColumns + 1];
                columnWidths[0] = 5f; // Sr.No
                for (int i = 0; i < desiredColumns.Count; i++)
                    columnWidths[i + 1] = desiredColumns[i].width;

                float totalWidth = columnWidths.Sum();
                for (int i = 0; i < columnWidths.Length; i++)
                    columnWidths[i] = (columnWidths[i] / totalWidth) * 100f;

                pdfTable.SetWidths(columnWidths);

                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 7, BaseColor.WHITE);
                Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 6, BaseColor.BLACK);

                // Add header cells
                PdfPCell srNoHeader = new PdfPCell(new Phrase("Sr.No", headerFont))
                {
                    BackgroundColor = new BaseColor(51, 102, 153),
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 3f
                };
                pdfTable.AddCell(srNoHeader);

                foreach (var col in desiredColumns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(col.displayName, headerFont))
                    {
                        BackgroundColor = new BaseColor(51, 102, 153),
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 3f
                    };
                    pdfTable.AddCell(cell);
                }

                int detailRowNum = 1;        // Serial for detail rows
                int routeSummaryRowNum = 1;  // Serial for route summary rows

                foreach (DataRow row in dataTable.Rows)
                {
                    string placementDate = row["PlacementDate"]?.ToString()?.Trim() ?? "";
                    string product = row["Product"]?.ToString()?.Trim() ?? "";
                    string srNo = "";

                    // TOTAL / Heading / Blank rows
                    if (placementDate == "TOTAL" || placementDate == "Route Name" || string.IsNullOrWhiteSpace(placementDate))
                    {
                        srNo = "";
                    }
                    // Route summary row: product is empty but sRouteName has value
                    else if (string.IsNullOrEmpty(product) && string.IsNullOrEmpty(row["sRouteName"]?.ToString()))
                    {
                        srNo = routeSummaryRowNum.ToString();
                        routeSummaryRowNum++;
                    }
                    // Detail row
                    else
                    {
                        srNo = detailRowNum.ToString();
                        detailRowNum++;
                    }

                    // Decide if current row is Route Summary heading (for color)
                    bool isRouteSummary = placementDate == "Route Name";

                    // Add Sr.No cell
                    pdfTable.AddCell(new PdfPCell(new Phrase(srNo, isRouteSummary ? headerFont : dataFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 3f,
                        BackgroundColor = isRouteSummary ? new BaseColor(51, 102, 153) : BaseColor.WHITE
                    });

                    // Add other columns
                    foreach (var col in desiredColumns)
                    {
                        string value = row.Table.Columns.Contains(col.columnName)
                            ? (row[col.columnName] == DBNull.Value ? "" : row[col.columnName].ToString())
                            : "";

                        PdfPCell dataCell = new PdfPCell(new Phrase(value, isRouteSummary ? headerFont : dataFont))
                        {
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            Padding = 3f,
                            BackgroundColor = isRouteSummary ? new BaseColor(51, 102, 153) : BaseColor.WHITE
                        };
                        pdfTable.AddCell(dataCell);
                    }
                }

                document.Add(pdfTable);
                document.Close();
                return memoryStream.ToArray();
            }
        }



        [HttpGet("DispatchReportWhatsApp_JRPL")]
        public async Task<IActionResult> DispatchReportWhatsApp_JRPL()
        {
            try
            {
                int branchId = 1009;

                //login to web server first
                SqlParameter[] login = new SqlParameter[]
                {
                new SqlParameter("@EmailId", "jaisingh"),
                new SqlParameter("@Passwordstr", "QbKdahhKaBdYfTXxspcC0XhZDNmizFC/h2TbTXGHYlQ="),
                new SqlParameter("@BranchId", branchId)
                };
                DBOperation.FillDataSet("[dbo].[USP_MASTER_UserLoginCheck_Select]", login);

                //login to web server first
                SqlParameter[] loginBranch = new SqlParameter[]
                {
                new SqlParameter("@roleId", 2),
                new SqlParameter("@deptId", 5),
                new SqlParameter("@branchId", branchId),
                new SqlParameter("@locationId", 3406),
                new SqlParameter("@usrcode", "U000002")
                };
                DBOperation.FillDataSet("[dbo].[USP_ADMIN_PremissionDetails_View]", loginBranch);



                var ownCnt = DBOperation.FillDataSet("[dbo].[USP_OwnerNameAndContactForWhatsApp_Get]");


                string mobileNumber = "0000000000";
                string ownerName = "User";

                if (ownCnt.Tables.Count > 0 && ownCnt.Tables[0].Rows.Count > 0)
                {
                    mobileNumber = ownCnt.Tables[0].Rows[0][0]?.ToString() ?? "0000000000";
                    ownerName = ownCnt.Tables[0].Rows[0][1]?.ToString() ?? "User";
                }

                DateTime fromDate = DateTime.Today.AddDays(-1).AddHours(8); // Yesterday 8 AM
                DateTime toDate = DateTime.Today.AddHours(8);              // Today 8 AM

                SqlParameter[] pdfParams = new SqlParameter[]
                {
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate),
                new SqlParameter("@BranchId", branchId)
                };

                var dsPdf = DBOperation.FillDataSet("[dbo].[USP_Report_DispatchReportForWhatsApp_GroupedByCustomerProduct]", pdfParams);

                if (dsPdf == null || dsPdf.Tables.Count == 0)
                {
                    return new JsonResult(new
                    {
                        responseCode = -1,
                        message = "No data found to generate PDF.",
                        statusCode = 0
                    });
                }

                //string uploadsDir = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "Docs", "WhatsApp");
                //if (!Directory.Exists(uploadsDir))
                //    Directory.CreateDirectory(uploadsDir);


                string uploadsDir = @"C:\Eunoia\eunoiaweb\Docs\WhatsApp";

                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);


                string apiUrl = "https://backend.api-wa.co/campaign/jam-research-services/api/v2";
                var apiResponses = new List<object>();

                for (int i = 0; i < dsPdf.Tables.Count; i++)
                {
                    DataTable table = dsPdf.Tables[i];
                    if (table.Rows.Count == 0) continue;

                    string customerName = $"{(table.Rows[0]["Customer"]?.ToString()?.Trim() ?? "Customer")} - {(table.Rows[0]["PoTypeName"]?.ToString()?.Trim() ?? "PoTypeName")}";
                    string routeName = table.Rows[0]["sRouteName"]?.ToString()?.Trim() ?? "Route";
                    string Material = table.Rows[0]["Product"]?.ToString()?.Trim() ?? "Product";
                    string companyName = table.Rows[0]["CompanyName"]?.ToString() ?? "";

                    decimal totalLoadingQty = 0;

                    foreach (DataRow r in table.Rows)
                    {
                        if (r["LoadingQty"] == DBNull.Value || string.IsNullOrWhiteSpace(r["LoadingQty"].ToString()))
                        {
                            break; // exit loop on first blank/null
                        }

                        totalLoadingQty += Convert.ToDecimal(r["LoadingQty"]);
                    }



                    totalLoadingQty = Math.Round(totalLoadingQty, 2);


                    var singleTableDs = new DataSet();
                    singleTableDs.Tables.Add(table.Copy());

                    byte[] pdfBytes = GeneratePdfFromDataSet(singleTableDs, companyName);

                    string guid = Guid.NewGuid().ToString();
                    string fileName = $"{guid}_Dispatch_{i + 1}.pdf";
                    string filePath = Path.Combine(uploadsDir, fileName);
                    await System.IO.File.WriteAllBytesAsync(filePath, pdfBytes);


                    var UrlForAccess = DBOperation.FillDataSet("[dbo].[USP_UrlForAccessForWhatsApp_Get]");

                    string UrlName = "http://122.163.13.47/eunoiaweb/Docs/WhatsApp";

                    if (UrlForAccess.Tables.Count > 0 && UrlForAccess.Tables[0].Rows.Count > 0)
                    {
                        UrlName = UrlForAccess.Tables[0].Rows[0][0]?.ToString() ?? "http://122.163.13.47/eunoiaweb/Docs/WhatsApp";
                    }
                    totalLoadingQty = (totalLoadingQty / 2);
                    string publicUrl = $"{UrlName}/{fileName}";
                    var whatsappPayload = new
                    {
                        apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY4NTUwOGUxM2RhYTE0NDJjY2U5ZThhZCIsIm5hbWUiOiJKQUhOQVZJIE1JTkVSQUxTIiwiYXBwTmFtZSI6IkFpU2Vuc3kiLCJjbGllbnRJZCI6IjY4NTUwOGUwM2RhYTE0NDJjY2U5ZThhNiIsImFjdGl2ZVBsYW4iOiJOT05FIiwiaWF0IjoxNzUwNDAzMjk3fQ.ZCsEqQUn-QjQ9e2WEX2DzI4vE8bdjJnGe13dpQbrxwA",
                        campaignName = "SendDispatchReportWithDoc_CompanyName",
                        destination = "91" + mobileNumber,
                        userName = "JAHNAVI MINERALS",
                        templateParams = new[] { ownerName, Material, customerName, totalLoadingQty.ToString("N2"), companyName },
                        source = "new-landing-page form",
                        media = new { url = publicUrl, filename = "DispatchReport" },
                        buttons = new object[] { },
                        carouselCards = new object[] { },
                        location = new { },
                        attributes = new { },
                        paramsFallbackValue = new { FirstName = "User" }
                    };

                    using var client = new HttpClient();
                    string json = System.Text.Json.JsonSerializer.Serialize(whatsappPayload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    apiResponses.Add(new
                    {
                        FileName = fileName,
                        Url = publicUrl,
                        WhatsAppResponse = apiResponse,
                        Success = response.IsSuccessStatusCode
                    });

                }

                return new JsonResult(new
                {
                    responseCode = 0,
                    message = "WhatsApp messages sent successfully.",
                    statusCode = 1,
                    data = apiResponses
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    responseCode = -1,
                    message = "An error occurred: " + ex.Message,
                    statusCode = 0
                });
            }
        }



        [HttpGet("LRReceiptAlert")]
        public async Task<IActionResult> LRReceiptAlert()
        {
            try
            {
                int branchId = 1;
                int yearId = 1;

                // 1. Login to web server first
                SqlParameter[] login = new SqlParameter[]
                {
                    new SqlParameter("@EmailId", "jaisingh"),
                    new SqlParameter("@Passwordstr", "QbKdahhKaBdYfTXxspcC0XhZDNmizFC/h2TbTXGHYlQ="),
                    new SqlParameter("@BranchId", branchId)
                };
                DBOperation.FillDataSet("[dbo].[USP_MASTER_UserLoginCheck_Select]", login);

                SqlParameter[] loginBranch = new SqlParameter[]
                {
                    new SqlParameter("@roleId", 2),
                    new SqlParameter("@deptId", 5),
                    new SqlParameter("@branchId", branchId),
                    new SqlParameter("@locationId", 3406),
                    new SqlParameter("@usrcode", "U000002")
                };
                DBOperation.FillDataSet("[dbo].[USP_ADMIN_PremissionDetails_View]", loginBranch);

                // 2. Get Owner details for WhatsApp
                var ownCnt = DBOperation.FillDataSet("[dbo].[USP_OwnerNameAndContactForWhatsApp_Get]");
                string mobileNumber = "0000000000";
                string ownerName = "User";

                if (ownCnt.Tables.Count > 0 && ownCnt.Tables[0].Rows.Count > 0)
                {
                    mobileNumber = ownCnt.Tables[0].Rows[0][0]?.ToString() ?? "0000000000";
                    ownerName = ownCnt.Tables[0].Rows[0][1]?.ToString() ?? "User";
                }

                // 3. Call the new Stored Procedure
                SqlParameter[] spParams = new SqlParameter[]
                {
                    new SqlParameter("@branchId", branchId),
                    new SqlParameter("@yearId", yearId)
                };

                var dsDelay = DBOperation.FillDataSet("[dbo].[USP_LRReceipt_GetDelayLRReceipts_Get]", spParams);

                if (dsDelay == null || dsDelay.Tables.Count == 0 || dsDelay.Tables[0].Rows.Count == 0)
                {
                    return new JsonResult(new
                    {
                        responseCode = -1,
                        message = "No delayed LR receipts found to generate PDF.",
                        statusCode = 0
                    });
                }

                // Extract Company Name from the first row to put in the PDF header
                string companyName = dsDelay.Tables[0].Rows[0]["CompanyName"]?.ToString() ?? "Company Name";

                int alertDays = Convert.ToInt32(dsDelay.Tables[0].Rows[0]["alertDays"]);
                int totalCount = dsDelay.Tables[0].Rows.Count;

                // 4. Generate the clean PDF
                byte[] pdfBytes = GenerateDelayLRPdf(dsDelay, companyName);

                // 5. Save PDF locally
                string uploadsDir = @"C:\Eunoia\eunoiaweb\Docs\WhatsApp";
                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                string guid = Guid.NewGuid().ToString();
                string fileName = $"{guid}_DelayedLRReceipts.pdf";
                string filePath = Path.Combine(uploadsDir, fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, pdfBytes);

                // 6. Set up WhatsApp Public URL
                var UrlForAccess = DBOperation.FillDataSet("[dbo].[USP_UrlForAccessForWhatsApp_Get]");
                string UrlName = "http://122.163.13.47/eunoiaweb/Docs/WhatsApp";
                if (UrlForAccess.Tables.Count > 0 && UrlForAccess.Tables[0].Rows.Count > 0)
                {
                    UrlName = UrlForAccess.Tables[0].Rows[0][0]?.ToString() ?? "http://122.163.13.47/eunoiaweb/Docs/WhatsApp";
                }
                string publicUrl = $"{UrlName}/{fileName}";

                // 7. Send via WhatsApp (AiSensy)
                string apiUrl = "https://backend.api-wa.co/campaign/jam-research-services/api/v2";

                var whatsappPayload = new
                {
                    apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY4NTUwOGUxM2RhYTE0NDJjY2U5ZThhZCIsIm5hbWUiOiJKQUhOQVZJIE1JTkVSQUxTIiwiYXBwTmFtZSI6IkFpU2Vuc3kiLCJjbGllbnRJZCI6IjY4NTUwOGUwM2RhYTE0NDJjY2U5ZThhNiIsImFjdGl2ZVBsYW4iOiJOT05FIiwiaWF0IjoxNzUwNDAzMjk3fQ.ZCsEqQUn-QjQ9e2WEX2DzI4vE8bdjJnGe13dpQbrxwA",
                    campaignName = "LRReceiptAlertPrint", 
                    destination = "91" + mobileNumber,
                    userName = "JAHNAVI MINERALS",
                    templateParams = new[] { alertDays.ToString(), totalCount.ToString(), companyName },
                    source = "new-landing-page form",
                    media = new { url = publicUrl, filename = "DelayedLRReceipts.pdf" },
                    buttons = new object[] { },
                    carouselCards = new object[] { },
                    location = new { },
                    attributes = new { },
                    paramsFallbackValue = new { FirstName = "User" }
                };

                using var client = new HttpClient();
                string json = System.Text.Json.JsonSerializer.Serialize(whatsappPayload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);
                string apiResponse = await response.Content.ReadAsStringAsync();

                return new JsonResult(new
                {
                    responseCode = 0,
                    message = "WhatsApp message for Delayed LR Receipts sent successfully.",
                    statusCode = 1,
                    data = new { FileName = fileName, Url = publicUrl, Success = response.IsSuccessStatusCode, WhatsAppResponse = apiResponse }
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    responseCode = -1,
                    message = "An error occurred: " + ex.Message,
                    statusCode = 0
                });
            }
        }


        private byte[] GenerateDelayLRPdf(DataSet ds, string companyName)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4.Rotate(), 20, 20, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Title
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.DARK_GRAY);
                Paragraph title = new Paragraph("Delayed LR Receipts Summary", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                document.Add(title);

                // Date & Company Info
                Font dateFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
                Paragraph date = new Paragraph($"Generated on: {DateTime.Now:dd-MMM-yyyy HH:mm}\nCompany Name: {companyName}", dateFont)
                {
                    Alignment = Element.ALIGN_LEFT,
                    SpacingAfter = 10f
                };
                document.Add(date);

                DataTable dataTable = ds.Tables[0];

                // Map exactly to your SP Output
                var desiredColumns = new List<(string columnName, string displayName, float width)>
                {
                    ("sConsignmntNo", "LR/CN No", 12f),
                    ("dtConsignmntDt", "Date", 10f),
                    ("iNoOfDays", "Delay (Days)", 8f),
                    ("sVehicleNumber", "Vehicle No", 10f),
                    ("Material", "Material", 15f),
                    ("sRouteName", "Route Name", 15f),
                    ("sCustomerName", "Customer", 15f)
                    // CompanyName is left out of the table grid because we put it in the header, 
                    // saving valuable horizontal space on the page.
                };

                int numberOfColumns = desiredColumns.Count;
                PdfPTable pdfTable = new PdfPTable(numberOfColumns + 1)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 10f,
                    SpacingAfter = 10f
                };

                // Width logic
                float[] columnWidths = new float[numberOfColumns + 1];
                columnWidths[0] = 5f; // Sr.No
                for (int i = 0; i < desiredColumns.Count; i++)
                    columnWidths[i + 1] = desiredColumns[i].width;

                float totalWidth = columnWidths.Sum();
                for (int i = 0; i < columnWidths.Length; i++)
                    columnWidths[i] = (columnWidths[i] / totalWidth) * 100f;

                pdfTable.SetWidths(columnWidths);

                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.WHITE);
                Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 7, BaseColor.BLACK);
                BaseColor headerBgColor = new BaseColor(51, 102, 153);

                // Add Header Row
                PdfPCell srNoHeader = new PdfPCell(new Phrase("Sr.No", headerFont))
                {
                    BackgroundColor = headerBgColor,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 4f
                };
                pdfTable.AddCell(srNoHeader);

                foreach (var col in desiredColumns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(col.displayName, headerFont))
                    {
                        BackgroundColor = headerBgColor,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 4f
                    };
                    pdfTable.AddCell(cell);
                }

                // Add Data Rows (Straightforward loop, no grouping logic)
                int srNo = 1;
                foreach (DataRow row in dataTable.Rows)
                {
                    // Sr.No Cell
                    pdfTable.AddCell(new PdfPCell(new Phrase(srNo.ToString(), dataFont))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 3f
                    });

                    // Data Cells
                    foreach (var col in desiredColumns)
                    {
                        string value = row.Table.Columns.Contains(col.columnName)
                            ? (row[col.columnName] == DBNull.Value ? "" : row[col.columnName].ToString())
                            : "";

                        PdfPCell dataCell = new PdfPCell(new Phrase(value, dataFont))
                        {
                            HorizontalAlignment = col.columnName == "iNoOfDays" ? Element.ALIGN_CENTER : Element.ALIGN_LEFT, // Center align the delay days
                            Padding = 3f
                        };
                        pdfTable.AddCell(dataCell);
                    }
                    srNo++;
                }

                document.Add(pdfTable);
                document.Close();
                return memoryStream.ToArray();
            }
        }

        [HttpGet("LRReceiptAlert_JRPL")]
        public async Task<IActionResult> LRReceiptAlert_JRPL()
        {
            try
            {
                int branchId = 1009;
                int yearId = 1;

                // 1. Login to web server first
                SqlParameter[] login = new SqlParameter[]
                {
                    new SqlParameter("@EmailId", "jaisingh"),
                    new SqlParameter("@Passwordstr", "QbKdahhKaBdYfTXxspcC0XhZDNmizFC/h2TbTXGHYlQ="),
                    new SqlParameter("@BranchId", branchId)
                };
                DBOperation.FillDataSet("[dbo].[USP_MASTER_UserLoginCheck_Select]", login);

                SqlParameter[] loginBranch = new SqlParameter[]
                {
                    new SqlParameter("@roleId", 2),
                    new SqlParameter("@deptId", 5),
                    new SqlParameter("@branchId", branchId),
                    new SqlParameter("@locationId", 3406),
                    new SqlParameter("@usrcode", "U000002")
                };
                DBOperation.FillDataSet("[dbo].[USP_ADMIN_PremissionDetails_View]", loginBranch);

                // 2. Get Owner details for WhatsApp
                var ownCnt = DBOperation.FillDataSet("[dbo].[USP_OwnerNameAndContactForWhatsApp_Get]");
                string mobileNumber = "0000000000";
                string ownerName = "User";

                if (ownCnt.Tables.Count > 0 && ownCnt.Tables[0].Rows.Count > 0)
                {
                    mobileNumber = ownCnt.Tables[0].Rows[0][0]?.ToString() ?? "0000000000";
                    ownerName = ownCnt.Tables[0].Rows[0][1]?.ToString() ?? "User";
                }

                // 3. Call the new Stored Procedure
                SqlParameter[] spParams = new SqlParameter[]
                {
                    new SqlParameter("@branchId", branchId),
                    new SqlParameter("@yearId", yearId)
                };

                var dsDelay = DBOperation.FillDataSet("[dbo].[USP_LRReceipt_GetDelayLRReceipts_Get]", spParams);

                if (dsDelay == null || dsDelay.Tables.Count == 0 || dsDelay.Tables[0].Rows.Count == 0)
                {
                    return new JsonResult(new
                    {
                        responseCode = -1,
                        message = "No delayed LR receipts found to generate PDF.",
                        statusCode = 0
                    });
                }

                // Extract Company Name from the first row to put in the PDF header
                string companyName = dsDelay.Tables[0].Rows[0]["CompanyName"]?.ToString() ?? "Company Name";

                int alertDays = Convert.ToInt32(dsDelay.Tables[0].Rows[0]["alertDays"]);
                int totalCount = dsDelay.Tables[0].Rows.Count;

                // 4. Generate the clean PDF
                byte[] pdfBytes = GenerateDelayLRPdf(dsDelay, companyName);

                // 5. Save PDF locally
                string uploadsDir = @"C:\Eunoia\eunoiaweb\Docs\WhatsApp";
                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                string guid = Guid.NewGuid().ToString();
                string fileName = $"{guid}_DelayedLRReceipts.pdf";
                string filePath = Path.Combine(uploadsDir, fileName);
                await System.IO.File.WriteAllBytesAsync(filePath, pdfBytes);

                // 6. Set up WhatsApp Public URL
                var UrlForAccess = DBOperation.FillDataSet("[dbo].[USP_UrlForAccessForWhatsApp_Get]");
                string UrlName = "http://122.163.13.47/eunoiaweb/Docs/WhatsApp";
                if (UrlForAccess.Tables.Count > 0 && UrlForAccess.Tables[0].Rows.Count > 0)
                {
                    UrlName = UrlForAccess.Tables[0].Rows[0][0]?.ToString() ?? "http://122.163.13.47/eunoiaweb/Docs/WhatsApp";
                }
                string publicUrl = $"{UrlName}/{fileName}";

                // 7. Send via WhatsApp (AiSensy)
                string apiUrl = "https://backend.api-wa.co/campaign/jam-research-services/api/v2";

                var whatsappPayload = new
                {
                    apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY4NTUwOGUxM2RhYTE0NDJjY2U5ZThhZCIsIm5hbWUiOiJKQUhOQVZJIE1JTkVSQUxTIiwiYXBwTmFtZSI6IkFpU2Vuc3kiLCJjbGllbnRJZCI6IjY4NTUwOGUwM2RhYTE0NDJjY2U5ZThhNiIsImFjdGl2ZVBsYW4iOiJOT05FIiwiaWF0IjoxNzUwNDAzMjk3fQ.ZCsEqQUn-QjQ9e2WEX2DzI4vE8bdjJnGe13dpQbrxwA",
                    campaignName = "LRReceiptAlertPrint",
                    destination = "91" + mobileNumber,
                    userName = "JAHNAVI MINERALS",
                    templateParams = new[] { alertDays.ToString(), totalCount.ToString(), companyName }, // Adjust parameters based on your WhatsApp template
                    source = "new-landing-page form",
                    media = new { url = publicUrl, filename = "DelayedLRReceipts.pdf" },
                    buttons = new object[] { },
                    carouselCards = new object[] { },
                    location = new { },
                    attributes = new { },
                    paramsFallbackValue = new { FirstName = "User" }
                };

                using var client = new HttpClient();
                string json = System.Text.Json.JsonSerializer.Serialize(whatsappPayload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content);
                string apiResponse = await response.Content.ReadAsStringAsync();

                return new JsonResult(new
                {
                    responseCode = 0,
                    message = "WhatsApp message for Delayed LR Receipts sent successfully.",
                    statusCode = 1,
                    data = new { FileName = fileName, Url = publicUrl, Success = response.IsSuccessStatusCode, WhatsAppResponse = apiResponse }
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    responseCode = -1,
                    message = "An error occurred: " + ex.Message,
                    statusCode = 0
                });
            }
        }

    }
}
