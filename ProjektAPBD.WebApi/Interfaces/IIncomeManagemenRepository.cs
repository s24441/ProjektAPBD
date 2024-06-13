﻿namespace ProjektAPBD.WebApi.Interfaces
{
    public interface IIncomeManagemenRepository
    {
        Task<decimal> GetActualIncomeAsync(int? idProduct = default, string? currency = default);
        Task<decimal> GetIncomePrognosisAsync(int? idProduct = default, string? currency = default);
    }
}
