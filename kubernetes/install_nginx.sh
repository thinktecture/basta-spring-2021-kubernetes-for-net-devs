#!/bin/bash

echo "Ensure that Helm.sh (Helm 3) is installed locally and your kubectl points to the desired cluster"
read -p "Hit [RETURN] to continue ..." cont

helm install nginx-ingress ingress-nginx/ingress-nginx -n nginx
