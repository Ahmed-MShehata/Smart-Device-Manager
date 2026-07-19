using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;

Console.WriteLine("Starting Final Verification...");

// 0. Fix password hash in DB for admin user and prepare dummy software file
var hasher = new PasswordHasher<object>();
var rawHash = hasher.HashPassword(null, "password"); // Hash for "password" using PBKDF2
var connStr = "Server=.;Database=SmartDeviceManagerDb;Trusted_Connection=True;TrustServerCertificate=True;";
try {
    using var conn = new SqlConnection(connStr);
    conn.Open();
    using var cmd = new SqlCommand("UPDATE AdminUsers SET PasswordHash = @P WHERE Username = 'admin'", conn);
    cmd.Parameters.AddWithValue("@P", rawHash);
    cmd.ExecuteNonQuery();
    Console.WriteLine("Updated admin password hash to valid ASP.NET Core Identity format.");

    // Create dummy.zip in wwwroot so download test passes
    var webRoot = Path.Combine(Directory.GetCurrentDirectory(), "..", "src", "SDM.API", "wwwroot");
    Directory.CreateDirectory(webRoot);
    File.WriteAllText(Path.Combine(webRoot, "dummy.zip"), "dummy payload");
} catch (Exception ex) {
    Console.WriteLine("Failed to update password hash or create dummy file: " + ex.Message);
}


var handler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
};
var client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:7156/") };

async Task<HttpResponseMessage> CheckEndpoint(string method, string url, object payload = null)
{
    Console.Write($"[{method}] {url} -> ");
    HttpResponseMessage response = null;
    try
    {
        switch (method.ToUpper())
        {
            case "GET": response = await client.GetAsync(url); break;
            case "POST": response = await client.PostAsJsonAsync(url, payload); break;
            case "PUT": response = await client.PutAsJsonAsync(url, payload); break;
            case "DELETE": response = await client.DeleteAsync(url); break;
        }
        Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode} - {(response.IsSuccessStatusCode ? "PASS" : "FAIL")}");
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"  Body: {await response.Content.ReadAsStringAsync()}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"ERROR: {ex.Message}");
    }
    return response;
}

// 1. Health
await CheckEndpoint("GET", "api/health");

// 2. Auth
Console.WriteLine("\n-- Authenticating --");
var authRes = await CheckEndpoint("POST", "api/v1/auth/login", new { Username = "admin", Password = "password" });
if (!authRes.IsSuccessStatusCode) return;

var authJson = await authRes.Content.ReadFromJsonAsync<JsonElement>();
var token = authJson.GetProperty("data").GetProperty("token").GetString();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
Console.WriteLine($"Token acquired: {token.Substring(0, 15)}...");

// 3. Dashboard
Console.WriteLine("\n-- Dashboard --");
await CheckEndpoint("GET", "api/v1/dashboard");

// 4. Products
Console.WriteLine("\n-- Products --");
var pCreate = await CheckEndpoint("POST", "api/v1/products", new { Name = "Test Prod", Brand = "TestBrand", Category = 1, Description = "Desc", Price = 99.99m, Quantity = 10, WarrantyMonths = 12 });
var pId = (await pCreate.Content.ReadFromJsonAsync<JsonElement>()).GetProperty("data").GetProperty("id").GetGuid();
await CheckEndpoint("GET", $"api/v1/products/{pId}");
await CheckEndpoint("PUT", $"api/v1/products/{pId}", new { Id = pId, Name = "Test Prod Updated", Brand = "TestBrand", Category = 1, Description = "Desc", Price = 100m, Quantity = 10, WarrantyMonths = 12, Status = 1 });
await CheckEndpoint("GET", "api/v1/products?search=Updated&page=1&pageSize=10");
// (Product deletion moved to the end)

// 5. Software Packages
Console.WriteLine("\n-- Software Packages --");
var sCreate = await CheckEndpoint("POST", "api/v1/softwarepackages", new { Name = "WinApp", Category = "Application", Version = "1.0", Description = "Software", SetupFileUrl = "dummy.zip" });
var sId = (await sCreate.Content.ReadFromJsonAsync<JsonElement>()).GetProperty("data").GetProperty("id").GetGuid();
await CheckEndpoint("GET", $"api/v1/softwarepackages/{sId}");
await CheckEndpoint("PUT", $"api/v1/softwarepackages/{sId}", new { Id = sId, Name = "WinApp V2", Category = "Application", Description = "Updates" });
await CheckEndpoint("GET", "api/v1/softwarepackages?search=V2");
await CheckEndpoint("GET", $"api/v1/softwarepackages/{sId}/download");
await CheckEndpoint("DELETE", $"api/v1/softwarepackages/{sId}");

// 6. Knowledge Base
Console.WriteLine("\n-- Knowledge Base --");
var kCreate = await CheckEndpoint("POST", "api/v1/knowledgebase", new { ProblemName = "Bug", Description = "Fix", Category = "General", DisplayOrder = 1 });
var kId = (await kCreate.Content.ReadFromJsonAsync<JsonElement>()).GetProperty("data").GetProperty("id").GetGuid();
await CheckEndpoint("GET", $"api/v1/knowledgebase/{kId}");
await CheckEndpoint("PUT", $"api/v1/knowledgebase/{kId}", new { Id = kId, ProblemName = "Bug Fixed", Description = "Fix", Category = "General", DisplayOrder = 2, Visible = true });
await CheckEndpoint("GET", "api/v1/knowledgebase?search=Fixed");
await CheckEndpoint("DELETE", $"api/v1/knowledgebase/{kId}");

// 7. Admin Users
Console.WriteLine("\n-- Admin Users --");
var randUser = "testadmin_" + Guid.NewGuid().ToString("N").Substring(0, 5);
var randUser2 = randUser + "_2";
var uCreate = await CheckEndpoint("POST", "api/v1/adminusers", new { Username = randUser, Password = "Password123!", Role = 1 });
var uId = (await uCreate.Content.ReadFromJsonAsync<JsonElement>()).GetProperty("data").GetProperty("id").GetGuid();
await CheckEndpoint("GET", $"api/v1/adminusers/{uId}");
await CheckEndpoint("PUT", $"api/v1/adminusers/{uId}", new { Id = uId, Username = randUser2, Role = 1 });
await CheckEndpoint("GET", $"api/v1/adminusers?search={randUser2}");
await CheckEndpoint("DELETE", $"api/v1/adminusers/{uId}"); // Deactivates the user
await CheckEndpoint("PUT", $"api/v1/adminusers/{uId}/status", new { Id = uId, IsActive = true }); // Reactivates the user

// 8. Orders
Console.WriteLine("\n-- Orders --");
var oCreate = await CheckEndpoint("POST", "api/v1/orders", new { CustomerName = "John", CustomerEmail = "a@a.com", CustomerPhone = "123", CustomerGovernorate = "Cairo", CustomerAddress = "123 Street", ShippingAddress = "123 Street", Notes = "N/A", Items = new[] { new { ProductId = pId, Quantity = 1, Price = 100m, ProductName = "A" } } });
var oId = (await oCreate.Content.ReadFromJsonAsync<JsonElement>()).GetProperty("data").GetProperty("id").GetGuid();
await CheckEndpoint("GET", $"api/v1/orders/{oId}");
await CheckEndpoint("PUT", $"api/v1/orders/{oId}/status", new { Id = oId, Status = 1 }); 
await CheckEndpoint("GET", "api/v1/orders?page=1&pageSize=5");
await CheckEndpoint("DELETE", $"api/v1/orders/{oId}");

Console.WriteLine("\n-- Cleanup --");
await CheckEndpoint("DELETE", $"api/v1/products/{pId}");

Console.WriteLine("\nVerification Complete.");
