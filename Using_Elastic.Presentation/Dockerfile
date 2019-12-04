FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Using_Elastic.Presentation/Using_Elastic.Presentation.csproj", "Using_Elastic.Presentation/"]
RUN dotnet restore "Using_Elastic.Presentation/Using_Elastic.Presentation.csproj"
COPY . .
WORKDIR "/src/Using_Elastic.Presentation"
RUN dotnet build "Using_Elastic.Presentation.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Using_Elastic.Presentation.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Using_Elastic.Presentation.dll"]