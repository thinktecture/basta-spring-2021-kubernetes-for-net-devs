#!/bin/bash

echo "To ensure globally unique service names, your initials will be appended to every Azure resource"
read -p "Please provide your intials:" initials

echo "All services will be created in the following account"

az account show --query "name" -o tsv

read -p "Hit [RETURN] to continue ([CTRL]+C to cancel)" cont

rgName="rg-basta-"$initials
location="westeurope"
acrName="basta2021"$initials
sqlServerName="basta2021"$initials
aksName="basta-2021-"$initials

echo "Crearing Resource Group " $rgName
az group create -n $rgName -l $location

echo "Creating Azure Container Registry " $acrName 
az acr create -n $acrName -l $location -g $rgName --sku Basic --admin-enabled false

echo "Create SQL Server " $sqlServerName
az sql server create -n $sqlServerName -l $location -g $rgName --admin-user Basta2021 --admin-password LoremIpsum@2021

echo "Create SQL Server Firewall rule for Azure Services" 
az sql server firewall-rule create --name allazure --server $sqlServerName -g $rgName --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0

echo "Create Azure SQL database"
az sql db create --server $sqlServerName -n sessions -g $rgName --service-objective S0

acrId=$(az acr show -n $acrName --query "id" -o tsv)

echo "Creating Azure Kubernetes Servivce " $aksName
az aks create -n $aksName -g $rgName --location $location -c 1 --enable-managed-identity --attach-acr $acrId
