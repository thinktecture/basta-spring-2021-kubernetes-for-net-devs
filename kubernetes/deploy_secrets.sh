#!/bin/bash

echo "Ensure that your kubectl points to the desired cluster"
read -p "Hit [RETURN] to continue ..." cont

echo "Please provide your SQL Azure Connection String"
read -s -p "Connection String" cstr

kubectl create secret generic sql --from-literal ConnectionString="$cstr" -n live
