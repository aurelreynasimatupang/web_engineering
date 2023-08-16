#!/bin/bash


#Assume we are running on linux
#We are setting up maria db via docker-compose
#Make sure the port 3006 is not in use
#Assuming mariadb is already running, run on linux "sudo systemctl stop mariadb"

sudo docker-compose up mariadb



#END
