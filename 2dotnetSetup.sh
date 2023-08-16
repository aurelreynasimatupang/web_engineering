#!/bin/bash


#Assume we are running on linux
#We are setting up the dotnet

cd backend-dotnetcore/

sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-6.0
  
  sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y aspnetcore-runtime-6.0
  
sudo dotnet add package Newtonsoft.Json
sudo dotnet watch run


#END
