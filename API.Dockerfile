FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY src/Thinktecture.Samples.BASTA.Configuration/Thinktecture.Samples.BASTA.Configuration.csproj ./Thinktecture.Samples.BASTA.Configuration/
COPY src/Thinktecture.Samples.BASTA.Entities/Thinktecture.Samples.BASTA.Entities.csproj ./Thinktecture.Samples.BASTA.Entities/
COPY src/Thinktecture.Samples.BASTA.WebAPI/Thinktecture.Samples.BASTA.WebAPI.csproj ./Thinktecture.Samples.BASTA.WebAPI/

RUN dotnet restore "./Thinktecture.Samples.BASTA.WebAPI/Thinktecture.Samples.BASTA.WebAPI.csproj"
COPY ./src/Thinktecture.Samples.BASTA.Configuration/ ./Thinktecture.Samples.BASTA.Configuration/
COPY ./src/Thinktecture.Samples.BASTA.Entities/ ./Thinktecture.Samples.BASTA.Entities/
COPY ./src/Thinktecture.Samples.BASTA.WebAPI/ ./Thinktecture.Samples.BASTA.WebAPI/

WORKDIR "/src/Thinktecture.Samples.BASTA.WebAPI"
RUN dotnet build "Thinktecture.Samples.BASTA.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Thinktecture.Samples.BASTA.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Thinktecture.Samples.BASTA.WebAPI.dll"]
