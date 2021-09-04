import React from 'react';
import * as styles from './ParkingSpot.scss';

class ParkingSpot extends React.PureComponent {
    static displayName = 'ParkingSpot';

    constructor(props) {
        super(props);
    }

    static propTypes = {
        occupied: React.PropTypes.bool,
        text: React.PropTypes.string,
        noBorder: React.PropTypes.bool,
        leftBorder: React.PropTypes.bool,
        onSpotClick: React.PropTypes.func,
        reserved: React.PropTypes.bool
    };

    render() {
        const {occupied, text, noBorder, leftBorder, onSpotClick, reserved} = this.props;
        let currentClassName = styles.circleGreen;
        if (occupied) {
            currentClassName = styles.circleRed;
        } else if (reserved) {
            currentClassName = styles.circleGrey;
        }
        return (
            <div
                className={styles.container}
                style={noBorder ? {border: 'none'} : {borderLeft: leftBorder ? '4px solid white' : 'none'}}
                onClick={onSpotClick}
            >
                <div className={styles.innerContainer}>
                    <div className={styles.caption}>{text}</div>
                    <div
                        className={currentClassName}
                        style={occupied !== null ? null : {width: '0px', height: '0px'}}
                    />
                </div>
            </div>
        );
    }
}

export default ParkingSpot;

