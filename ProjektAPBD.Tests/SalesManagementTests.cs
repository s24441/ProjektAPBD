namespace ProjektAPBD.Tests
{
    public class SalesManagementTests
    {
        [Fact]
        public void AddSale_Should_Throw_PriceTooLowException_When_Price_Is_Lower_Than_Zero()
        {

        }

        [Fact]
        public void AddSale_Should_Throw_InvalidDateException_When_ExpirationDaysRange_Is_Lower_Than_3()
        {

        }

        [Fact]
        public void AddSale_Should_Throw_InvalidDateException_When_ExpirationDaysRange_Is_Higher_Than_30()
        {

        }

        [Fact]
        public void AddSale_Should_Throw_InvalidSupportYearsAmountException_When_AdditionalSupportYearsAmount_Is_Lower_Than_Zero()
        {

        }

        [Fact]
        public void AddSale_Should_Throw_InvalidSupportYearsAmountException_When_AdditionalSupportYearsAmount_Is_Higher_Than_3()
        {

        }

        [Fact]
        public void AddSale_Should_Throw_ClientNotExistsException_When_Given_IdClient_Not_Exists_In_The_Database()
        {

        }

        [Fact]
        public void AddSale_Should_Throw_ProductNotExistsException_When_Given_IdProduct_Not_Exists_In_The_Database()
        {

        }

        /* Spróbować dodać zniżki */

        [Fact]
        public void AddClient_Should_Add_New_Sale_Into_Database()
        {

        }

        [Fact]
        public void PayForSale_Should_Throw_PaymentValueException_When_Payment_Is_Lower_Than_Zero()
        {

        }

        [Fact]
        public void PayForSale_Should_Throw_SaleNotExistsException_When_Given_IdContract_Not_Exists_In_The_Database()
        {

        }

        [Fact]
        public void PayForSale_Should_Throw_InactiveSaleException_When_PaymentDay_Is_Lower_Than_Contract_CreationDate()
        {

        }

        [Fact]
        public void PayForSale_Should_Throw_InactiveSaleException_When_PaymentDay_Is_Higher_Than_Contract_ExpirationDate()
        {

        }

        [Fact]
        public void PayForSale_Should_Throw_PaymentValueException_When_Sum_Of_Contract_Payments_Actual_Payment_Is_Higher_Than_Contract_Price()
        {

        }

        [Fact]
        public void PayForSale_Should_Add_New_Payment_Into_Database()
        {

        }
    }
}
