var router = require('express').Router();
var mongo = require('./mongo');

router.get('/', function (req, res) {
    if (!req.signedCookies || !req.signedCookies.phrase) {
        return;
    }
    mongo.getMongoCollection('parking')
        .then(function (collection) {
            collection.map(function (item, index) {
                var currentTime = new Date();
                if (item.time && (currentTime - item.time >= 15000)) {
                    mongo.updateMongoCollection('parking', {id: item.id}, {
                        $set: {
                            'reserved': false,
                            'userName': null,
                            'time': null
                        }
                    }).then(function () {
                        item.userName = null;
                        item.reserved = false;
                        item.time = null;
                    })
                        .catch(function (err) {
                            res.status(500);
                            res.send(err);
                        });
                }
                if (item.userName === req.signedCookies.phrase) {
                    item.currentUserReservation = true;
                    item.remainingTime = item.time ? (15000 - (new Date() - new Date(item.time))) : null;
                }
            });
            res.status(200);
            res.send(collection);
        })
        .catch(function (err) {
            res.status(500);
            res.send(err);
        });
});
router.post('/', function (req, res) {
    if (!req.signedCookies || !req.signedCookies.phrase) {
        return;
    }
    var id = req.body.id;
    mongo.canUserReserve(req.signedCookies.phrase).then(function () {
        mongo.getDocumentFromCollectionByCustomId('parking', id)
            .then(function (document) {
                if (document.reserved === true) {
                    res.status(403);
                    res.send('Spot is already reserved');
                    return;
                }
                if (document.occupiedBySensors === true || document.occupiedByVideo === true) {
                    res.status(403);
                    res.send('Spot is already occupied');
                    return;
                }
                var time = new Date();
                mongo.updateMongoCollection('parking', {id: id}, {
                    $set: {
                        'reserved': true,
                        'userName': req.signedCookies.phrase,
                        'time': time
                    }
                })
                    .then(function () {
                        res.status(200);
                        res.send('Reserved a spot');
                    })
                    .catch(function (err) {
                        res.status(500);
                        res.send(err);
                    });
            })
            .catch(function (err) {
                res.status(500);
                res.send(err);
            });
    }).catch(function (err) {
        res.status(403);
        res.send(err);
    });
});

function autoReserveHandler(req, res, currentSpot) {
    mongo.findFreeSpot().then(function (spot) {
        if (spot) {
            //нашли свободное место
            currentSpot.spotId = spot.id;
            var time = new Date();
            mongo.updateMongoCollection('parking', {id: spot.id}, {
                $set: {
                    'reserved': true,
                    'userName': currentSpot.userName,
                    'licensePlate': currentSpot.licensePlate,
                    'time': time
                }
            })
                .then(function () {
                    res.status(200);
                    res.send(currentSpot);
                })
                .catch(function (err) {
                    res.status(500);
                    res.send(err);
                });
        } else {
            //не нашли свободное место
            res.status(424);
            res.send('No free spots on parking, sorry. Please try again later');
        }
    }).catch(function (err) {
        res.status(500);
        res.send(err);
    })
}

router.post('/auto', function (req, res) {
    if (!req.body.plateNumber && !req.signedCookies && !req.signedCookies.phrase) {
        res.status(400);
        res.send('No plate number or user name provided');
        return;
    }
    if (req.body.plateNumber) {
        var currentSpot;
        //в запросе есть распознанный номер автомобиля
        var plateNumber = req.body.plateNumber;
        mongo.isExistingUserByPlate(plateNumber).then(function (user) {
            currentSpot = {licensePlate: plateNumber};
            if (user) {
                //нашли пользователя с таким номером
                currentSpot.userName = user.name;
                mongo.getReservedPlaceByUserName(user.name).then(function (place) {
                    if (place) {
                        //нашли зарезервированное место у данного пользователя
                        var time = new Date();
                        mongo.updateMongoCollection('parking', {id: place.id}, {
                            $set: {
                                'reserved': true,
                                'userName': user.name,
                                'licensePlate': currentSpot.licensePlate,
                                'time': time
                            }
                        }).then(function () {
                            res.status(200);
                            res.send('User arrived to parking');
                        })
                            .catch(function (err) {
                                res.status(500);
                                res.send(err);
                            });
                    }
                    else {
                        //не нашли зарезервированное место у данного пользователя
                        autoReserveHandler(req, res, currentSpot);
                    }
                }).catch(function (err) {
                    res.status(500);
                    res.send(err);
                })
            } else {
                //не нашли пользователя с таким номером, значит просто занимаем место по номеру автомобиля
                currentSpot.userName = null;
                autoReserveHandler(req, res, currentSpot);
            }
        }).catch(function (err) {
            res.status(500);
            res.send(err);
        });
    } else {
        currentSpot = {
            userName: req.signedCookies.phrase,
            licensePlate: null
        };
        mongo.canUserReserve(req.signedCookies.phrase).then(function () {
            autoReserveHandler(req, res, currentSpot);
        })
            .catch(function (err) {
                res.status(403);
                res.send(err);
            });
    }
});

module.exports = router;