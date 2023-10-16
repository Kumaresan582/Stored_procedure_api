using DataBaseNull;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Stored_procedure_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SPController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly spdbcontext _spdbcontext;

        //private readonly spdbcontext _context;

        public SPController(IConfiguration configuration, spdbcontext spdbcontext)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("Kumaresan");
            _spdbcontext = spdbcontext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("GetStudents", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            List<StudentModel> results = new List<StudentModel>();

                            while (await reader.ReadAsync())
                            {
                                StudentModel model = new StudentModel
                                {
                                    Student_Id = reader.TryGetInt32("Student_Id"),
                                    Student_Name = reader.TryGetString("Student_Name"),
                                    //Students_Age = reader.IsDBNull(reader.GetOrdinal("Student_Age")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Student_Age")),
                                    Students_Age = reader.TryGetInt32("Student_Age"),
                                    Students_Course = reader.TryGetString("Student_Course"),
                                    Students_Mark = reader.TryGetInt32("Student_Marks"),
                                };

                                results.Add(model);
                            }

                            await connection.CloseAsync();
                            return Ok(results);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("GetStudentById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                var mod = new StudentModel();
                                reader.TryGetInt32("Student_Id", x => mod.Student_Id = x);
                                reader.TryGetString("Student_Name", x => mod.Student_Name = x);
                                reader.TryGetInt32("Student_Age", x => mod.Students_Age = x);
                                reader.TryGetString("Student_Course", x => mod.Students_Course = x);
                                reader.TryGetInt32("Student_Marks", x => mod.Students_Mark = x);

                                /* StudentModel model = new StudentModel
                                 {
                                     Student_Id = reader.GetInt32("Student_Id"),
                                     Student_Name = reader.GetString("Student_Name"),
                                     Students_Age = reader.GetInt32("Student_Age"),
                                     Students_Course = reader.GetString("Student_Course"),
                                     Students_Mark = reader.GetInt32("Student_Marks")
                                 };*/
                                await connection.CloseAsync();
                                return Ok(mod);
                            }

                            return NotFound();
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, $"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid input data");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("InsertStudent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@stud_Id", model.Student_Id);
                        command.Parameters.AddWithValue("@Student_Name", model.Student_Name);
                        command.Parameters.AddWithValue("@Student_Course", model.Students_Course);
                        command.Parameters.AddWithValue("@Student_Age", model.Students_Age);
                        command.Parameters.AddWithValue("@Student_Marks", model.Students_Mark);

                        await command.ExecuteNonQueryAsync();

                        return Ok("Student inserted successfully");
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return StatusCode(500, $"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudentModel model)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("UpdateStudent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@stud_Id", id);
                        command.Parameters.AddWithValue("@Student_Name", model.Student_Name);
                        command.Parameters.AddWithValue("@Student_Course", model.Students_Course);
                        command.Parameters.AddWithValue("@Student_Age", model.Students_Age);
                        command.Parameters.AddWithValue("@Student_Marks", model.Students_Mark);

                        await command.ExecuteNonQueryAsync();

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("DeleteStudent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID", id);

                        await command.ExecuteNonQueryAsync();

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetTwoTableJoin")]
        public IActionResult GetTwoTableJoin()
        {
            try
            {
                List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetStudentGenderDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Dictionary<string, object> row = new Dictionary<string, object>();

                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        string columnName = reader.GetName(i);
                                        object columnValue = reader.IsDBNull(i) ? null : reader.GetValue(i);
                                        row[columnName] = columnValue;
                                    }

                                    resultList.Add(row);
                                }

                                reader.Close();
                            }
                            else
                            {
                                return NotFound("No data found.");
                            }
                        }
                    }
                }

                return Ok(resultList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("UpdateTwoTableJoin")]
        public IActionResult UpdateTwoTableJoin()
        {
            try
            {
                List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UpdateStudentDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@stud_Id", id);

                        command.ExecuteNonQuery();
                    }
                }

                return Ok(resultList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("CreatePdf")]
        public IActionResult CreatePdf()
        {
            try
            {
                // Create a new document
                iTextSharp.text.Document doc = new iTextSharp.text.Document();

                // Set the output file path (change as needed)
                string outputPath = "E:\\New folder\\output.pdf";

                // Create a MemoryStream to write the PDF content
                MemoryStream memoryStream = new MemoryStream();

                // Create a PdfWriter to write the document to the MemoryStream
                PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);

                // Open the document for writing
                doc.Open();

                // Create a new page
                doc.NewPage();

                // Add content to the PDF (e.g., text, images, tables)
                // Example: Adding text
                Paragraph paragraph = new Paragraph("Hello, this is a sample PDF!");
                doc.Add(paragraph);

                // Close the document and the writer
                doc.Close();
                writer.Close();

                // Save the PDF to a file
                using (FileStream fs = new FileStream(outputPath, FileMode.Create))
                {
                    byte[] pdfBytes = memoryStream.ToArray();
                    fs.Write(pdfBytes, 0, pdfBytes.Length);
                }

                Console.WriteLine("PDF created successfully at: " + outputPath);

                // Return the PDF file as a response
                return File(memoryStream.ToArray(), "application/pdf", "output.pdf");
            }
            catch (ObjectDisposedException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Transaction")]
        public IActionResult CreateOrder([FromBody] CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string customerName = model.CustomerName;
                    int OrderId = model.OrderId;
                    List<OrderItem> orderItems = model.OrderItems;

                    CreateOrder(_connectionString, OrderId, customerName, orderItems);

                    return Ok("Order created successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to create the order. Reason: {ex.Message}");
                }
            }
            else
            {
                return BadRequest("Model validation failed");
            }
        }

        private static bool CreateOrder(string con, int OrderId, string customerName, List<OrderItem> orderItems)
        {
            using (SqlConnection sqlConnection = new SqlConnection(con))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                SqlTransaction sqlTransaction;

                sqlTransaction = sqlConnection.BeginTransaction("CreateOrder");
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Transaction = sqlTransaction;

                try
                {
                    sqlCommand.CommandText = $"INSERT INTO OrderOn (OrderID, CustomerName, TotalAmount) VALUES ({OrderId},'{customerName}', 0.00)";
                    sqlCommand.ExecuteNonQuery();

                    /*
                    sqlCommand.CommandText = "SELECT SCOPE_IDENTITY()";
                    int orderID = Convert.ToInt32(sqlCommand.ExecuteScalar());*/

                    int OrderItemID = 102;
                    foreach (var item in orderItems)
                    {
                        sqlCommand.CommandText = $"INSERT INTO OrderItems (OrderItemID,OrderID, ProductName, Quantity, Price) " +
                                              $"VALUES ({OrderItemID}, {OrderId}, '{item.ProductName}', {item.Quantity}, {item.Price})";
                        sqlCommand.ExecuteNonQuery();
                        OrderItemID++;
                    }

                    sqlCommand.CommandText = $"UPDATE OrderOn SET TotalAmount = (SELECT SUM(Quantity * Price) FROM OrderItems WHERE OrderID = {OrderId}) WHERE OrderID = {OrderId}";
                    sqlCommand.ExecuteNonQuery();

                    sqlTransaction.Commit();
                    sqlConnection.Close();
                    Console.WriteLine("Order created successfully.");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Order creation failed. Reason: {ex.Message}");
                    try
                    {
                        sqlTransaction.Rollback();
                        Console.WriteLine("Transaction rolled back.");
                        return false;
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"Rollback failed. Reason: {exception.Message}");
                        return false;
                    }
                }
            }
        }

        [HttpGet("IdGenerate")]
        public IActionResult UserIdGenerate()
        {
            int currentYear = DateTime.Now.Year;
            int lastTwoDigits = currentYear % 100;
            var userQuery = _spdbcontext.UserIdGenerate
                .Where(u => u.UserId.StartsWith($"VAF-{lastTwoDigits}"))
                .Select(u => u.UserId);

            var userIDs = userQuery.ToList();
            if (userIDs.Count == 0)
                return Ok($"VAF-{lastTwoDigits}01");
            var maxNumericPart = userIDs
                .Select(u => int.Parse(u.Split('-').Last()))
                .Max();
            string newUserId = $"VAF-{(maxNumericPart + 1)}";

            return Ok(newUserId);
        }
    }
}