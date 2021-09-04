import React from 'react';
import * as styles from './ReservationComponent.scss';
import * as constants from '../../reflux/constants/Constants';
import {browserHistory} from 'react-router';
import HighlightCircle from '../Icons/HighlightCircle.react';

class ReservationComponent extends React.PureComponent {
    static displayName = 'ReservationComponent';

    constructor(props) {
        super(props);
    }

    /**
     * @function _reserveSpotTypeMethod
     * @param {String} type
     * @return {void}
     * */
    _reserveSpotTypeMethod = (type) => {
        if (type === constants.AUTO_RESERVATION) {
            browserHistory.push('/parking/auto');
        } else if (type === constants.MANUAL_RESERVATION) {
            browserHistory.push('/parking/manual');
        } else {
            console.error('reserving spot technique is not available or defined');
        }
    };


    render() {
        return (
            <div className={styles.container}>
                <div className={styles.caption}>МЕСТО НА ПАРКОВКЕ</div>
                <HighlightCircle onClick={this._reserveSpotTypeMethod.bind(null, constants.AUTO_RESERVATION)}>
                    ЗАНЯТЬ
                </HighlightCircle>
                <div
                    onClick={this._reserveSpotTypeMethod.bind(null, constants.MANUAL_RESERVATION)}
                    className={styles.reserveButton}
                >
                    Или забронировать вручную
                </div>
            </div>
        );
    }
}

export default ReservationComponent;

