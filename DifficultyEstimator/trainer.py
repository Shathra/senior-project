#!/usr/bin/env python

# Filename: trainer.py
# Description: Trains data in input_dir folder and save result estimator object to estimator_object_file
# Author: Selcuk Gulcan
# Date created: 20.04.2016
# Date last modified: 20/04/2016
# Python Version: 2.7

import numpy as np
import os
from sklearn.ensemble import RandomForestRegressor
import pickle

input_dir = "train_data"

estimator_object_file = "estimator.obj"

train_file_count = 0

feature_list = []
value_list = []
for root, dirs, filenames in os.walk(input_dir):
	for input_file_name in filenames:
		with open(os.path.join(root, input_file_name),'r') as input_file:

			feature_list.append( list())

			for line in input_file:
				line = line.split(':')
				feature = line[0]
				value = line[1].split(';')[0]
				feature_list[train_file_count].append( value)

			value_list.append( feature_list[train_file_count][-1])
			feature_list[train_file_count].pop()
			train_file_count += 1

X = np.array( feature_list)
Y = np.array( value_list)
regressor = RandomForestRegressor(n_estimators=20)
regressor.fit(X,Y)
importances = regressor.feature_importances_
print importances

with open( estimator_object_file, 'wb') as output:
	pickle.dump(regressor, output, pickle.HIGHEST_PROTOCOL)

print "Total data :", train_file_count