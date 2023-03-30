
 A passion project to create a crypto investment portfolio tacker.

 ## Daily Programming Journal:
 
### 3/6/2023 

- **Installed EF**
  - Microsoft.EntityFrameworkCore 
  - Microsoft.EntityFrameworkCore.SqlServer 
  - Microsoft.EntiryFrameworkCore.Tools 
- **Stubbed-Out Portfolios Area**
  - This Area will contain all the Portfolio resources for an authenticated user. 
- **Created Models**
  - AppUser 
  - Crypto 
  - Exchange 
  - ExchangeTransactionType 
  - Transaction 
  - CryptoPrice 
  - **ENUM:** CommonTransactionType 
- **Configure Data Layer**
  - Created AltFuture DB in SSMS 
  - In VS SQL Server Object Explorer I right clicked the SQLExpress Server > Properties > and copied the Connection string. 
  - Added a Connection String object in the appsettings.json file. 
    - Initial Catalog=AltFuture 
  - Created AppDbConext.cs 
  - Registered AppDbContext in Program.cs builder services. 
  - Ran Add-Migration InitialCreate and Update-Database from Package Manager Console. 
- Implemented CRUD Repository Pattern 
  - Created Interface and Repository for all data sets. 
  - Wired repositories into the Program.cs builder.Services  
- Created Seed Data file _NOTE: No longer using. See notes dated 4/11/2023_
  - ./Data/Seed.cs 
    - File to seed data into the database. 
  - Run command: dotnet run seeddata 
 
 
### 3/7/2023
- **Setup Unit Testing**
  - Created 2nd project in solution: AltFutureWebApp.Test
  - Installed XUnit packages.
  - Installed Microsoft.EntityFrameworkCore.InMemory package.
  - Installed FluentAssertions package.
- **Unit Tests**
  - Created Unit Tests for Crypto Repository. All passed.
  - *ToDo: Unit test the rest of the Repositories and all of the Controllers.*
- **Stubbed-Out Admin Area**
  - This Area will contain all the Admin resources for an authenticated admin user.  
- **Refactor:**
  - Moved location of data layer files.

### 3/8/2023
- **Created Admin Area**
	- Created basic CRUD pages for maintaining Crypto data
	- Created basic CRUD pages for maintaining Exchange data
- **Create new Partial**
	- Created UserMessage.cshtml to handle displaying system messages back to the user to notify them of Success, Alert, Warning, or System message.
-  **Added FontAwesome CDN**

### 3/9/2023
- **Created new Helper Class**
    - UserMessagePartial.cs: This class makes the job of passing the needed values to the UserMessage Partial cleaner and safer by abstracting away the need for the programmer to know the ViewModel, having to Serialize the object, and potentially putting a typo in the needed TempData name.
- **To Do**:
	- Need to style the UserMessage partial and add JS so the user can close it.
	- 
### 3/10/2023
-  **Created Portfolio Summary Feature**
  - _This new feature will bind its data from a Stored Procedure, not a Table Entity
  - Stubbed out new Stored Proc: dbo.PortfolioSummaryGetAll
  - Created new /Model/StoredProcs/PortfolioSummaryGetAll.cs
  - Changed AppDbContext into a partial class.
  - Created AppDbContextStoredProcs partial class of AppDbContext
    - Added a NoKey DbSet of PortfolioSummaryGetAll
  - Created new repository PortfolioSummaryRepository.cs
  - Created new Portfolios Area Controller and View for Assets

### 3/11/2023
- **Created TransactionType Entity
	- Created Model TransactionType
	- Created ITransactionTypeRepository.cs & TransactionTypeRepository.cs
	- Added DbSet TransactionType to AppDbContext.cs
- **Setup EF Core Data Seed for Tables
	- Created a separate Config file per table and added seed data.
	- Applied Configs into the AppDbContext.cs > OnModelCreating method.
	- _NOTE: No longer using the original manual /Data/Seed.cs file.
- **Fully implemented SP: dbo.PortfolioSummaryGetAll
	- Created Migration for SP for versioning.
- **EF Migration Updates
	- CreateTransactionTypeTable_AlterExchangeTransactionTypeTable.cs
	- CreateForeignKey_Transactions_ExchangeTransactionTypeID.cs
	- NewEFCoreSeedData.cs
	- CreateStoredProc_PortfolioSummaryGetAll.cs
### 3/12/2023
- **Architecture Overhaul
	- Refactored from 1-Tier to N-Tier architecture.
	- Class Library AltFuture.DataAccessLayer
	- Class Library AltFuture.Models
### 3/13/2023
- Created CoinMarketCapAPI Class Library in solution
	- Used to retrieve crypto market data from CoinMarketCap.com
	- Created custom Models to load API data.
		- Due to API Json formatting issues I couldn't just deserialize returned response.
	- Created appsettings.json: CoinMarketCapSettings
		- Api Keys (Pro and Sandbox).
			- The Pro Key value is in Secrets.
		- Base Urls (Pro and Sandbox)
		- EndPointsV2
	- Used IOptions Pattern in Program.cs to make available strongly typed access to EndPointsV2
	- Configured 2 named HttpClients in Program.cs. Pro and Sandbox versions.
	- Implemented GetQuotesLatestAsync method
		- Accepts an array of crypto symbols to request market data from API endpoint.
		- Returns list of crypto info and price data.
### 3/14/2023
- Implemented a call to the CoinMarketCapAPI GetQuotesLatestAsync and store the result in the local database.
- Ensured that the new price data reflects correctly in the Portfolio/Assets feature.
- Clean up and refactoring of the API layer.
- Created new API call of GetKeyInfoAsync to get usage data about the Pro account.
- Created Admin page to view the KeyInfo

### 3/15/2023 - 3/18/2023
- Created Business Logic Layer
- Created Controller and View to facilitate the uploading of a Coinbase Transaction History CSV file
- Installed CsvReader package
- Installed AutoMapper package
- Created DTO to store Coinbase Transaction History data specifics read from the CSV file.
- Step 1: Get CSV file from user
- Step 2: Use CsvReader to build a List of Coinbase Transaction specific DTOs
- Step 3: Converted TransactionTypes and Exchanges from Table Entities to Enum Entities to improve coding, efficiency, and maintain Single Source of data.
- Step 4: Created Singletons of ExchangeTransactionType and Crypto data Lists.
	- Their data is mostly stable and needed for heavy business logic use.
	- Ensured Thread safety and data concurrency.
- Step 5: Created AutoMapper Profile and Resolvers to translate Coinbase Transactions to App Transactions.
- Step 6: Save mapped Coinbase transactions as a Range to the Transactions DB table.
- Improvements to the UserMessage Partial View
- Refactoring to ensure SOLID Principles and Clean Architecture
- Seed data corrections
- Database Migrations
- Removal of Exchange CRUD. Not needed because adding a new Exchange requires other programming updates. Also switched Exchange Entity to be base off of a Enum for Single Source concerns.

### 3/19/2023
- Populate the Home page with the latest crypto prices from CoinMarketCap API
- Added new data read methods to the CryptoPriceRepsitory.
- Created new CoinMarketCapQuotesLatest Service that implements a rate limiting check to access the API. The service, base on rate limit, will either sync from the API, or call cached data from DB.


### 3/20/2023
- Added new fields to CryptoQuote model
- Updated COinMarketCapQuotesLatest.cs service to map the new fields from the API to the Model.
- Database Migration
- Updated Home View to show new data points


### 3/21/2023
- Adding Google Charts to the Dashboard page.
- Installed Microsoft.AspNetCore.Mvc.NewtonsoftJson to allow Newtonsoft serialization of Json returned Actions.
- Built DashboardChartsData service
	- Get data for pie chart of crypto investement percentages
	- Get data for column chart showing investment performance per crypto asset.
- Wired up charts to data service on Dashboard page.

### 3/22/2023
- Created computed column property of Transaction.InvestmentTotal. Migrated to DB.
- Fixed/Updated stored proc PortfolioSummaryGetAll.
	- Made use of the new InvestmentTotal calc field.
	- Factored in Fees into the Unrealized Profit calculation.
	- Add FeeTotal to the result set.
	- Migrated to DB.
- Adjusted the Asset Portfolio View to wire up to new properties. Beautified as well.
- Implemented the DashboardChartsData.GetCryptoInvestmentPerformanceAsync method
- Improved Dashboard View html

### 3/23/2023
- Took a step back today to look at the big picture of how my features were coming together.
- Did a bunch of consolidating, clean up and architecture refactoring.
- Combined the Portfolio Dashboard and Asset modules into just Dashboard
- Realized I had the Chart data access modules in the BLL. Moved to DAL.
- Originally thought I might like having a separate project to hold the Models. Hated it.
	- Moved Models from their own class library and put them in the DAL. 
	- Refactored code set to point to new Models namespace.
- Renamed project AltFutureWebApp to AltFuture.WebApp.
	- Wanted to do it now while it was still early. It would have bugged me forever.
	- Updated namespaces and project references.	
- Naming convention clean up of PortfolioChartData service and methods.
- Created new pie chart feature for showing Exchange Usage data on dashboard.

### 3/24/2023
- Changed method name of CryptoPriceRepository GetLastestAsync to GetLatestPricesAsync.
- Improved LINQ query for CryptoPriceRepository.GetLatestPricesAsnyc
- Changed hard coded count tests to dynamic count in CryptoRespoitoryTests.cs

### 3/25/2023 - 3/29/2023
- Removed AltFuture.CoinMarketCapAPI
- Rewrote as AltFuture.MarketDataConsumer
- Did this in order to build it from the ground up using SOLID design principles and decoupling from concrete dependencies on using the CoinMarketCap API.
- Any market data API can now easily be swapped in or out without affecting any high-level entities.
- Switched from using a Named HttpClient to a Typed HttpClient.

### 3/30/2023
- Moved PortfoliochartData service from DLL to BLL. It was in wrong layer.
- Moved BLL.Models.DTOs.CoinbaseTransactionHistoryDto.cs to BLL.Models.ExchangeTransaction folder in prep for building out more Exchange Transaction import features.
