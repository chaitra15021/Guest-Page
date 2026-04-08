using System;
using System.Collections.Generic;
using StockApp.Models;

namespace StockApp.Interfaces
{
    public interface IStockRepository
    {
        Task SaveAsync(StockData stock);
    }
}