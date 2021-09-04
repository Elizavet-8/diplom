var MongoClient = require('mongodb').MongoClient;
var db;
var Promise = require('bluebird');

// Initialize connection once
function connect() {
    var deferred = Promise.pending();
    MongoClient.connect("mongodb://localhost:27017/sp_db", function (err, database) {
        if (err) deferred.reject(err);
        else {
            db = database;
            // clearAllCounters();
            deferred.resolve();
        }
    });
    return deferred.promise;
}

function getUserPassword(userName) {
    var deferred = Promise.pending();
    getDocumentFromCollectionByCustomName('users', userName).then(function (collection) {
        deferred.resolve(collection.password);
    });
    return deferred.promise;
}
function getAdminPassword() {
    var deferred = Promise.pending();
    getMongoCollection('passwords').then(function (collection) {
        deferred.resolve(collection[0].admin_password);
    });
    return deferred.promise;
}

/**
 * Функция для обновления значения поля заданной таблицы
 * @function
 * @param {string} tableName - Название mongoDB коллекции
 * @param {object} filter - Фильтр, по которому выбирается документ из tableName-коллекции.
 * @param {object} data - Update-object в формате mongo. Например: {$set: {"errors.rx: 1"}}
 * @return {object} Promise
 */
function updateMongoCollection(tableName, filter, data) {
    var deferred = Promise.pending();
    var table = db.collection(tableName);
    if (filter && Object.keys(filter)) {
        (Object.keys(filter) && Object.keys(filter).length > 0) ?
            table.updateOne(filter, data).then(
                function () {
                    return deferred.resolve();
                }).catch(
                function () {
                    return deferred.reject();
                }) :
            table.updateMany(filter, data).then(function () {
                return deferred.resolve();
            }).catch(
                function () {
                    return deferred.reject();
                });
    }
    else {
        table.updateMany({}, data).then(function () {
            return deferred.resolve();
        }).catch(
            function () {
                return deferred.reject();
            });
    }
    return deferred.promise;
}

function getDocumentFromCollectionByCustomName(tableName, name) {
    var document = null;
    var deferred = Promise.pending();
    var cursor = db.collection(tableName).find({name: name}).limit(1);
    cursor.forEach(function (doc) {
        if (doc !== null) {
            document = doc;
        }
    }, function (err) {
        if (err) {
            console.log("Error in getting mongo collection ", err);
            deferred.reject(err);
        }
        else {
            deferred.resolve(document);
        }
    });

    return deferred.promise;
}
function findFreeSpot() {
    var document = null;
    var deferred = Promise.pending();
    var cursor = db.collection('parking').find({occupiedByVideo: false, occupiedBySensors: false, reserved: false}).limit(1);
    cursor.forEach(function (doc) {
        if (doc !== null) {
            document = doc;
        }
    }, function (err) {
        if (err) {
            console.log("Error in getting mongo collection ", err);
            deferred.reject(err);
        }
        else {
            deferred.resolve(document);
        }
    });

    return deferred.promise;
}
function getDocumentFromCollectionByCustomId(tableName, id) {
    var document = null;
    var deferred = Promise.pending();
    var cursor = db.collection(tableName).find({id: id}).limit(1);
    cursor.forEach(function (doc) {
        if (doc !== null) {
            document = doc;
        }
    }, function (err) {
        if (err) {
            console.log("Error in getting mongo collection ", err);
            deferred.reject(err);
        }
        else {
            deferred.resolve(document);
        }
    });

    return deferred.promise;
}
/**
 * Функция для получения коллекции MongoDB
 * @function
 * @param {string} tableName - Название коллекции.
 * @return {Object} Promise
 */
function getMongoCollection(tableName) {
    var collection = [];
    var deferred = Promise.pending();
    var cursor = db.collection(tableName).find();
    cursor.forEach(function (doc) {
        if (doc !== null) {
            collection.push(doc);
        }
    }, function (err) {
        if (err) {
            console.log("Error in getting mongo collection ", err);
            deferred.reject(err);
        }
        else {
            deferred.resolve(collection);
        }
    });

    return deferred.promise;
}
function isNotExistingUser(tableName, userName) {
    var deferred = Promise.pending();
    var cursor = db.collection(tableName).find({name: {$exists: true, $eq: userName}});
    cursor.forEach(function (doc) {
        if (doc !== null) {
            deferred.reject('User already exists');
        }
    }, function (err) {
        if (err) {
            console.log("Error in getting mongo collection ", err);
            deferred.reject(err);
        }
        else {
            deferred.resolve();
        }
    });
    return deferred.promise;
}
function isExistingUserByPlate(plateNumber) {
    var deferred = Promise.pending();
    var cursor = db.collection('users').find({licensePlate: {$exists: true, $eq: plateNumber}});
    cursor.forEach(function (doc) {
        if (doc !== null) {
            deferred.resolve(doc);
        }
    }, function (err) {
        if (err) {
            console.log("Error in getting mongo collection ", err);
            deferred.reject(err);
        } else {
            deferred.resolve(null);
        }
    });
    return deferred.promise;
}
function canUserReserve(userName) {
    var deferred = Promise.pending();
    var cursor = db.collection('parking').find({userName: {$exists: true, $eq: userName}});
    cursor.forEach(function (doc) {
        if (doc !== null) {
            deferred.reject('Current user has already reserved a place');
        }
    }, function (err) {
        if (err) {
            console.log("Error in getting mongo collection ", err);
            deferred.reject(err);
        }
        else {
            deferred.resolve();
        }
    });
    return deferred.promise;
}
function getReservedPlaceByUserName(userName) {
    var deferred = Promise.pending();
    var cursor = db.collection('parking').find({userName: userName}).limit(1);
    var document = null;
    cursor.forEach(function (doc) {
        if (doc !== null) {
            document = doc;
        }
    }, function (err) {
        if (err) {
            console.log("Error in getting mongo collection ", err);
            deferred.reject(err);
        }
        else {
            deferred.resolve(document);
        }
    });
    return deferred.promise;
}
function insertRow(tableName, data) {
    var deferred = Promise.pending();
    db.collection(tableName).insertOne(data)
        .then(deferred.resolve())
        .catch(function (err) {
            deferred.reject(err)
        });
    return deferred.promise;
}
/**
 * Полностью перезаписывает коллекцию документов MongoDB
 * @function
 * @param {string} tableName - Название коллекции.
 * @param {object || object[]} data - Монго-документ или массив монго-документов для сохранения.
 * @return {Object} Promise
 */
function rewriteMongoCollection(tableName, data) {
    var deferred = Promise.pending();
    db.collection(tableName).deleteMany({})
        .then(function () {
            if (Object.prototype.toString.call(data) === '[object Array]') {
                db.collection(tableName).insertMany(data)
                    .then(deferred.resolve())
                    .catch(function (err) {
                        deferred.reject(err)
                    })
            }
            else {
                db.collection(tableName).insertOne(data)
                    .then(deferred.resolve())
                    .catch(function (err) {
                        deferred.reject(err)
                    })
            }
        })
        .catch(function (err) {
            deferred.reject()
        });
    return deferred.promise;
}

exports.updateMongoCollection = updateMongoCollection;
exports.getMongoCollection = getMongoCollection;
exports.insertRow = insertRow;
exports.isNotExistingUser = isNotExistingUser;
exports.canUserReserve = canUserReserve;
exports.rewriteMongoCollection = rewriteMongoCollection;
exports.getDocumentFromCollectionByCustomId = getDocumentFromCollectionByCustomId;
exports.isExistingUserByPlate = isExistingUserByPlate;
exports.findFreeSpot = findFreeSpot;
exports.getReservedPlaceByUserName = getReservedPlaceByUserName;
exports.getAdminPassword = getAdminPassword;
exports.getUserPassword = getUserPassword;
exports.connect = connect;