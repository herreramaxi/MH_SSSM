# Super Simple Stock Market by Maxi Herrera
- [Requirements](#Requirements)
- [Solution](#Solution)
- [Assumptions](#Assumptions)
- [Running the solution from cloud](#Running-the-solution-from-cloud)
- [How to run the test cases](#How-to-run-the-test-cases)
- [Test cases](#Test-cases)
- [How to run the solution locally](#How-to-run-the-solution-locally)
  * [Backend - Web API](#Backend---Web API)
  * [UI](#UI)
- [Architecture](#Architecture)
- [UI repository](#UI-repository)
- [Stack](#Stack)

## Requirements
[Requirements.md](https://github.com/herreramaxi/MH_SSSM/blob/main/resources/Requirements.md)

## Solution
My proposal solution is a distributed cloud application composed by two nodes, one for the UI and the other for the backend. The frontend node is the customer facing UI implemented with React as a single page application (SPA). The second node is the backend, which has two critical missions: 
 * To provide its calculation services to frontend node through its Web API. 
 * To validates the calculations required by a NUnit test project.

### Assumptions
* Last price: Rather than sending the price and stock symbol to calculate “dividend yield” and “P/E Ratio”, the application requires at least to perfrom one trade for a given stock. This is because the last trade’s price is taken as a last price.
* P/E Ratio: As the formula does not specify if the “Dividend” on its denominator is the “Last Dividend” or “Dividend Yield”. I assumed that the correct approach would be to use the “Last dividend” so that the resulting formula would be: DY = Price/Last Dividend.
* GBCE All Share Index: I followed same approach as “Volume Weighted Stock Price” so that the index is based on trades in past 15 minutes to limit the volume of data.
To calculate the stock price, which is used on geometric mean, I am applying the "Volume Weighted Stock Price" for the selected stock. After that, the geometric mean is applied for every stock price previously calculated.

### Running the solution from cloud
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

### How to run the test cases
1. Download or clone repository
2. Open the Visual Studio solution [SSSM.sln](https://github.com/herreramaxi/MH_SSSM/blob/main/SSSM.sln) (NB: I used VS 2019)
3. Click on "Test/Run All Tests"

<img src="https://github.com/herreramaxi/MH_SSSM/blob/main/resources/TestsRunAll.JPG" alt="TestsRunAll" width="600" height="180">

### Test cases
- [DividendYieldUnitTests.cs](https://github.com/herreramaxi/MH_SSSM/blob/main/SSSM.Test/DividendYieldUnitTests.cs) 
 - [PERatioUnitTests.cs](https://github.com/herreramaxi/MH_SSSM/blob/main/SSSM.Test/PERatioUnitTests.cs)
 - [GetVolumeWeightedStockPriceUnitTests.cs](https://github.com/herreramaxi/MH_SSSM/blob/main/SSSM.Test/GetVolumeWeightedStockPriceUnitTests.cs)
 - [GBCEAllShareIndexUnitTests.cs](https://github.com/herreramaxi/MH_SSSM/blob/main/SSSM.Test/GBCEAllShareIndexUnitTests.cs)

### How to run the solution locally

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
* UI will load and display the [Dashboard](https://github.com/herreramaxi/MH_SSSM/#dashboard)

### Architecture
<img src="https://github.com/herreramaxi/MH_SSSM/blob/main/resources/Architecture.png" width="900" height="300">

### UI repository
[MH_SSSM_UI](https://github.com/herreramaxi/MH_SSSM_UI) 

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
