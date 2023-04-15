# Start with the .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /SimpleApi

# Copy the project files and restore dependencies
COPY SimpleApi/*.csproj .

RUN dotnet restore

# Copy the rest of the project files and build the application
COPY . ./

RUN dotnet publish -c Release -o out

# Create a new image with only the runtime and the application
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /SimpleApi
COPY --from=build /SimpleApi/out .

# Set the environment variables for the database connection
ENV ConnectionStrings__DefaultConnection="Server=db;Database=empresas;User Id=sa;Password=empresas123"

# Expose the port the application is listening on
EXPOSE 5000 5001

# Start the application
ENTRYPOINT ["dotnet", "SimpleApi.dll"]