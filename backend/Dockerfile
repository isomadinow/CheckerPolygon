FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ./backend/backend.csproj ./
RUN dotnet restore "./backend.csproj"

COPY ./backend ./
RUN dotnet publish "./backend.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000

ENTRYPOINT ["dotnet", "backend.dll"]
