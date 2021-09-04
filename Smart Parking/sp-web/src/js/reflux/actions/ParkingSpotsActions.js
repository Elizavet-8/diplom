import axios from 'axios';
import ParkingSpotsStore from '../stores/ParkingSpotsStore';

let refreshId = null;
const ParkingSpotsActions = {
    /**
     * @function getParkingInfo
     * @return {void}
     * */
    getParkingInfo: () => {
        axios.get('/api/parking')
             .then((response) => {
                 ParkingSpotsStore.rewriteContent(response.data);
             })
             .catch((response) => {
                 //notify user about an error
                 console.error('Error while receiving response = ', response.data);
             });
    },

    /**
     * @function setParkingInfoPolling
     * @return {void}
     * */
    setParkingInfoPolling: () => {
        refreshId = setInterval(
            () => {
                axios.get('/api/parking')
                     .then((response) => {
                         ParkingSpotsStore.rewriteContent(response.data);
                     })
                     .catch((response) => {
                         //notify user about an error
                         console.error('Error while receiving response = ', response.data);
                     });
            }, 1000
        );
    },

    /**
     * @function stopParkingInfoPolling
     * @return {void}
     * */
    stopParkingInfoPolling: () => {
        if (refreshId) {
            clearInterval(refreshId);
        }
    },

    /**
     * @function reserveSpot
     * @param {Number} id
     * @return {void}
     * */
    reserveSpot: (id) => {
        if (ParkingSpotsStore.wasSpotReserved === true) {
            console.warn('no more reserving');
        } else {
            ParkingSpotsStore.toggleSpotReservationBlocking(true);
            if (!ParkingSpotsStore.content || !ParkingSpotsStore.content[id]) {
                console.warn('Spot was not found by the provided ID');
                return;
            }
            for (let index = 0; index < ParkingSpotsStore.content.length; ++index) {
                if (ParkingSpotsStore.content[index].currentUserReservation === true) {
                    console.warn('Cannot reserve');
                    return;
                }
            }
            if (ParkingSpotsStore.content && ParkingSpotsStore.content[id].reserved) {
                console.warn('already reserved. Try another');
                return;
            }
            if (ParkingSpotsStore.content && (ParkingSpotsStore.content[id].occupiedByVideo || ParkingSpotsStore.content[id].occupiedBySensors)) {
                console.warn('already occupied. Try another');
                return;
            }
            axios.post('/api/parking', {id: id})
                 .then((response) => {
                     ParkingSpotsStore.reserveParkingSpot(id);
                     //notify user that a spot has been reserved
                 })
                 .catch((response) => {
                     //notify user about an error
                     ParkingSpotsStore.toggleSpotReservationBlocking(false);
                     console.error('Error while receiving response = ', response);
                 });
        }
    }
};

export default ParkingSpotsActions;