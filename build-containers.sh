#!/bin/bash
echo "This script will build and push all Docker images to Azure Container registry"

echo "Please provide the name of your ACR instance (just the prefix before .azurecr.io)"

read -p "ACR prefix: " acrPrefix

echo "Which tag should be created for the images?"
read -p "Image tag: " imageTag
docker build . -f API.Dockerfile -t $acrPrefix.azurecr.io/api:$imageTag
docker build . -f Entities.Dockerfile -t $acrPrefix.azurecr.io/entities:$imageTag
docker build . -f Cleanup.Dockerfile -t $acrPrefix.azurecr.io/cleanup:$imageTag

az acr login -n $acrPrefix

docker push $acrPrefix.azurecr.io/api:$imageTag
docker push $acrPrefix.azurecr.io/entities:$imageTag
docker push $acrPrefix.azurecr.io/cleanup:$imageTag
