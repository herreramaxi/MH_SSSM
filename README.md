# Super Simple Stock Market by Maxi Herrera

### Index

- [Requirements](#Requirements)
- [Solution](#Solution)
- [Assumptions and considerations](#Assumptions-and-considerations)
- [Running the solution in the cloud](#Running-the-solution-in-the-cloud)
- [How to clean in-memory data](#How-to-clean-in-memory-data)
- [How to run the test cases](#How-to-run-the-test-cases)
- [Test cases](#Test-cases)
- [Calculations](#Calculations)
- [How to run the solution locally](#How-to-run-the-solution-locally)
  * [Backend - Web API](#backend---Web-API)
  * [UI](#UI)
- [Architecture](#Architecture)
- [Nodes urls](#Nodes urls)
- [Github repositories](#Github-repositories)
- [Stack](#Stack)

## Requirements
[Requirements.md](https://github.com/herreramaxi/MH_SSSM/blob/main/resources/Requirements.md)

## Solution
My proposal solution is a distributed cloud application composed by two nodes, one for the UI and the other for the backend. The frontend node is the customer facing UI implemented with React as a single page application (SPA). The second node is the backend, which has two critical missions: 
 * To provide its calculation services to frontend node through its Web API. 
 * To validates the calculations required by a NUnit test project.

### Assumptions and considerations
* Last price: Rather than sending the price and stock symbol to calculate “dividend yield” and “P/E Ratio”, the application requires at least to perfrom one trade for a given stock. This is because the last trade’s price is taken as a last price.
* P/E Ratio: As the formula does not specify if the “Dividend” on its denominator is the “Last Dividend” or “Dividend Yield”. I assumed that the correct approach would be to use the “Last dividend” so that the resulting formula would be: DY = Price/Last Dividend.
* GBCE All Share Index: I followed same approach as “Volume Weighted Stock Price” so that the index is based on trades in past 15 minutes to limit the volume of data.
To calculate the stock price, which is used on geometric mean, I am applying the "Volume Weighted Stock Price" for the selected stock. After that, the geometric mean is applied for every stock price previously calculated.
* Backend language: c#.
* Frontend: React.
* Error handling backend: global filter exception with Log4Net, which can be read with heroku [ExceptionFilter.cs](https://github.com/herreramaxi/MH_SSSM/blob/c0c66b215ac52b4b1c5acaf50e089ce6b9f1d04a/SSSM.WebAPI/Infrastructure/ExceptionFilter.cs)
* HTTPS enabled in both nodes but no redirect is forced.
* There is no authentication flow between frontend and backend endpoints, this is a pending.
* CORS is enabled on backend.

## Running the solution in the cloud
*Web app -->* [https://mh-sssm-ui.herokuapp.com](https://mh-sssm-ui.herokuapp.com)

##### Steps
1. Navigate to the above web app.
2. Selects a stock symbol from list provided.
3. Perform a trade which requires the following fields: price, quantity of shares and trade indicator.

##### Expected results
* The right panel will show calculations for the last price, which was traded, and the selected stock symbol.
* The left graphic panel will display the last price together with previous prices from trades.
* The table located on the botton of the page will display the last trade together with previous trades.
##### Dashboard
<img src="https://github.com/herreramaxi/MH_SSSM/blob/main/resources/DashboardWIthAnnotations.png" alt="DashboardWIthAnnotations" width="400" height="300">

## How to clean in-memory data
For testing purpose it is possible to clean/delete all the in-memory data, which includes trades and last prices.
On the top right corner, clicks on "delete icon" and confirm the operation in dialog.

## How to run the test cases
1. Download or clone repository
2. Open the Visual Studio solution [SSSM.sln](https://github.com/herreramaxi/MH_SSSM/blob/main/SSSM.sln) (NB: I used VS 2019)
3. Click on "Test/Run All Tests"

<img src="https://github.com/herreramaxi/MH_SSSM/blob/main/resources/TestsRunAll.JPG" alt="TestsRunAll" width="600" height="180">

### Test cases
- [DividendYieldUnitTests.cs](https://github.com/herreramaxi/MH_SSSM/blob/main/SSSM.Test/DividendYieldUnitTests.cs) 
 - [PERatioUnitTests.cs](https://github.com/herreramaxi/MH_SSSM/blob/main/SSSM.Test/PERatioUnitTests.cs)
 - [GetVolumeWeightedStockPriceUnitTests.cs](https://github.com/herreramaxi/MH_SSSM/blob/main/SSSM.Test/GetVolumeWeightedStockPriceUnitTests.cs)
 - [GBCEAllShareIndexUnitTests.cs](https://github.com/herreramaxi/MH_SSSM/blob/main/SSSM.Test/GBCEAllShareIndexUnitTests.cs)

## Calculations

* #### Dividend yield

  - [StockMarketService.cs#L52](https://github.com/herreramaxi/MH_SSSM/blob/fa0ff755d90efb997d60437d8ae79a2a7036cf99/SSSM.Services/StockMarketService.cs#L52)
  - [CommonStock.cs#L22](https://github.com/herreramaxi/MH_SSSM/blob/fa0ff755d90efb997d60437d8ae79a2a7036cf99/SSSM.Model/CommonStock.cs#L22)
  - [PreferredStock.cs#L11](https://github.com/herreramaxi/MH_SSSM/blob/fa0ff755d90efb997d60437d8ae79a2a7036cf99/SSSM.Model/PreferredStock.cs#L11)

* #### P/E Ratio
  - [StockMarketService.cs#L59](https://github.com/herreramaxi/MH_SSSM/blob/fa0ff755d90efb997d60437d8ae79a2a7036cf99/SSSM.Services/StockMarketService.cs#L59)
  - [CommonStock.cs#L27](https://github.com/herreramaxi/MH_SSSM/blob/fa0ff755d90efb997d60437d8ae79a2a7036cf99/SSSM.Model/CommonStock.cs#L27)

* #### Volume Weighted Stock Price based on trades in past 15 minutes

  - [StockMarketService.cs#L66](https://github.com/herreramaxi/MH_SSSM/blob/fa0ff755d90efb997d60437d8ae79a2a7036cf99/SSSM.Services/StockMarketService.cs#L66)

* #### GBCE All Share Index

  - [StockMarketService.cs#L81](https://github.com/herreramaxi/MH_SSSM/blob/fa0ff755d90efb997d60437d8ae79a2a7036cf99/SSSM.Services/StockMarketService.cs#L81)

## How to run the solution locally

#### Backend - Web API
1. Download zip file from [MH_SSSM-main.zip](https://github.com/herreramaxi/MH_SSSM/archive/refs/heads/main.zip) or download/clone repository from [MH_SSSM](https://github.com/herreramaxi/MH_SSSM)
2. Open folder MH_SSSM-main
3. Open solution SSSM.sln with Visual Studio 2019 or above.
4. Select as defaul the project: SSSM.WebAPI
5. Press run or F5

##### Expected result
The web API will run on [http://localhost:60485](http://localhost:60485), which is configured on [launchSettings.json#L6](https://github.com/herreramaxi/MH_SSSM/blob/f1dad3506adf80df8227a59cd79cb4ec52909a73/SSSM.WebAPI/Properties/launchSettings.json#L6)

NB: If you need to change web api port you would have to change two settings so that ui and backend can communicate:
* Backend: [launchSettings.json#L6](https://github.com/herreramaxi/MH_SSSM/blob/f1dad3506adf80df8227a59cd79cb4ec52909a73/SSSM.WebAPI/Properties/launchSettings.json#L6)
* UI: [.env#L2](https://github.com/herreramaxi/MH_SSSM_UI/blob/8923a58ed8ec00b35bba996ceaf61eb1ad272cb9/.env#L2)

#### UI
1. Download zip file from [MH_SSSM_UI-main.zip](https://github.com/herreramaxi/MH_SSSM_UI/archive/refs/heads/main.zip) or download/clone repository from [MH_SSSM_UI](https://github.com/herreramaxi/MH_SSSM_UI)
2. Open folder MH_SSSM_UI-main (where package.json is located)
3. npm install
4. npm run start

##### Expected result
* The web app will run on [http://localhost:3000](http://localhost:3000), which is the standard for React apps.
* UI will load and display the [Dashboard](#dashboard)

## Architecture
#### Could
<img src="https://github.com/herreramaxi/MH_SSSM/blob/main/resources/Architecture.png" width="900" height="300">

#### Component dependencies, backend - Web API
<img src="https://github.com/herreramaxi/MH_SSSM/blob/main/resources/Component%20dependencies.png" width="400" height="300">

#### Nodes urls
* [Backend](https://mh-sssm.herokuapp.com/)
* [UI](https://mh-sssm-ui.herokuapp.com/)

### Github repositories
* [MH_SSSM_UI](https://github.com/herreramaxi/MH_SSSM_UI) 
* [MH_SSSM](https://github.com/herreramaxi/MH_SSSM) 

### Stack
- ##### Backend
    - .NET Core 3.1
    - Docker
    - WebApi
    - NUnit
    - Log4Net
- ##### Frontend
    - React 17.0.2
    - mui/material
    - bootstrap
    - react-apexcharts
- ##### Cloud
    - Heroku
