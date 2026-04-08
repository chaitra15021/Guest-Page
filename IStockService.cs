using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockApp.DTO;

namespace StockApp.Interfaces
{
    public interface IStockService
    {
         Task<StockResponseDto> GetStockAsync(string symbol);
    }
}