import EventEmitter from 'events';
var CURRENT_USER_RESERVED_SPOT = 'CURRENT_USER_RESERVED_SPOT';
var CONTENT_CHANGE = 'CONTENT_CHANGE';
var RESERVED_SPOT_CHANGE = 'RESERVED_SPOT_CHANGE';

var ParkingSpotsStore = Object.assign({}, EventEmitter.prototype, {
    content: null,
    reservedSpotId: null,
    wasSpotReserved: false,

    emitCurrentUserSpotReserve: function () {
        this.emit(CURRENT_USER_RESERVED_SPOT);
    },

    emitContentChange: function () {
        this.emit(CONTENT_CHANGE);
    },

    isLoaded: function () {
        return !!this.content;
    },

    /**
     * @param {function} callback
     */
    addCurrentUserSpotReserveListener: function (callback) {
        this.on(CURRENT_USER_RESERVED_SPOT, callback);
    },

    /**
     * @param {function} callback
     */
    addContentChangeListener: function (callback) {
        this.on(CONTENT_CHANGE, callback);
    },

    /**
     * @param {function} callback
     */
    addReserveListener: function (callback) {
        this.on(RESERVED_SPOT_CHANGE, callback);
    },

    reserveParkingSpot: function (id) {
        if (id !== null) {
            if (!this.content || this.content.length <= 0) {
                return;
            }
            this.reservedSpotId = +id;
            this.emitReserve();
            // this.emitContentChange();
        }
    },

    emitReserve: function() {
        this.emit(RESERVED_SPOT_CHANGE);
    },

    rewriteContent: function (data) {
        if (data) {
            let newReservedSpotId = null;
            let didThisUserReserve = false;
            for (let index = 0; index < data.length; ++index) {
                if (data[index].currentUserReservation === true) {
                    newReservedSpotId = index;
                    didThisUserReserve = true;
                    break;
                }
            }
            this.toggleSpotReservationBlocking(didThisUserReserve);
            this.content = data;
            if (newReservedSpotId !== this.reservedSpotId) {
                this.reservedSpotId = newReservedSpotId;
                this.emitReserve();
            }
            this.emitContentChange();
        }
    },

    toggleSpotReservationBlocking: function (wasSpotReserved) {
        if (wasSpotReserved !== this.wasSpotReserved) {
            this.wasSpotReserved = wasSpotReserved;
            this.emitCurrentUserSpotReserve();
        }
    },

    /**
     * @param {function} callback
     */
    removeCurrentUserSpotReserveListener: function (callback) {
        this.removeListener(CURRENT_USER_RESERVED_SPOT, callback);
    },

    /**
     * @param {function} callback
     */
    removeContentChangeListener: function (callback) {
        this.removeListener(CONTENT_CHANGE, callback);
    },

    /**
     * @param {function} callback
     */
    removeReserveListener: function (callback) {
        this.removeListener(RESERVED_SPOT_CHANGE, callback);
    }
});

export default ParkingSpotsStore;