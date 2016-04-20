#!/usr/bin/env python

# Filename: estimator.py
# Description: Given estimator object, this script listens a tcp port, when it receives a play data, it predicts its difficulty and responds back.
#	Script ends when connection is closed by other side of communication
# Author: Selcuk Gulcan
# Date created: 20.04.2016
# Date last modified: 20/04/2016
# Python Version: 2.7

import socket
import numpy as np
from sklearn.ensemble import RandomForestRegressor
import pickle

estimator_object_file = "estimator.obj"
with open( estimator_object_file, 'rb') as input_file:
	estimator = pickle.load(input_file)

TCP_IP = '127.0.0.1'
TCP_PORT = 5005
BUFFER_SIZE = 1024

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.bind((TCP_IP, TCP_PORT))
s.listen(1)

conn, addr = s.accept()
print 'Connection address:', addr
while 1:
	data = conn.recv(BUFFER_SIZE)
	if not data: break
	print "Estimate request received"
	features = map( float, data.split())
	features_dup = list()
	features_dup.append( features)
	features_dup.append( features)
	result = estimator.predict( features_dup)[0]
	print "Estimated Difficulty:", result
	conn.send( str(result))
conn.close()