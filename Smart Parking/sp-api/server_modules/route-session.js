var router = require('express').Router();
var getUserPassword = require('./mongo').getUserPassword;
var bCrypt = require('bcryptjs');

router.get('/', function(req, res) {
    if (!req.signedCookies.phrase) {
        res.status(403); res.send('Unauthorized');
    }
    else if (req.signedCookies.phrase) {
        res.status(200);
        res.send('welcome');
    }
});

router.post('/', function (req, res) {
    if (!req.body.password || !req.body.username) {res.status(400); res.send('Not enough auth info'); return;}
    getUserPassword(req.body.username).then(function (pass) {
        if (bCrypt.compareSync(req.body.password, pass) === true) {
            // var encryptedUserName = bCrypt.hashSync(req.body.username);
            res.cookie('phrase', req.body.username, {maxAge: 9000000, httpOnly: true, signed: true});
            res.status(200);
            res.send('welcome');
        }
        else {
            res.status(401);
            res.send('auth_info_incorrect');
        }
    }).catch(function (err) {
        res.status(500);
        res.send(err);
    });
});

router.delete('/', function (req, res) {
    res.clearCookie('phrase', null);
    res.status(200);
    res.send("Successfully logged out");
});

module.exports = router;