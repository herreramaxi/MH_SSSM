# Super Simple Stock Market by Maxi Herrera

### Web app on the cloud
[https://mh-sssm-ui.herokuapp.com](https://mh-sssm-ui.herokuapp.com)

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

### Architecture
<img src="https://github.com/herreramaxi/MH_SSSM/blob/main/resources/Architecture.png" width="900" height="300">

### Assumptions

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
