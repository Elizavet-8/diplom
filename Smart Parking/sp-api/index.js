var express = require('express');
var route_session = require('./server_modules/route-session');
var route_registration = require('./server_modules/route-registration');
var app = express();
var mongo = require('./server_modules/mongo');
var bodyParser = require('body-parser');
var cookieParser = require('cookie-parser');
var route_parking = require('./server_modules/route-parking');
var path = require('path');

// Initialize connection once
mongo.connect().then(function () {
    app.listen(app.get('port'), function () {
        console.log('Express started on http://localhost:' +
            app.get('port') + '; press Ctrl-C to terminate.');
    });
})
    .catch(function (error) {
        console.error('error in connecting to MongoDB');
        throw error;
    });

var port = process.env.PORT || 8080;
app.set('port', port);
app.use(bodyParser.json()); // for parsing application/json
app.use(cookieParser("Look at my horse! My horse is amazing!"));
app.use('/api/session', route_session);
app.use('/api/registration', route_registration);
app.use('/api/parking', route_parking);
// Serve static assets
app.use('/release', express.static(__dirname + '/release'));

// Always return the main index.html, so react-router render the route in the client
app.get('*', function (req, res) {
    res.sendFile(path.resolve(__dirname, '.', 'release', 'index.html'));
});

// 404 catch-all handler (middleware)
app.use(function (req, res, next) {
    res.status(404);
});
// 500 error handler (middleware)
app.use(function (err, req, res, next) {
    console.error(err.stack);
    res.status(500);
});
