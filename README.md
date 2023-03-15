
 A passion project to create a crypto investment portfolio tacker.

 ## Daily Programming Journal:
 
### 4/6/2023 

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
  - In VS SQL Server Object Explorer I right clicked the SQLExpress Server > Properites > and copied the Connection string. 
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
 
 
### 4/7/2023
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

### 4/8/2023
- **Created Admin Area**
	- Created basic CRUD pages for maintaining Crypto data
	- Created basic CRUD pages for maintaining Exchange data
- **Create new Partial**
	- Created UserMessage.cshtml to handle displaying system messages back to the user to notify them of Success, Alert, Warning, or System message.
-  **Added FontAwesome CDN**

### 4/9/2023
- **Created new Helper Class**
    - UserMessagePartial.cs: This class makes the job of passing the needed values to the UserMessage Partial cleaner and safer by abstracting away the need for the programmer to know the ViewModel, having to Serialize the object, and potentially putting a typo in the needed TempData name.
- **To Do**:
	- Need to style the UserMessage partial and add JS so the user can close it.
	- 
### 4/10/2023
-  **Created Portfolio Summary Feature**
  - _This new feature will bind its data from a Stored Procedure, not a Table Entity
  - Stubbed out new Stored Proc: dbo.PortfolioSummaryGetAll
  - Created new /Model/StoredProcs/PortfolioSummaryGetAll.cs
  - Changed AppDbContext into a partial class.
  - Created AppDbContextStoredProcs partial class of AppDbContext
    - Added a NoKey DbSet of PortfolioSummaryGetAll
  - Created new repository PortfolioSummaryRepository.cs
  - Created new Portfolios Area Controller and View for Assets

### 4/11/2023
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
	
### 4/12/2023
- **Architecture Overhaul
	- Re-factored from 1-Tier to N-Tier architecture.
	- Class Library AltFuture.DataAccessLayer
	- Class Library AltFuture.Models	
	
### 4/13/2023
- Created CoinMarketCapAPI Class Library in solution
	- Used to retrieve crypto market data from CoinMarketCap.com
	- Created custom Models to load API data.
		- Due to API Json formatting issues I couldn't just deserialize returned response.
	- Created appsettings.json: CoinMarketCapSettings
		- Api Keys (Pro and Sandbox).
			- The Pro Key value is in Secrets.
		- Base Urls (Pro and Sandbox).
		- EndPointsV2
	- Used IOptions Pattern in Program.cs to make available strongly typed access to EndPointsV2
	- Configured 2 named HttpClients in Program.cs. Pro and Sandbox versions.
	- Implemented GetQuotesLatestAsync method
		- Accepts an array of crypto symbols to request market data from API endpoint.
		- Returns list of crypto info and price data.
		
### 4/14/2023
- Implemented a call to the CoinMarketCapAPI GetQuotesLatestAsync and store the result in the local database.
- Ensured that the new price data reflects correctly in the Portfolio/Assets feature.
- Clean up and refactoring of the API layer.
- Created new API call of GetKeyInfoAsync to get usage data about the Pro account.
- Created Admin page to view the KeyInfo