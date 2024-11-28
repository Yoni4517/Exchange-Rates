
## Prerequisites

Before running the project, make sure you have the following installed:

- [Node.js](https://nodejs.org/) (version 14 or higher)
- [npm](https://www.npmjs.com/) (comes with Node.js)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet) (for the backend)
- [Visual Studio Code](https://code.visualstudio.com/) (recommended for development)
- [Git](https://git-scm.com/) (for version control)

## Setup Instructions

### 1. Clone the Repository

Clone the repository to your local machine:
```bash
git clone https://github.com/your-username/your-repository.git
cd your-repository
```

### 2. Setup Frontend (React)

Navigate to the exchangeRates/exchangeRatesClient directory and install the required dependencies:

```bash
cd exchangeRates/exchangeRatesClient
npm install
```

Once the installation is complete, start the frontend app:

```bash
npm run dev
```
This will start the frontend app on http://localhost:3000.

### 3. Setup Backend (ASP.NET C#)

Navigate to the exchangeRates/exchangeRatesServer directory and restore the dependencies:

```bash
cd exchangeRates/exchangeRatesServer
dotnet restore
```

To run the backend server:

```bash
dotnet run
```
This will start the backend API on http://localhost:5132.

### 4. Run the Application
After starting both the frontend and backend servers, navigate to http://localhost:3000 in your browser. You should see the React app with the dropdown and table displaying currency exchange rates.

### 5. Development Workflow
Frontend: The React app will hot reload whenever you make changes.
Backend: The ASP.NET C# API will also restart automatically when changes are made.

Folder Structure:

/exchangeRates
  /exchangeRatesClient      # React frontend app
  /exchangeRatesServer      # ASP.NET C# backend API
/README.md                 # This file
