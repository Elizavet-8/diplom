import React from 'react';
import * as styles from './HighlightCircle.scss';

class HighlightCircle extends React.PureComponent {
    static displayName = 'HighlightCircle';

    constructor(props) {
        super(props);

        this.state = {
            color: 'red'
        };
    }

    _onMouseOver = () => {
        this.setState({color: 'green'});
    };

    _onMouseOut = () => {
        this.setState({color: 'red'});
    };

    render() {
        return (
            <svg
                className={styles.svgContainer}
                onClick={this.props.onClick}
                onMouseOver={this._onMouseOver}
                onMouseOut={this._onMouseOut}
                xmlns="http://www.w3.org/2000/svg"
                width="230"
                height="230"
                id="svg2"
            >
                <g id="layer" style={{transform: 'translate(4px, 14px)'}}>
                    <path
                        x="0" y="0"
                        style={{
                            opacity: 0.5,
                            color: 'black',
                            fill: 'none',
                            fillOpacity: 1.0,
                            fillRule: 'evenodd',
                            stroke: '#192229',
                            strokeWidth: 15,
                            strokeLinecap: 'butt',
                            strokeLinejoin: 'miter',
                            marker: 'none',
                            markerStart: 'none',
                            markerMid: 'none',
                            markerEnd: 'none',
                            strokeMiterlimit: 4,
                            strokeDasharray: 'none',
                            strokeDashoffset: 0,
                            strokeOpacity: 1,
                            visibility: 'visible',
                            display: 'inline',
                            overflow: 'visible'
                        }}
                        id="path1872"
                        d="M 212 105 A 90 90 0 1 1  15,103.21429 A 90 90 0 1 1  212 105 z"
                    />
                </g>
                <g id="layer1" style={{transform: 'translate(15px, 15px)'}}>
                    <path
                        x="0" y="0"
                        style={{
                            opacity: 0.5,
                            color: 'black',
                            fill: 'none',
                            fillOpacity: 1.0,
                            fillRule: 'evenodd',
                            stroke: this.state.color,
                            strokeWidth: 15,
                            strokeLinecap: 'butt',
                            strokeLinejoin: 'miter',
                            marker: 'none',
                            markerStart: 'none',
                            markerMid: 'none',
                            markerEnd: 'none',
                            strokeMiterlimit: 4,
                            strokeDasharray: 'none',
                            strokeDashoffset: 0,
                            strokeOpacity: 1,
                            visibility: 'visible',
                            display: 'inline',
                            overflow: 'visible'
                        }}
                        id="path1872"
                        d="M 190 103.21429 A 87.5 87.5 0 1 1  15,103.21429 A 87.5 87.5 0 1 1  190 103.21429 z"
                    />
                    <text x="43" y="110" fill="white" fontSize="30">{this.props.children}</text>
                </g>
            </svg>
        );
    }
}

export default HighlightCircle;