FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*


WORKDIR /src
COPY ["Mendel.Web/Mendel.Web.csproj", "Mendel.Web/"]
RUN dotnet restore "./Mendel.Web/Mendel.Web.csproj"
COPY . .
WORKDIR "/src/Mendel.Web"
RUN dotnet build "Mendel.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mendel.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mendel.Web.dll"]