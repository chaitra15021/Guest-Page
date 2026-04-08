using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using StockApp.DTO;
using StockApp.Interfaces;
using StockApp.Models;

namespace StockApp.Services
{
    public class StockService : IStockService
    {
         private readonly IStockRepository _repository;
    private readonly HttpClient _httpClient;

    public StockService(IStockRepository repository, HttpClient httpClient)
    {
        _repository = repository;
        _httpClient = httpClient;
    }

    public async Task<StockResponseDto> GetStockAsync(string symbol)
    {
        // ✅ Validation
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be empty");

        string apiKey = "YOUR_API_KEY";

        string url = $"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={apiKey}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception("API call failed");

        var json = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);

        var root = doc.RootElement.GetProperty("Global Quote");

        if (!root.TryGetProperty("05. price", out var priceElement))
            throw new Exception("Invalid symbol");

        decimal price = decimal.Parse(priceElement.GetString());

        // Save to DB
        var stock = new StockData
        {
            Symbol = symbol,
            Price = price
        };

        await _repository.SaveAsync(stock);

        // Return DTO
        return new StockResponseDto
        {
            Symbol = symbol,
            Price = price
        };
    }
    }
}