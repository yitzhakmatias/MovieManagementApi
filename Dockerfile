# Use the official image as a base image (we're using a version of .NET 6 here)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MovieManagementApi/MovieManagementApi.csproj", "MovieManagementApi/"]
RUN dotnet restore "MovieManagementApi/MovieManagementApi.csproj"
COPY . .
WORKDIR "/src/MovieManagementApi"
RUN dotnet build "MovieManagementApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieManagementApi.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieManagementApi.dll"]
