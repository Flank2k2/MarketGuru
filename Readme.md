# Market Guru Application

## Proposed solution:

This is an "enterprizey" solution to the problem.  
UI: https://flank2k2.github.io/MarketGuru/.  
Api: https://marketguru-api-3tfjt4473a-uc.a.run.app/swagger/index.html.   

Due to the choice of financial data vendor [AlphaVantage](https://www.alphavantage.co/documentation/) we can only do about 1 stock quote per minute.   
We are limited to 5 api calls per minute and the data service will do the following GET:
  -  To retrieve the full name of the company.
  -  To retrieve the daily price information.
  -  To retrieve the monthly historical data.

The codebase is separated in 2 part:
  - The backend api (`/api`), implemented using net5.0, comprised of 3 projects
    - MarketGuru.Core: Core services and domain models
    - MarketGuru.Data.Firestore: Repository to store API Results
    - MarketGuruApi: aps.net core web api.
  - The front-end UI (`/ui`), implemented using Vue.js
    - Vue components: StockInformation, StockPriceTable.
    - Vuex Store for data sync.

## Assumptions:

 - Given a stock quote, the service will retrieve the high, low and closing price of the previous day.
 - The API retrieve stock history on a monthly basis.
 - The API will look at the history over a period and make a recommendation:
    - Sell if price difference between start and end period is below a set threshold.
    - Buy if price difference between start and end period is over a set threshold.
    - No recommendation is the volume of trading is below a set threshold.
 - The period is not a defined set of days, but the last 3 data points returned by the API. 
    - As a result, the period is between 30 and 60 days depending on the day of the current month (Modulo opening trading days...). 
 - The yearly trend is the chart of the January price for the past 5 years.

### Backend API point of interest:

- To accommodate for financial API rate limit, the data service ([IStockDataService](https://github.com/Flank2k2/MarketGuru/blob/main/api/MarketGuru.Core/Services/StockDataService.cs)) uses an in-memory cache for past queries.
- The recommendation service ([IStockRecommendationService](https://github.com/Flank2k2/MarketGuru/blob/main/api/MarketGuru.Core/Services/StockRecommendationService.cs)) logic is driven through configurations.
- The API is using FirestoreDb to store api recommendations [IStockRecommendationRepository](https://github.com/Flank2k2/MarketGuru/blob/main/api/MarketGuru.Data/Repository/StockRecommendationRepository.cs)
- Service Unit tests can be found in project: [MarketGuru.Core.Tests](https://github.com/Flank2k2/MarketGuru/tree/main/api/MarketGuru.Core.Tests)
- Integration tests can be found in project: [MarketGuruApi.Tests](https://github.com/Flank2k2/MarketGuru/tree/main/api/MarketGuruApi.Tests)
- Api implements healthcheck endpoint: [/healthx](https://marketguru-api-3tfjt4473a-uc.a.run.app/healthz)
- Api prodide OpenAPI endpoint: [/swagger](https://marketguru-api-3tfjt4473a-uc.a.run.app/swagger/index.html)
- Api is deployed to GCP Cloud Run on commits to `main` branch [deploy-cloud-run](https://github.com/Flank2k2/MarketGuru/blob/main/.github/workflows/deploy-cloud-run.yml)  
- No secrets are stored in plaintext.
- Separate cloud service accounts are used to run tests and deploy the app.

### Front-end point of interest:

- Main App page: [App.vue](https://github.com/Flank2k2/MarketGuru/blob/main/ui/src/App.vue)
- Vuex store for data reactivity [index.js](https://github.com/Flank2k2/MarketGuru/blob/main/ui/src/store/index.js)
- UI Components are located at: [StockInformation.vue](https://github.com/Flank2k2/MarketGuru/blob/main/ui/src/components/StockInformation.vue) and [StockPriceTable.vue](https://github.com/Flank2k2/MarketGuru/blob/main/ui/src/components/StockPriceTable.vue)
- UI is deployed to Github page: [https://flank2k2.github.io/MarketGuru/](https://flank2k2.github.io/MarketGuru/) on commits to `main` branch [deploy-gh-page](https://github.com/Flank2k2/MarketGuru/blob/main/.github/workflows/deploy-gh-page.yml)

## Area of improvements

1. **Script IAM / cloud deployment:**.   
The initial cloud run and accounts setup was done manually. This is not a reproducible nor documented process.
2. **Separate Domain entities and DTO:**.   
The API serialize the domain entities in the reponse. This is good enough for now but we will need to create Dto / response records at some point.
4. **Properly handle API rate limiting.**.  
 The financial API library will throw an exception when the API hit the rate limit. (The vendor does not return a HTTP 429). 
 We either need to re-implement the HttpClient to handle this situation OR add our own rate limiting within the API. 
5. **Improve secret management:**    
The API secret is an environment variable setup in Cloud Run. A more secure way would be to implement an IConfigurationProvider and read the secrets from a KMS.
6. **Complete repository abstraction**.   
The configuration and entities of the data layer are strongly tied to the storage implementation (FirestoreDb). We could refine the design to ease new storage implementation.
7. **Add authentication**
8. **Add end to end testing of UI:**
9. **Finish Unit Testing of Core services**
