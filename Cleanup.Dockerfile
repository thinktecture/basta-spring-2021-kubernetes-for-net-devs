FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY src/Thinktecture.Samples.BASTA.Configuration/Thinktecture.Samples.BASTA.Configuration.csproj ./Thinktecture.Samples.BASTA.Configuration/
COPY src/Thinktecture.Samples.BASTA.Entities/Thinktecture.Samples.BASTA.Entities.csproj ./Thinktecture.Samples.BASTA.Entities/
COPY src/Thinktecture.Samples.BASTA.Jobs.Cleanup/Thinktecture.Samples.BASTA.Jobs.Cleanup.csproj ./Thinktecture.Samples.BASTA.Jobs.Cleanup/

RUN dotnet restore "./Thinktecture.Samples.BASTA.Jobs.Cleanup/Thinktecture.Samples.BASTA.Jobs.Cleanup.csproj"
COPY ./src/Thinktecture.Samples.BASTA.Configuration/ ./Thinktecture.Samples.BASTA.Configuration/
COPY ./src/Thinktecture.Samples.BASTA.Entities/ ./Thinktecture.Samples.BASTA.Entities/
COPY ./src/Thinktecture.Samples.BASTA.Jobs.Cleanup/ ./Thinktecture.Samples.BASTA.Jobs.Cleanup/

WORKDIR "/src/Thinktecture.Samples.BASTA.Jobs.Cleanup"
RUN dotnet build "Thinktecture.Samples.BASTA.Jobs.Cleanup.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Thinktecture.Samples.BASTA.Jobs.Cleanup.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Thinktecture.Samples.BASTA.Jobs.Cleanup.dll"]
