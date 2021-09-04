'use strict';

var Promise = require('bluebird');
var MongoClient = require('mongodb').MongoClient;
var PARKING_SPOTS_NUMBER = 10;

MongoClient.connect('mongodb://localhost:27017/sp_db').then(function (database) {
    database.dropDatabase('sp_db').then(function () {
        database.close();
    }).catch(function (err) {
        console.error('Error while dropping database sp_db. Reason: ', err);
    });
}).catch(function (err) {
    console.error('Error while connecting to database sp_db. Reason: ', err);
});

MongoClient.connect('mongodb://localhost:27017/sp_db').then(function (database) {
    var allPromises = [];
    allPromises.push(database.createCollection('parking').then(function (collection) {
        var parkingSpots = [];
        for (var i = 0; i < PARKING_SPOTS_NUMBER; i++) {
            parkingSpots.push({id: i, name: 'A' + i, reserved: false, occupiedByVideo: false, occupiedBySensors: false});
        }
        return allPromises.push(collection.insertMany(parkingSpots));
    }).catch(function (err) {
        console.error('Error while creating parking collection. Reason: ', err);
    }));
    allPromises.push(database.createCollection('users').then(function(collection) {
        return collection.insertOne({name: 'admin', password: '$2a$10$vtBSbbjL5x7QqDzliv43vubV5luzGyg/pKxxK9qprTMcqtatnNPDy'});
    }));
    return Promise.all(allPromises)
        .then(function (collections) {
            console.log('Database reset was completed successfully');
            database.close();
        })
        .catch(function (err) {
            console.error(err);
        });
}).catch(function (err) {
    console.error('Error while creating database sp_db. Reason: ', err);
});

console.log('Resetting db...');

