#!/bin/bash

echo "To delete the desired resources, provide the same initials as provided during setup"
read -p "Please provide your intials:" initials

echo "All services will be deleted in the following account"

az account show --query "name" -o tsv

read -p "Hit [RETURN] to continue ([CTRL]+C to cancel)" cont

rgName="rg-basta-"$initials

az group delete -n $rgName --no-wait --yes
