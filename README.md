# Movie Management API and Frontend Setup

## Project Overview:
This repository contains the back-end API and the front-end React application for the Movie Management system. The API provides functionalities to manage movies, actors, ratings, and to perform search operations. The front-end consumes this API to display the movie and actor details.

## Back-End Setup:

### Prerequisites:
1. .NET SDK (version 6 or higher)
2. Docker (for containerization)
3. SQL Server or SQLite for database management

### API Key Configuration:
1. The API secret key is stored in the application configuration, specifically in the `appsettings.json` or environment variables. The key is used for authentication on routes that modify data.

### Running the API:
1. Clone the repository:
   ```bash
   git clone https://github.com/yitzhakmatias/MovieManagementApi.git

    Open the project folder and run the following command to restore the dependencies:

dotnet restore

To run the API locally, use the following command:

dotnet run

The API will be accessible at:

    http://localhost:5000

Docker Setup:

    The repository contains a Docker configuration to containerize the application.

    To build and run the API inside a Docker container:

    docker-compose up --build

    This will start the application, and you can access it at http://localhost:5000.

Front-End Setup:
Prerequisites:

    Node.js (version 16 or higher)
    npm (Node Package Manager)

Running the Front-End:

    Clone the front-end repository:

git clone https://github.com/yitzhakmatias/MovieManagementApp.git

Navigate to the project folder and install the dependencies:

cd MovieManagementApp
npm install

To start the front-end application, use:

npm start

The front-end will be available at:

    http://localhost:3000

Features:

    Movies Page: Displays a list of movies with details such as title, release year, genre, and ratings.
    Search Functionality: Allows searching for movies by name or actors by name.
    API Integration: The front-end makes API calls to fetch and display movie and actor details.

CORS Configuration:

    The back-end API allows cross-origin requests from http://localhost:3000, ensuring the React front-end can communicate with the API without restrictions.

Additional Information:

    API Documentation: You can access the Swagger UI for API documentation at http://localhost:5000/swagger.
    Environment Variables: Ensure you have the correct API_SECRET_KEY set in your environment variables or appsettings.json to interact with the API endpoints securely.
