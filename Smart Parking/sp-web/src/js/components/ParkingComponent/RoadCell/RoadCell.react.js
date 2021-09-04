import React from 'react';
import * as styles from './RoadCell.scss';

class RoadCell extends React.PureComponent {
    static displayName = 'RoadCell';

    constructor(props) {
        super(props);
    }

    static propTypes = {
        noBorder: React.PropTypes.bool
    };

    render() {
        const {noBorder} = this.props;
        return <div className={styles.container} style={noBorder ? {border: 'none'} : null} />;
    }
}

export default RoadCell;

