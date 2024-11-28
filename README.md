# Currency Exchange Rates App

This project consists of a frontend React app (`exchangeRatesClient`) and a backend ASP.NET C# API (`exchangeRatesServer`) that provides currency exchange rate data. 
The frontend allows users to select a base currency and displays the exchange rates for other currencies in a sortable and searchable table.

## Features

- **Frontend (React)**:
  - A dropdown with the following currencies: USD, EUR, GBP, CNY, ILS.
  - A table that displays the exchange rates for the selected base currency. The table is sortable and searchable, allowing users to sort and filter the exchange rates by any column.
  - The exchange rates are fetched from the backend API.

- **Backend (ASP.NET C#)**:
  - An API that returns a list of available currencies.
  - An API that returns the exchange rates for a given base currency.
  - **Two modes of data retrieval:**
    - **Fake Data (Random Data)**: The server returns random exchange rates data.
    - **Real Data from External API**: The server fetches real-time exchange rate data from an external API to provide accurate rates.

