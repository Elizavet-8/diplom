'use strict';

var Promise = require('bluebird');
var MongoClient = require('mongodb').MongoClient;
var PARKING_SPOTS_NUMBER = 10;

MongoClient.connect('mongodb://localhost:27017/sp_db').then(function (database) {
    var allPromises = [];
    allPromises.push(database.listCollections({name: 'parking'}).toArray().then(function(items) {
        if (items.length !== 1) {
            return database.createCollection('parking').then(function (collection) {
                var parkingSpots = [];
                for (var i = 0; i < PARKING_SPOTS_NUMBER; i++) {
                    parkingSpots.push({id: i, name: 'A' + i, reserved: false, occupiedByVideo: false, occupiedBySensors: false});
                }
                return collection.insertMany(parkingSpots);
            }).catch(function (err) {
                console.error('Error while creating parking collection. Reason: ', err);
            });
        }
    }).catch(function(err) {
        console.error(err)
    }));

    allPromises.push(database.listCollections({name: 'users'}).toArray().then(function(items) {
        if (items.length !== 1) {
            return database.createCollection('users').then(function(collection) {
                return collection.insertOne({name: 'admin', password: '$2a$10$vtBSbbjL5x7QqDzliv43vubV5luzGyg/pKxxK9qprTMcqtatnNPDy'});
            }).catch(function(err) {
                console.error('Error while creating users collection. Reason: ', err);
            });
        }
    }).catch(function(err) {
        console.error(err)
    }));

    return Promise.all(allPromises)
        .then(function (collections) {
            console.log('Database check was completed successfully');
            database.close();
        })
        .catch(function (err) {
            console.error(err);
        });
}).catch(function (err) {
    console.error('Error while connecting to database sp_db. Reason: ', err);
});

console.log('Bootstrapping db...');
