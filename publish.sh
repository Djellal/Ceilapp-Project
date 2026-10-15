#!/bin/bash

git pull
dotnet publish -c Release Ceilapp/
sudo service ceilapp stop
rsync -av /opt/ceilapp/Ceilapp-Project/Ceilapp/bin/Release/net10.0/publish/ /opt/ceilapp/publish/
sudo sleep 5 && sudo service ceilapp start
sudo service ceilapp status
