#!/bin/bash

docker build . -f API.Dockerfile -t thhbasta.azurecr.io/api:1
docker build . -f Entities.Dockerfile -t thhbasta.azurecr.io/entities:1
docker build . -f Cleanup.Dockerfile -t thhbasta.azurecr.io/cleanup:1

az acr login -n thhbasta

docker push thhbasta.azurecr.io/api:1
docker push thhbasta.azurecr.io/entities:1
docker push thhbasta.azurecr.io/cleanup:1
