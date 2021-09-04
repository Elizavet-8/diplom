var router = require('express').Router();
var mongo = require('./mongo');
var bCrypt = require('bcryptjs');

router.post('/', function (req, res) {
	if (req.body.password && req.body.username && req.body.licensePlate) {
	    var encryptedPassword = bCrypt.hashSync(req.body.password);
        mongo.isNotExistingUser('users', req.body.username).then(function() {
            mongo.insertRow('users', {name: req.body.username, password: encryptedPassword, licensePlate: req.body.licensePlate}).then(function() {
                res.clearCookie('phrase', null);
                res.cookie('phrase', req.body.username, {maxAge: 9000000, httpOnly: true, signed: true});
                res.status(200);
                res.send('welcome');
            }).catch(function() {
                res.status(500);
                res.send('Internal server error');
            });
        }).catch(function(err) {
            res.status(500);
            res.send(err);
        });
	}
	else {
		res.status(400);
        res.send('Incorrect registration info');
	}
});

module.exports = router;