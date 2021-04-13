## SETUP:

1. Clone repo
2. Go to Api folder: `cd ./api`
3. Setup GCP Service account: `gcloud auth application-default login`
4. Setup API Secret: `dotnet user-secrets set "MarketGuruConfigurations:ApiKey" "12345" --project MarketGuruApi`
5. Test API: `dotnet test`
6. Run API: `dotnet run --project MarketGuruApi`

Make sure to update CORS settings if changing port or prod client url (without trailing / )
